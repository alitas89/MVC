using System;
using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpGenelBildirimDal : DpEntityRepositoryBase<GenelBildirim>, IGenelBildirimDal
    {
        public List<GenelBildirim> GetList()
        {
            return GetListQuery("select * from GenelBildirim where Silindi=0", new { });
        }

        public GenelBildirim Get(int Id)
        {
            return GetQuery("select * from GenelBildirim where BildirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(GenelBildirim genelbildirim)
        {
            return AddQuery("insert into GenelBildirim(BildirimTriggerID,Tip,Kime,KimeTip,Ad,Icerik,BildirimAksiyonSayfaID,BildirimAksiyonID,BildirimTarih,Tarih,OkunmaTarih,PushTarih,IsOkundu,IsPush,Silindi) values (@BildirimTriggerID,@Tip,@Kime,@KimeTip,@Ad,@Icerik,@BildirimAksiyonSayfaID,@BildirimAksiyonID,@BildirimTarih,@Tarih,@OkunmaTarih,@PushTarih,@IsOkundu,@IsPush,@Silindi)", genelbildirim);
        }

        public int Update(GenelBildirim genelbildirim)
        {
            return UpdateQuery("update GenelBildirim set BildirimTriggerID=@BildirimTriggerID,Tip=@Tip,Kime=@Kime,KimeTip=@KimeTip,Ad=@Ad,Icerik=@Icerik,BildirimAksiyonSayfaID=@BildirimAksiyonSayfaID,BildirimAksiyonID=@BildirimAksiyonID,BildirimTarih=@BildirimTarih,Tarih=@Tarih,OkunmaTarih=@OkunmaTarih,PushTarih=@PushTarih,IsOkundu=@IsOkundu,IsPush=@IsPush,Silindi=@Silindi where BildirimID=@BildirimID", genelbildirim);
        }

        public int UpdatePushOkundu(GenelBildirimPushOkundu genelBildirimPushOkundu)
        {
            return UpdateQuery("update GenelBildirim set OkunmaTarih=@OkunmaTarih,PushTarih=@PushTarih,IsOkundu=@IsOkundu,IsPush=@IsPush where BildirimID=@BildirimID", genelBildirimPushOkundu);
        }

        public int GetCountByKime(int KullaniciID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GenelBildirim where Silindi = 0 and 
                                         (Kime = @KullaniciID or KimeTip in 
                                         (select IsTipiID from IsTalebiOnayBirim where KullaniciID = @KullaniciID))
                                        { filterQuery }", new { KullaniciID }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GenelBildirim where BildirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GenelBildirim set Silindi = 1 where BildirimID=@Id", new { Id });
        }

        public List<GenelBildirim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM GenelBildirim where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GenelBildirim where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<GenelBildirim> GetListPaginationByKime(PagingParams pagingParams, int KullaniciID)
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

            return GetListQuery($@"
                                SELECT BildirimID, BildirimTriggerID, Tip, Kime, KimeTip, Ad, Icerik, 
                                    BildirimAksiyonSayfaID, BildirimAksiyonID, 
                                    BildirimTarih, Tarih, QuartzJobTip, Silindi,
                                
								--okundu durumu
								case when QuartzJobTip=2
									then 						
									case when EXISTS(select * from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsOkundu
										end as IsOkundu,

								--push durumu
								case when QuartzJobTip=2
									then 									
									case when EXISTS(select * from PeriyodikBakimBildirimPushed where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsPush
										end as IsPush,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(OkunmaTarih) from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.OkunmaTarih
										end as OkunmaTarih,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(PushTarih) from PeriyodikBakimBildirimPushed where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.PushTarih
										end as PushTarih

                                    FROM GenelBildirim as G where Silindi=0 
                                and
                                (Kime = @KullaniciID or KimeTip in 
                                (select IsTipiID from IsTalebiOnayBirim where KullaniciID = @KullaniciID))
                                    {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { KullaniciID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public List<GenelBildirim> GetListByKime(int Kime)
        {
            return GetListQuery("select * from GenelBildirim where Silindi=0 and Kime=@Kime", new { Kime });
        }

        public List<GenelBildirim> GetListYeniBildirimByKime(int Kime)
        {
            return GetListQuery($@"
                                select * from (

                                 SELECT BildirimID, BildirimTriggerID, Tip, Kime, KimeTip, Ad, Icerik, 
                                    BildirimAksiyonSayfaID, BildirimAksiyonID, 
                                    BildirimTarih, Tarih, QuartzJobTip, Silindi,
                                
								--okundu durumu
								case when QuartzJobTip=2
									then 						
									case when EXISTS(select * from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@Kime and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsOkundu
										end as IsOkundu,

								--push durumu
								case when QuartzJobTip=2
									then 									
									case when EXISTS(select * from PeriyodikBakimBildirimPushed where 
												KullaniciID=@Kime and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsPush
										end as IsPush,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(OkunmaTarih) from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@Kime and BildirimID=G.BildirimID)
									else G.OkunmaTarih
										end as OkunmaTarih,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(PushTarih) from PeriyodikBakimBildirimPushed where 
												KullaniciID=@Kime and BildirimID=G.BildirimID)
									else G.PushTarih
										end as PushTarih

                                    FROM GenelBildirim as G where Silindi=0 
                                and
                                (Kime = @Kime or KimeTip in 
                                (select IsTipiID from IsTalebiOnayBirim where KullaniciID = @Kime))
                                    
                                ) t where IsOkundu=0
								",
                new { Kime });
        }

        public List<GenelBildirimKullaniciDto> GetListGenelBildirimKullaniciDtoByKime(int BildirimID, int KullaniciID)
        {
            return new DpDtoRepositoryBase<GenelBildirimKullaniciDto>().GetListDtoQuery($@"
                 select t.BildirimID, t.Kime, t.KimeTip, t.Ad, t.Icerik, 
		                        t.BildirimTarih, t.BildirimTriggerID, t.QuartzJobTip,
		                        t.OkunmaTarih, t.PushTarih, t.IsPush, t.IsOkundu,
		                        bil.PeriyotDeger, brm.BirimAd, k.KullaniciAdi as OlusturanKullaniciAdi
								from (SELECT BildirimID, Kime, KimeTip, Ad, Icerik, 
                                                BildirimTarih, BildirimTriggerID, QuartzJobTip,
                                
								--okundu durumu
								case when QuartzJobTip=2
									then 						
									case when EXISTS(select * from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsOkundu
										end as IsOkundu,

								--push durumu
								case when QuartzJobTip=2
									then 									
									case when EXISTS(select * from PeriyodikBakimBildirimPushed where 
												 KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsPush
										end as IsPush,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(OkunmaTarih) from PeriyodikBakimBildirimOkundu where 
												 KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.OkunmaTarih
										end as OkunmaTarih,

								--push tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(PushTarih) from PeriyodikBakimBildirimPushed where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.PushTarih
										end as PushTarih

                                    FROM GenelBildirim as G where Silindi=0 
                                and BildirimID=@BildirimID
                                ) t
							inner join BildirimTrigger bil on t.BildirimTriggerID = bil.BildirimTriggerID
							inner join Birim brm on bil.PeriyotBirimID=brm.BirimID
							inner join Kullanici k on bil.OlusturanID=k.KullaniciID
            ", new { BildirimID, KullaniciID });
        }

        public List<GenelBildirimYoneticiDto> GetListGenelBildirimYoneticiDtoByKime(int BildirimID, int KullaniciID)
        {
            return new DpDtoRepositoryBase<GenelBildirimYoneticiDto>().GetListDtoQuery($@"
                              select t.*, bil.PeriyotBirimID, bil.OlusturanID, bil.PeriyotDeger, bil.KimeTipID,
                              bil.BaslangicTarih, bil.BitisTarih, bil.OlusturulmaTarih, bil.SonDegisiklikTarih,
                              bil.QuartzTriggerTarih, bil.IsTrigger ,brm.BirimAd, k.KullaniciAdi as OlusturanKullaniciAdi
								
		                            from (SELECT BildirimID, BildirimTriggerID, Tip, Kime, KimeTip, Ad,
		                            Icerik, BildirimAksiyonSayfaID, BildirimAksiyonID, BildirimTarih, Tarih,
		                            QuartzJobTip,
                                
								--okundu durumu
								case when QuartzJobTip=2
									then 						
									case when EXISTS(select * from PeriyodikBakimBildirimOkundu where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsOkundu
										end as IsOkundu,

								--push durumu
								case when QuartzJobTip=2
									then 									
									case when EXISTS(select * from PeriyodikBakimBildirimPushed where 
												 KullaniciID=@KullaniciID and BildirimID=G.BildirimID) then 1 else 0 end
									else G.IsPush
										end as IsPush,

								--okunma tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(OkunmaTarih) from PeriyodikBakimBildirimOkundu where 
												 KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.OkunmaTarih
										end as OkunmaTarih,

								--push tarihi
								case when QuartzJobTip=2
									then 						
										(select MAX(PushTarih) from PeriyodikBakimBildirimPushed where 
												KullaniciID=@KullaniciID and BildirimID=G.BildirimID)
									else G.PushTarih
										end as PushTarih

                                    FROM GenelBildirim as G where Silindi=0 
                                and BildirimID=@BildirimID
                                ) t

							inner join BildirimTrigger bil on t.BildirimTriggerID = bil.BildirimTriggerID
							inner join Birim brm on bil.PeriyotBirimID=brm.BirimID
							inner join Kullanici k on bil.OlusturanID=k.KullaniciID            
            ", new { BildirimID, KullaniciID });
        }
    }
}