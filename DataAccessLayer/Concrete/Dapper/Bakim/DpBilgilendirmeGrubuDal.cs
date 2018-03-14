using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBilgilendirmeGrubuDal : DpEntityRepositoryBase<BilgilendirmeGrubu>, IBilgilendirmeGrubuDal
    {
        public List<BilgilendirmeGrubu> GetList()
        {
            return GetListQuery("select * from BilgilendirmeGrubu where Silindi=0", new { });
        }

        public BilgilendirmeGrubu Get(int Id)
        {
            return GetQuery("select * from BilgilendirmeGrubu where BilgilendirmeGrubuID= @Id and Silindi=0", new { Id });
        }

        public int Add(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return AddQuery("insert into BilgilendirmeGrubu(BilgilendirmeTuruID,Kod,Ad,YetkiKodu,Aciklama,Silindi) values (@BilgilendirmeTuruID,@Kod,@Ad,@YetkiKodu,@Aciklama,@Silindi)", bilgilendirmegrubu);
        }

        public int Update(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return UpdateQuery("update BilgilendirmeGrubu set BilgilendirmeTuruID=@BilgilendirmeTuruID,Kod=@Kod,Ad=@Ad,YetkiKodu=@YetkiKodu,Aciklama=@Aciklama,Silindi=@Silindi where BilgilendirmeGrubuID=@BilgilendirmeGrubuID", bilgilendirmegrubu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BilgilendirmeGrubu where BilgilendirmeGrubuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BilgilendirmeGrubu set Silindi = 1 where BilgilendirmeGrubuID=@Id", new { Id });
        }
        public List<BilgilendirmeGrubu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM BilgilendirmeGrubu where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BilgilendirmeGrubu where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<BilgilendirmeGrubuDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<BilgilendirmeGrubuDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_BilgilendirmeGrubuDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_BilgilendirmeGrubuDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}