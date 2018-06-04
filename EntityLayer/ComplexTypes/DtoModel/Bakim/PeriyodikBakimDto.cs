using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class PeriyodikBakimDto : PeriyodikBakim
    {
        public string BirimAd { get; set; }
        public string VarlikAd { get; set; }
        public string BakimArizaKoduAd { get; set; }
        public string IsEmriTuruAd { get; set; }
        public string IsTipiAd { get; set; }
        public string KisimAd { get; set; }
        public string BakimOncelikAd { get; set; }
        public string SorumluAd { get; set; }
        public string SorumluSoyad { get; set; }
        public string SorumluEkipAd { get; set; }
        public string ArizaNedeniAd { get; set; }
        public string ParaBirimAd { get; set; }
        public string StatuAd { get; set; }
        public string TalepEdenAd { get; set; }
        public string TalepEdenSoyad { get; set; }
        public string FirmaAd { get; set; }
    }
}