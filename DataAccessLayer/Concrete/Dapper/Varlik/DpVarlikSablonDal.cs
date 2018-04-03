using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikSablonDal : DpEntityRepositoryBase<VarlikSablon>, IVarlikSablonDal
    {
        public List<VarlikSablon> GetList()
        {
            return GetListQuery("select * from VarlikSablon where Silindi=0", new { });
        }

        public VarlikSablon Get(int Id)
        {
            return GetQuery("select * from VarlikSablon where VarlikSablonID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikSablon varliksablon)
        {
            return AddQuery("insert into VarlikSablon(Ad,VarlikTuruID,Silindi) values (@Ad,@VarlikTuruID,@Silindi); " +
                " SELECT CAST(SCOPE_IDENTITY() as int)", varliksablon, true);
        }

        public int Update(VarlikSablon varliksablon)
        {
            return UpdateQuery("update VarlikSablon set Ad=@Ad,VarlikTuruID=@VarlikTuruID,Silindi=@Silindi where VarlikSablonID=@VarlikSablonID", varliksablon);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikSablon where VarlikSablonID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikSablon set Silindi = 1 where VarlikSablonID=@Id", new { Id });
        }

        public List<VarlikSablon> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VarlikSablon where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikSablon where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }


        public List<VarlikSablonDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikSablonDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikSablonDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikSablonDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsSablonDefined(int VarlikTuruID)
        {
            var result = GetScalarQuery("select Count(*) from VarlikSablon where VarlikTuruID= @VarlikTuruID and Silindi=0", new { VarlikTuruID }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }
    }
}
