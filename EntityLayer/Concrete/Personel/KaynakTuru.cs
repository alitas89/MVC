using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class KaynakTuru : IEntity
    {
        public int KaynakTuruID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}