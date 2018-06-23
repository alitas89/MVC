using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpPeriyodikBakimDal : DpEntityRepositoryBase<PeriyodikBakim>, IPeriyodikBakimDal
    {
        public List<PeriyodikBakim> GetList()
        {
            return GetListQuery("select * from PeriyodikBakim where Silindi=0", new { });
        }

        public PeriyodikBakim Get(int Id)
        {
            return GetQuery("select * from PeriyodikBakim where PeriyodikBakimID= @Id and Silindi=0", new { Id });
        }


        public int Add(PeriyodikBakim periyodikbakim)
        {
            return AddQuery("insert into PeriyodikBakim(Kod,Ad,BakimPeriyodu,PeriyodBirimID,SonBakimTarih,BakimYapilacakTarih,ToleransGun,VarlikID,BakimArizaID,IsEmriTuruID,IsTipiID,KisimID,OncelikID,SorumluEkipID,IsSorumluID,ArizaNedeniID,BakimSuresi,TahminiBakimMaliyeti,ParaBirimID,StatuID,TalepEdenID,FirmaID,TalepAciklamasi,YapilanIsinAciklamasi,IsOtomatik,IsCalismaZamaniSinirli,GecerlilikBitisTarih,BildirimTriggerID,Silindi) values (@Kod,@Ad,@BakimPeriyodu,@PeriyodBirimID,@SonBakimTarih,@BakimYapilacakTarih,@ToleransGun,@VarlikID,@BakimArizaID,@IsEmriTuruID,@IsTipiID,@KisimID,@OncelikID,@SorumluEkipID,@IsSorumluID,@ArizaNedeniID,@BakimSuresi,@TahminiBakimMaliyeti,@ParaBirimID,@StatuID,@TalepEdenID,@FirmaID,@TalepAciklamasi,@YapilanIsinAciklamasi,@IsOtomatik,@IsCalismaZamaniSinirli,@GecerlilikBitisTarih,@BildirimTriggerID,@Silindi)", periyodikbakim);
        }

        public int Update(PeriyodikBakim periyodikbakim)
        {
            return UpdateQuery("update PeriyodikBakim set Kod=@Kod,Ad=@Ad,BakimPeriyodu=@BakimPeriyodu,PeriyodBirimID=@PeriyodBirimID,SonBakimTarih=@SonBakimTarih,BakimYapilacakTarih=@BakimYapilacakTarih,ToleransGun=@ToleransGun,VarlikID=@VarlikID,BakimArizaID=@BakimArizaID,IsEmriTuruID=@IsEmriTuruID,IsTipiID=@IsTipiID,KisimID=@KisimID,OncelikID=@OncelikID,SorumluEkipID=@SorumluEkipID,IsSorumluID=@IsSorumluID,ArizaNedeniID=@ArizaNedeniID,BakimSuresi=@BakimSuresi,TahminiBakimMaliyeti=@TahminiBakimMaliyeti,ParaBirimID=@ParaBirimID,StatuID=@StatuID,TalepEdenID=@TalepEdenID,FirmaID=@FirmaID,TalepAciklamasi=@TalepAciklamasi,YapilanIsinAciklamasi=@YapilanIsinAciklamasi,IsOtomatik=@IsOtomatik,IsCalismaZamaniSinirli=@IsCalismaZamaniSinirli,GecerlilikBitisTarih=@GecerlilikBitisTarih,BildirimTriggerID=@BildirimTriggerID,Silindi=@Silindi where PeriyodikBakimID=@PeriyodikBakimID", periyodikbakim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from PeriyodikBakim where PeriyodikBakimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update PeriyodikBakim set Silindi = 1 where PeriyodikBakimID=@Id", new { Id });
        }

        public int DeleteSoftWithTransaction(int Id, int BildirimTriggerID)
        {
            var count = 0;
            using (IDbConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                //Periyodik bakım silinir.
                count = connection.Execute("update PeriyodikBakim set Silindi = 1 where PeriyodikBakimID=@Id"
                    , new { Id }, transaction);
                
                //Trigger sonlandırılır.
                BildirimTrigger bildirimTrigger = new BildirimTrigger()
                {
                    BildirimTriggerID = BildirimTriggerID,
                    IsTrigger = false,
                    BitisTarih = DateTime.Now,
                    Silindi = false
                };

                connection.Execute("update BildirimTrigger set "+
                                   "BitisTarih=@BitisTarih," +
                                   "IsTrigger=@IsTrigger, Silindi=@Silindi" +
                                   " where BildirimTriggerID=@BildirimTriggerID",
                    bildirimTrigger, transaction);

                transaction.Commit();
            }
            return count;
        }

        public List<PeriyodikBakim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM PeriyodikBakimDto where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM PeriyodikBakim where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<PeriyodikBakimDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<PeriyodikBakimDto>().GetListDtoQuery($@"SELECT * FROM View_PeriyodikBakimDto where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_PeriyodikBakimDto where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }


        public int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                //Öncelikle BildirimTrigger oluşturulur ve IDsi PeriyodikBakıma işlenir.
                BildirimTrigger bildirimTrigger = new BildirimTrigger()
                {
                    Ad = periyodikBakim.Ad,
                    BaslangicTarih = periyodikBakim.BakimYapilacakTarih,
                    BitisTarih = periyodikBakim.GecerlilikBitisTarih,
                    Icerik = periyodikBakim.ToleransGun + " gün içerisinde",
                    KimeID = kullaniciID,
                    KimeTipID = periyodikBakim.IsTipiID,
                    IsTrigger = false,
                    OlusturanID = kullaniciID,
                    OlusturulmaTarih = DateTime.Now,
                    PeriyotBirimID = periyodikBakim.PeriyodBirimID,
                    PeriyotDeger = periyodikBakim.BakimPeriyodu,
                    QuartzJobTip = 2,
                    Silindi = false
                };

                var strBildirimTriggerID = connection.ExecuteScalar("insert into BildirimTrigger(PeriyotBirimID,PeriyotDeger,OlusturanID,KimeID,KimeTipID,Ad,Icerik,BaslangicTarih,BitisTarih,BildirimAksiyonSayfaID,BildirimAksiyonID,OlusturulmaTarih,SonDegisiklikTarih,QuartzTriggerTarih,IsTrigger,QuartzJobTip,Silindi) values (@PeriyotBirimID,@PeriyotDeger,@OlusturanID,@KimeID,@KimeTipID,@Ad,@Icerik,@BaslangicTarih,@BitisTarih,@BildirimAksiyonSayfaID,@BildirimAksiyonID,@OlusturulmaTarih,@SonDegisiklikTarih,@QuartzTriggerTarih,@IsTrigger,@QuartzJobTip,@Silindi); " +
                                                                      "SELECT CAST(SCOPE_IDENTITY() as int)", bildirimTrigger, transaction);
                int.TryParse(strBildirimTriggerID + "", out int BildirimTriggerID);

                periyodikBakim.BildirimTriggerID = BildirimTriggerID;
                //Periyodik Bakım Oluşturulur
                var strPeriyodikBakimID = connection.ExecuteScalar("insert into PeriyodikBakim(Kod,Ad,BakimPeriyodu,PeriyodBirimID,SonBakimTarih,BakimYapilacakTarih,ToleransGun,VarlikID,BakimArizaID,IsEmriTuruID,IsTipiID,KisimID,OncelikID,SorumluEkipID,IsSorumluID,ArizaNedeniID,BakimSuresi,TahminiBakimMaliyeti,ParaBirimID,StatuID,TalepEdenID,FirmaID,TalepAciklamasi,YapilanIsinAciklamasi,IsOtomatik,IsCalismaZamaniSinirli,GecerlilikBitisTarih,BildirimTriggerID,Silindi) values (@Kod,@Ad,@BakimPeriyodu,@PeriyodBirimID,@SonBakimTarih,@BakimYapilacakTarih,@ToleransGun,@VarlikID,@BakimArizaID,@IsEmriTuruID,@IsTipiID,@KisimID,@OncelikID,@SorumluEkipID,@IsSorumluID,@ArizaNedeniID,@BakimSuresi,@TahminiBakimMaliyeti,@ParaBirimID,@StatuID,@TalepEdenID,@FirmaID,@TalepAciklamasi,@YapilanIsinAciklamasi,@IsOtomatik,@IsCalismaZamaniSinirli,@GecerlilikBitisTarih,@BildirimTriggerID,@Silindi); " +
                                                            "SELECT CAST(SCOPE_IDENTITY() as int)", periyodikBakim, transaction);

                int.TryParse(strPeriyodikBakimID + "", out int PeriyodikBakimID);

                //BakimPlaniAraTablosuna Ekleme Yapılır
                foreach (var item in listBakimPlani)
                {
                    BakimPlaniAraTablo bakimPlaniAraTablo = new BakimPlaniAraTablo()
                    {
                        BakimPlaniID = item,
                        PeriyodikBakimID = PeriyodikBakimID,
                        Silindi = false
                    };
                    count += connection.Execute("insert into BakimPlaniAraTablo(PeriyodikBakimID,BakimPlaniID,Silindi) " +
                                                "values (@PeriyodikBakimID,@BakimPlaniID,@Silindi)", bakimPlaniAraTablo, transaction);
                }

                //BakimRiskiAraTablosuna Ekleme Yapılır
                foreach (var item in listBakimRiski)
                {
                    BakimRiskiAraTablo bakimRiskiAraTablo = new BakimRiskiAraTablo()
                    {
                        BakimRiskiID = item,
                        PeriyodikBakimID = PeriyodikBakimID,
                        Silindi = false
                    };
                    count += connection.Execute("insert into BakimRiskiAraTablo(PeriyodikBakimID,BakimRiskiID,Silindi) values" +
                                                " (@PeriyodikBakimID,@BakimRiskiID,@Silindi)", bakimRiskiAraTablo, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                //Öncelikle BildirimTrigger oluşturulur ve IDsi PeriyodikBakıma işlenir.
                BildirimTrigger bildirimTrigger = new BildirimTrigger()
                {
                    BildirimTriggerID = periyodikBakim.BildirimTriggerID,
                    Ad = periyodikBakim.Ad,
                    BaslangicTarih = periyodikBakim.BakimYapilacakTarih,
                    BitisTarih = periyodikBakim.GecerlilikBitisTarih,
                    Icerik = periyodikBakim.ToleransGun + " gün içerisinde",
                    KimeID = kullaniciID,
                    KimeTipID = periyodikBakim.IsTipiID,
                    IsTrigger = false,
                    OlusturanID = kullaniciID,
                    OlusturulmaTarih = DateTime.Now,
                    PeriyotBirimID = periyodikBakim.PeriyodBirimID,
                    PeriyotDeger = periyodikBakim.BakimPeriyodu,
                    QuartzJobTip = 2,
                    Silindi = false
                };

                connection.Execute("update BildirimTrigger set PeriyotBirimID=@PeriyotBirimID," +
                                         "PeriyotDeger=@PeriyotDeger,OlusturanID=@OlusturanID,KimeID=@KimeID," +
                                         "KimeTipID=@KimeTipID,Ad=@Ad,Icerik=@Icerik,BaslangicTarih=@BaslangicTarih," +
                                         "BitisTarih=@BitisTarih," +
                                         "OlusturulmaTarih=@OlusturulmaTarih," +
                                         "IsTrigger=@IsTrigger,QuartzJobTip=@QuartzJobTip,Silindi=@Silindi" +
                                         " where BildirimTriggerID=@BildirimTriggerID",
                                                                    bildirimTrigger, transaction);

                //Periyodik Bakım Oluşturulur
                connection.Execute("update PeriyodikBakim set Kod=@Kod,Ad=@Ad,BakimPeriyodu=@BakimPeriyodu,PeriyodBirimID=@PeriyodBirimID,SonBakimTarih=@SonBakimTarih,BakimYapilacakTarih=@BakimYapilacakTarih,ToleransGun=@ToleransGun,VarlikID=@VarlikID,BakimArizaID=@BakimArizaID,IsEmriTuruID=@IsEmriTuruID,IsTipiID=@IsTipiID,KisimID=@KisimID,OncelikID=@OncelikID,SorumluEkipID=@SorumluEkipID,IsSorumluID=@IsSorumluID,ArizaNedeniID=@ArizaNedeniID,BakimSuresi=@BakimSuresi,TahminiBakimMaliyeti=@TahminiBakimMaliyeti,ParaBirimID=@ParaBirimID,StatuID=@StatuID,TalepEdenID=@TalepEdenID,FirmaID=@FirmaID,TalepAciklamasi=@TalepAciklamasi,YapilanIsinAciklamasi=@YapilanIsinAciklamasi,IsOtomatik=@IsOtomatik,IsCalismaZamaniSinirli=@IsCalismaZamaniSinirli,GecerlilikBitisTarih=@GecerlilikBitisTarih,BildirimTriggerID=@BildirimTriggerID,Silindi=@Silindi where PeriyodikBakimID=@PeriyodikBakimID"
                    , periyodikBakim, transaction);

                //BakimPlaniAraTablodaki PeriyodikBakimIDsi gelen id olanlar silinir
                connection.Execute("update BakimPlaniAraTablo set Silindi = 1 where PeriyodikBakimID = @PeriyodikBakimID"
                    , periyodikBakim, transaction);

                //BakimRiskiAraTablodaki PeriyodikBakimIDsi gelen id olanlar silinir
                connection.Execute("update BakimRiskiAraTablo set Silindi = 1 where PeriyodikBakimID = @PeriyodikBakimID"
                    , periyodikBakim, transaction);

                //BakimPlaniAraTablosuna Ekleme Yapılır
                foreach (var item in listBakimPlani)
                {
                    BakimPlaniAraTablo bakimPlaniAraTablo = new BakimPlaniAraTablo()
                    {
                        BakimPlaniID = item,
                        PeriyodikBakimID = periyodikBakim.PeriyodikBakimID,
                        Silindi = false
                    };
                    count += connection.Execute("insert into BakimPlaniAraTablo(PeriyodikBakimID,BakimPlaniID,Silindi) " +
                                                "values (@PeriyodikBakimID,@BakimPlaniID,@Silindi)", bakimPlaniAraTablo, transaction);
                }

                //BakimRiskiAraTablosuna Ekleme Yapılır
                foreach (var item in listBakimRiski)
                {
                    BakimRiskiAraTablo bakimRiskiAraTablo = new BakimRiskiAraTablo()
                    {
                        BakimRiskiID = item,
                        PeriyodikBakimID = periyodikBakim.PeriyodikBakimID,
                        Silindi = false
                    };
                    count += connection.Execute("insert into BakimRiskiAraTablo(PeriyodikBakimID,BakimRiskiID,Silindi) values" +
                                                " (@PeriyodikBakimID,@BakimRiskiID,@Silindi)", bakimRiskiAraTablo, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public List<PeriyodikBakim> GetListByVarlikID(int VarlikID)
        {
            return GetListQuery("select * from PeriyodikBakim where VarlikID= @VarlikID and Silindi=0", new { VarlikID });
        }

        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciIDForIsEmri(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTipiForKullaniciTemp>().GetListDtoQuery($@"select a.IsTipiID, b.Ad as IsTipiAd, b.Kod from IsTalebiOnayBirim a inner join IsTipi b on a.IsTipiID=b.IsTipiID where a.KullaniciID=@KullaniciID and a.Silindi=0",
                new { KullaniciID });
        }
    }
}