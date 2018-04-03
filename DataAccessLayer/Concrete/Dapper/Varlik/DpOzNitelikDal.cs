using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpOzNitelikDal : DpEntityRepositoryBase<OzNitelik>, IOzNitelikDal
    {
        public List<OzNitelik> GetList()
        {
            return GetListQuery("select * from OzNitelik where Silindi=0", new { });
        }

        public OzNitelik Get(int Id)
        {
            return GetQuery("select * from OzNitelik where OzNitelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(OzNitelik oznitelik)
        {
            return AddQuery("insert into OzNitelik(VarlikSablonID,BirimID,Ad,Silindi) values (@VarlikSablonID,@BirimID,@Ad,@Silindi)", oznitelik);
        }

        public int Update(OzNitelik oznitelik)
        {
            return UpdateQuery("update OzNitelik set VarlikSablonID=@VarlikSablonID,BirimID=@BirimID,Ad=@Ad,Silindi=@Silindi where OzNitelikID=@OzNitelikID", oznitelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from OzNitelik where OzNitelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update OzNitelik set Silindi = 1 where OzNitelikID=@Id", new { Id });
        }

        public List<OzNitelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM OzNitelik where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM OzNitelik where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<OzNitelikDto> GetList(int VarlikSablonID)
        {
            return new DpDtoRepositoryBase<OzNitelikDto>().GetListDtoQuery("select * from View_OzNitelikDto where VarlikSablonID= @VarlikSablonID and Silindi=0", new { VarlikSablonID });
        }

        public List<OzNitelikDto> GetListByVarlikTuruID(int VarlikTuruID)
        {
            return new DpDtoRepositoryBase<OzNitelikDto>().GetListDtoQuery("select * from View_VarlikSablonOzNitelik where VarlikTuruID= @VarlikTuruID and Silindi=0", new { VarlikTuruID });
        }

        public List<OzNitelikDto> GetListPaginationDto(int VarlikSablonID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<OzNitelikDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_OzNitelikDto where Silindi=0 and VarlikSablonID=@VarlikSablonID {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { VarlikSablonID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(int VarlikSablonID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_OzNitelikDto where Silindi=0 and VarlikSablonID=@VarlikSablonID {filterQuery} ", new { VarlikSablonID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
