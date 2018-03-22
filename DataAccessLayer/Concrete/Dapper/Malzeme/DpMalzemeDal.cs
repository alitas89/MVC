using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeDal : DpEntityRepositoryBase<EntityLayer.Concrete.Malzeme.Malzeme>, IMalzemeDal
    {
        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetList()
        {
            return GetListQuery("select * from Malzeme where Silindi=0", new { });
        }

        public EntityLayer.Concrete.Malzeme.Malzeme Get(int Id)
        {
            return GetQuery("select * from Malzeme where MalzemeID= @Id and Silindi=0", new { Id });
        }

        public int Add(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return AddQuery("insert into Malzeme(Kod,Ad,OlcuBirimID,MalzemeGrupID,MalzemeAltGrupID,SeriNo,MarkaID,ModelID,Silindi) values (@Kod,@Ad,@OlcuBirimID,@MalzemeGrupID,@MalzemeAltGrupID,@SeriNo,@MarkaID,@ModelID,@Silindi)", malzeme);
        }

        public int Update(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return UpdateQuery("update Malzeme set Kod=@Kod,Ad=@Ad,OlcuBirimID=@OlcuBirimID,MalzemeGrupID=@MalzemeGrupID,MalzemeAltGrupID=@MalzemeAltGrupID,SeriNo=@SeriNo,MarkaID=@MarkaID,ModelID=@ModelID,Silindi=@Silindi where MalzemeID=@MalzemeID", malzeme);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Malzeme where MalzemeID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Malzeme set Silindi = 1 where MalzemeID=@Id", new { Id });
        }

        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";
            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }
            //columns ayrımı yapılır 
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT * FROM Malzeme where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Malzeme where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<MalzemeDto> GetListDto()
        {
            return new DpDtoRepositoryBase<MalzemeDto>().GetListDtoQuery("SELECT * FROM View_MalzemeDto where Silindi=0", new { });
        }

        public List<MalzemeDto> GetListPaginationDto(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return new DpDtoRepositoryBase<MalzemeDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_MalzemeDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_MalzemeDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from Malzeme where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }
    }
}
