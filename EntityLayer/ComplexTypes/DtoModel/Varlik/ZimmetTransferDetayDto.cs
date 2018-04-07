using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class ZimmetTransferDetayDto : ZimmetTransferDetay
    {
        public string Ad { get; set; }
        public string Kod { get; set; }
        public string ZimmetliPersonelAd { get; set; }
        public string ZimmetliPersonelID { get; set; }
        public int DurumID { get; set; }
    }
}