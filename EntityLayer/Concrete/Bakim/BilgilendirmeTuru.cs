using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BilgilendirmeTuru : IEntity
    {
        public int BilgilendirmeTuruID { get; set; }
        public string BilgilendirmeTuruAd { get; set; }
        public bool Silindi { get; set; }
    }
}
