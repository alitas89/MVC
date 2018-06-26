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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GenelBildirim where Silindi = 0 and Kime=@KullaniciID { filterQuery }", new { KullaniciID }) + "";

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

            return GetListQuery($@"SELECT * FROM GenelBildirim where Silindi=0 and Kime=@KullaniciID {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { KullaniciID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public List<GenelBildirim> GetListByKime(int Kime)
        {
            return GetListQuery("select * from GenelBildirim where Silindi=0 and Kime=@Kime", new { Kime });
        }

        public List<GenelBildirim> GetListYeniBildirimByKime(int Kime)
        {
            return GetListQuery("select * from GenelBildirim where Silindi=0 and Kime=@Kime and IsOkundu=0 and IsPush=0 ",
                new { Kime });
        }

        public List<GenelBildirimKullaniciDto> GetListGenelBildirimKullaniciDtoByKime(int BildirimID)
        {
            return new DpDtoRepositoryBase<GenelBildirimKullaniciDto>().GetListDtoQuery("select * from View_GenelBildirimKullaniciDto where BildirimID=@BildirimID", new { BildirimID });
        }

        public List<GenelBildirimYoneticiDto> GetListGenelBildirimYoneticiDtoByKime(int BildirimID)
        {
            return new DpDtoRepositoryBase<GenelBildirimYoneticiDto>().GetListDtoQuery("select * from View_GenelBildirimYoneticiDto where BildirimID=@BildirimID", new { BildirimID });
        }
    }
}