using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class AracServisDto : AracServis
    {
        public string AracAd { get; set; }
        public string GorevAd { get; set; }
        public string AmbarAd { get; set; }
        public string BolumAd { get; set; }
        public string VarlikDurumAd { get; set; }
        public string MarkaAd { get; set; }
        public string ModelAd { get; set; }
        public string ArizaNedeniAd { get; set; }
        public string HizmetAd { get; set; }
    }
}