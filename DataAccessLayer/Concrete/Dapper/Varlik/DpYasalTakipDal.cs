using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpYasalTakipDal : DpEntityRepositoryBase<YasalTakip>, IYasalTakipDal
    {
        public List<YasalTakip> GetList()
        {
            return GetListQuery("select * from YasalTakip where Silindi=0", new { });
        }

        public YasalTakip Get(int Id)
        {
            return GetQuery("select * from YasalTakip where YasalTakipID= @Id and Silindi=0", new { Id });
        }

        public int Add(YasalTakip yasaltakip)
        {
            return AddQuery("insert into YasalTakip(VarlikID,KaskoPoliceNo,KaskoSigortaSirketi,KaskoBaslangicTarih,KaskoBitisTarih,KaskoSigortaPrimTutar,KaskoUyariListesineEkle,TrafikSigortaPoliceNo,TrafikSigortaSirketi,TrafikSigortaBaslangicTarih,TrafikSigortaBitisTarih,TrafikSigortaPrimTutar,TrafikSigortaUyariListesineEkle,AracSonMuayeneTarih,AracSonrakiMuayeneTarih,AracMuayeneUyariListesineEkle,MTVBaslangicTarih,MTVBitisTarih,MTVUyariListesineEkle,Silindi) values (@VarlikID,@KaskoPoliceNo,@KaskoSigortaSirketi,@KaskoBaslangicTarih,@KaskoBitisTarih,@KaskoSigortaPrimTutar,@KaskoUyariListesineEkle,@TrafikSigortaPoliceNo,@TrafikSigortaSirketi,@TrafikSigortaBaslangicTarih,@TrafikSigortaBitisTarih,@TrafikSigortaPrimTutar,@TrafikSigortaUyariListesineEkle,@AracSonMuayeneTarih,@AracSonrakiMuayeneTarih,@AracMuayeneUyariListesineEkle,@MTVBaslangicTarih,@MTVBitisTarih,@MTVUyariListesineEkle,@Silindi)", yasaltakip);
        }

        public int Update(YasalTakip yasaltakip)
        {
            return UpdateQuery("update YasalTakip set VarlikID=@VarlikID,KaskoPoliceNo=@KaskoPoliceNo,KaskoSigortaSirketi=@KaskoSigortaSirketi,KaskoBaslangicTarih=@KaskoBaslangicTarih,KaskoBitisTarih=@KaskoBitisTarih,KaskoSigortaPrimTutar=@KaskoSigortaPrimTutar,KaskoUyariListesineEkle=@KaskoUyariListesineEkle,TrafikSigortaPoliceNo=@TrafikSigortaPoliceNo,TrafikSigortaSirketi=@TrafikSigortaSirketi,TrafikSigortaBaslangicTarih=@TrafikSigortaBaslangicTarih,TrafikSigortaBitisTarih=@TrafikSigortaBitisTarih,TrafikSigortaPrimTutar=@TrafikSigortaPrimTutar,TrafikSigortaUyariListesineEkle=@TrafikSigortaUyariListesineEkle,AracSonMuayeneTarih=@AracSonMuayeneTarih,AracSonrakiMuayeneTarih=@AracSonrakiMuayeneTarih,AracMuayeneUyariListesineEkle=@AracMuayeneUyariListesineEkle,MTVBaslangicTarih=@MTVBaslangicTarih,MTVBitisTarih=@MTVBitisTarih,MTVUyariListesineEkle=@MTVUyariListesineEkle,Silindi=@Silindi where YasalTakipID=@YasalTakipID", yasaltakip);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YasalTakip where YasalTakipID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YasalTakip set Silindi = 1 where YasalTakipID=@Id", new { Id });
        }

        public List<YasalTakip> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM YasalTakip where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM YasalTakip where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public YasalTakip GetYasalTakipByVarlikID(int VarlikID)
        {
            return GetQuery("select * from YasalTakip where VarlikID= @VarlikID and Silindi=0", new { VarlikID });
        }
    }
}