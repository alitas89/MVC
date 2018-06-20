using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class BildirimAksiyonSayfa : IEntity
    {
        public int BildirimAksiyonSayfaID { get; set; }
        public string Ad { get; set; }
        public string Url { get; set; }
        public bool Silindi { get; set; }
    }
}