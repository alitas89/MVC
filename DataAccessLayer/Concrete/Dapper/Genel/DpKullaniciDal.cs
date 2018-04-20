using System;
using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpKullaniciDal : DpEntityRepositoryBase<Kullanici>, IKullaniciDal
    {
        public List<Kullanici> GetList()
        {
            return GetListQuery("select * from Kullanici where Silindi=0", new { });
        }

        public Kullanici Get(int Id)
        {
            return GetQuery("select * from Kullanici where KullaniciId= @Id and Silindi=0", new { Id });
        }

        public int Add(Kullanici kullanici)
        {
            return AddQuery("insert into Kullanici(KullaniciAdi,Sifre,Ad,Soyad,Email,KaynakID,Silindi) values" +
                            " (@KullaniciAdi,@Sifre,@Ad,@Soyad,@Email,@KaynakID,@Silindi)" +
                            " SELECT CAST(SCOPE_IDENTITY() as int)", kullanici, true);
        }

        public int Update(Kullanici kullanici)
        {
            return UpdateQuery("update Kullanici set KullaniciAdi=@KullaniciAdi,Sifre=@Sifre,Ad=@Ad,Soyad=@Soyad," +
                               "Email=@Email,KaynakID=@KaynakID,Silindi=@Silindi where KullaniciId=@KullaniciId", kullanici);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kullanici where KullaniciId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kullanici set Silindi = 1 where KullaniciId=@Id", new { Id });
        }

        public List<Kullanici> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Kullanici where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Kullanici where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre)
        {
            return GetQuery("select * from Kullanici where Silindi=0 and KullaniciAdi= @kullaniciAdi and Sifre = @sifre",
                new { kullaniciAdi, sifre });
        }
    }
}
