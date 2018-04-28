using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsTipiDal : DpEntityRepositoryBase<IsTipi>, IIsTipiDal
    {
        public List<IsTipi> GetList()
        {
            return GetListQuery("select * from IsTipi where Silindi=0", new { });
        }

        public IsTipi Get(int Id)
        {
            return GetQuery("select * from IsTipi where IsTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsTipi isTipi)
        {
            return AddQuery("insert into IsTipi(Kod,Ad,BakimOncelikID,IsEmriTuruID,Aciklama,Silindi) values (@Kod,@Ad,@BakimOncelikID,@IsEmriTuruID,@Aciklama,@Silindi)  " +
                " SELECT CAST(SCOPE_IDENTITY() as int)", isTipi, true);
        }

        public int Update(IsTipi isTipi)
        {
            return UpdateQuery("update IsTipi set Kod=@Kod,Ad=@Ad,BakimOncelikID=@BakimOncelikID,IsEmriTuruID=@IsEmriTuruID,Aciklama=@Aciklama,Silindi=@Silindi where IsTipiID=@IsTipiID", isTipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsTipi where IsTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsTipi set Silindi = 1 where IsTipiID=@Id", new { Id });
        }
        public List<IsTipi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM IsTipi where Silindi=0 {filterQuery} {orderQuery}
                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsTipi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<IsTipiDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_IsTipiDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsTipiDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}