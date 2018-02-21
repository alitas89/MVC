using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class IsTipiDto: IsTipi
    {
        public string BakimOncelikAd { get; set; }
        public string BakimOncelikKod { get; set; }
        public string IsEmriTuruAd { get; set; }
        public string IsEmriTuruKod { get; set; }
    }
}
