using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class VarlikDurumu : IEntity
    {
        public int VarlikDurumuID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }
}
