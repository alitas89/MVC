using EntityLayer.Concrete.Malzeme;

namespace EntityLayer.ComplexTypes.DtoModel.Malzeme
{
    public class AmbarDto : Ambar
    {
        public string KisimAd { get; set; }
        public string SanalAmbarAd { get; set; }
        public string HurdaAmbarAd { get; set; }
    }
}