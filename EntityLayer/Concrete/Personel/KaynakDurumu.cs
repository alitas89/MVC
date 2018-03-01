using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class KaynakDurumu : IEntity
    {
        public int KaynakDurumuID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}