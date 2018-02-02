using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Marka : IEntity
    {
        public int MarkaID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }
}
