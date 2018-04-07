using System;
using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

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
            return GetQuery("select * from YetkiGrupKullanici where YetkiGrupKullaniciID= @Id and Silindi=0", new { Id });
        }

        public int Add(YetkiGrupKullanici yetkigrupkullanici)
        {
            return AddQuery("insert into YetkiGrupKullanici(YetkiGrupID,KullaniciID,Silindi) values (@YetkiGrupID,@KullaniciID,@Silindi)", yetkigrupkullanici);
        }

        public int Update(YetkiGrupKullanici yetkigrupkullanici)
        {
            return UpdateQuery("update YetkiGrupKullanici set YetkiGrupID=@YetkiGrupID,KullaniciID=@KullaniciID,Silindi=@Silindi where KullaniciID=@KullaniciID", yetkigrupkullanici);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiGrupKullanici where YetkiGrupKullaniciID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiGrupKullanici set Silindi = 1 where YetkiGrupKullaniciID=@Id", new { Id });
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM YetkiGrupKullanici where Silindi=0 {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int DeleteSoftByKullaniciId(int Id)
        {
            return UpdateQuery("update YetkiGrupKullanici set Silindi = 1 where KullaniciId=@Id", new { Id });
        }

        public List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId)
        {
            return GetListQuery("select * from YetkiGrupKullanici where Silindi=0 and KullaniciId=@kullaniciId", new { kullaniciId });
        }

        public int AddWithTransaction(int kullaniciId, Array arrYetki)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update YetkiGrupKullanici set Silindi = 1 where KullaniciId=@kullaniciId", new { kullaniciId }, transaction);

                foreach (var yetki in arrYetki)
                {
                    count += connection.Execute("insert into YetkiGrupKullanici(YetkiGrupID,KullaniciID,Silindi) values (@YetkiGrupID,@KullaniciID,@Silindi)", new YetkiGrupKullanici()
                    {
                        KullaniciID = kullaniciId,
                        YetkiGrupID = (int)yetki,
                        Silindi = false
                    }, transaction);

                }

                transaction.Commit();
            }
            return count;
        }
    }
}