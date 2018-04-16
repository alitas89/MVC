using EntityLayer.Concrete.Malzeme;

namespace EntityLayer.ComplexTypes.DtoModel.Malzeme
{
    public class MalzemeHareketDto : MalzemeHareket
    {
        public string AmbarAd { get; set; }
        public string Ambar2Ad { get; set; }
        public string HareketTuruAd { get; set; }
        public int DurumID { get; set; }
    }
}
