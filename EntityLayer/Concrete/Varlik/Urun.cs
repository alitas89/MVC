using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Urun : IEntity
    {
        public int UrunID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }
}
