using EntityLayer.Concrete.Personel;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class KaynakDto:Kaynak
    {
        public string KisimAd { get; set; }
        public string SarfYeriAd { get; set; }
        public string IsletmeAd { get; set; }
        public string VarlikAd { get; set; }
        public string EkipAd { get; set; }
        public string KaynakSinifiAd { get; set; }
        public string VardiyaAd { get; set; }
        public string StatuAd { get; set; }
        public string AmbarAd { get; set; }
        public string KaynakPozisyonuAd { get; set; }
        public string IsTipiAd { get; set; }
        public string KaynakTipiAd { get; set; }
        public string KaynakDurumuAd { get; set; }
        public string KaynakTuruAd { get; set; }
    }
}