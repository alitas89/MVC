using EntityLayer.Concrete.Satinalma;

namespace EntityLayer.ComplexTypes.DtoModel.SatinAlma
{
    public class TeklifIstemeSablonDto:TeklifIstemeSablon
    {
        public string KisimAd { get; set; }
        public string SarfYeriAd { get; set; }
        public string BelgeTuruAd { get; set; }
        public string MasrafTuruAd { get; set; }
    }
}