using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class KaynakTipi : IEntity
    {
        public int KaynakTipiID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}