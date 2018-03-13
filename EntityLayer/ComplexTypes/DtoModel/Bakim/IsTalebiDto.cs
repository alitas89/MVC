using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class IsTalebiDto : IsTalebi
    {
        public string VarlikAd { get; set; }

        public string IsEmriTuruAd { get; set; }

        public string KisimAd { get; set; }

        public string IsTipiAd { get; set; }

        public string BakimArizaKoduAd { get; set; }

        public string BakimOncelikAd { get; set; }

        public string SorumluAd { get; set; }

        public string OnaylayanAd { get; set; }

        public string TalepEdenAd { get; set; }
    }
}
