using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpIsTalebiOnayBirimDal : DpEntityRepositoryBase<IsTalebiOnayBirim>, IIsTalebiOnayBirimDal
    {
        public List<IsTalebiOnayBirim> GetList()
        {
            return GetListQuery("select * from IsTalebiOnayBirim where Silindi=0", new { });
        }

        public IsTalebiOnayBirim Get(int Id)
        {
            return GetQuery("select * from IsTalebiOnayBirim where IsTalebiOnayBirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsTalebiOnayBirim ıstalebionaybirim)
        {
            return AddQuery("insert into IsTalebiOnayBirim(IsTipiID,KullaniciID,Tarih,Silindi) values (@IsTipiID,@KullaniciID,@Tarih,@Silindi)", ıstalebionaybirim);
        }

        public int Update(IsTalebiOnayBirim ıstalebionaybirim)
        {
            return UpdateQuery("update IsTalebiOnayBirim set IsTipiID=@IsTipiID,KullaniciID=@KullaniciID,Tarih=@Tarih,Silindi=@Silindi where IsTalebiOnayBirimID=@IsTalebiOnayBirimID", ıstalebionaybirim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsTalebiOnayBirim where IsTalebiOnayBirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsTalebiOnayBirim set Silindi = 1 where IsTalebiOnayBirimID=@Id", new { Id });
        }

        public List<IsTalebiOnayBirim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM IsTalebiOnayBirim where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsTalebiOnayBirim where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID)
        {
            return new DpDtoRepositoryBase<IsTalebiKullaniciTemp>().GetListDtoQuery("select KullaniciAdi, i.KullaniciID, i.IsTipiID from IsTalebiOnayBirim i inner join Kullanici k on i.KullaniciID=k.KullaniciId  where IsTipiID=@IsTipiID and i.Silindi=0 and k.Silindi=0",
                new { IsTipiID });
        }

        public int AddWithTransaction(int IsTipiID, List<int> listKullaniciID)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                //Eğer silindi işlemi 1e çekilecek olan kullanıcıların IsTalebiOnayBirim içerisinde sadece 1 kaydı varsa yetkigrubundan IsEmri çıkarılır.
                connection.Execute(@" /*Yetkisi düşürülecek olan kullanıcılar*/
                    update YetkiGrupKullanici set Silindi = 1 where YetkiGrupID = 75 and Silindi = 0 and KullaniciID IN
                    (
                    /*Eşleşen kullanıcılar*/
                    select KullaniciID from IsTalebiOnayBirim where Silindi = 0 and IsTipiID = @IsTipiID  and KullaniciID in (
                    /*Sadece 1 tane kalmış olan kullanıcılar*/
                    select KullaniciID from IsTalebiOnayBirim  where Silindi = 0 group by KullaniciID having COUNT(*) = 1)
                    )", new { IsTipiID }, transaction);


                //Kisima ait olan tüm bağlamalar silinir
                connection.Execute("update IsTalebiOnayBirim set Silindi = 1 where IsTipiID=@IsTipiID", new { IsTipiID }, transaction);

                foreach (var KullaniciID in listKullaniciID)
                {
                    count += connection.Execute("insert into IsTalebiOnayBirim(IsTipiID,KullaniciID,Tarih,Silindi) values" +
                                                "(@IsTipiID,@KullaniciID,@Tarih,@Silindi)", new IsTalebiOnayBirim()
                                                {
                                                    IsTipiID = IsTipiID,
                                                    KullaniciID = KullaniciID,
                                                    Tarih = DateTime.Now,
                                                    Silindi = false
                                                }, transaction);

                    //Bir kullanıcıya iş talebi onay ekleniyorsa, 75-İşEmri yetkisi verilir
                    var resultYetki = connection.Execute(@"declare @Toplam int
                                        select @Toplam = count(*) from YetkiGrupKullanici where KullaniciID = @KullaniciID and YetkiGrupID = 75 and Silindi = 0
                                        if (@Toplam > 0)
                                            begin
                                                select 1;
                                             end
                                        else 
                                        begin
                                            insert into YetkiGrupKullanici(YetkiGrupID, KullaniciID, Silindi) values(75, @KullaniciID, 0)
                                        end", new { KullaniciID }, transaction);
                }

                transaction.Commit();
            }
            return count;
        }
    }
}