﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpYetkiGrupKullaniciDal : DpEntityRepositoryBase<YetkiGrupKullanici>, IYetkiGrupKullaniciDal
    {
        public List<YetkiGrupKullanici> GetList()
        {
            return GetListQuery("select * from YetkiGrupKullanici where Silindi=0", new { });
        }

        public YetkiGrupKullanici Get(int Id)
        {
            return GetQuery("select * from YetkiGrupKullanici where KullaniciID= @Id and Silindi=0", new { Id });
        }

        public int Add(YetkiGrupKullanici yetkigrupkullanici)
        {
            return AddQuery("insert into YetkiGrupKullanici(YetkiGrupKullaniciID,YetkiGrupID,Silindi) values (@YetkiGrupKullaniciID,@YetkiGrupID,@Silindi)", yetkigrupkullanici);
        }

        public int Update(YetkiGrupKullanici yetkigrupkullanici)
        {
            return UpdateQuery("update YetkiGrupKullanici set YetkiGrupKullaniciID=@YetkiGrupKullaniciID,YetkiGrupID=@YetkiGrupID,Silindi=@Silindi where KullaniciID=@KullaniciID", yetkigrupkullanici);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiGrupKullanici where KullaniciID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiGrupKullanici set Silindi = 1 where KullaniciID=@Id", new { Id });
        }

        public List<YetkiGrupKullanici> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";
           
            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM YetkiGrupKullanici where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM YetkiGrupKullanici {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId)
        {
            return GetListQuery("select * from YetkiGrupKullanici where Silindi=0 and KullaniciId=@kullaniciId", new { kullaniciId });
        }
    }
}