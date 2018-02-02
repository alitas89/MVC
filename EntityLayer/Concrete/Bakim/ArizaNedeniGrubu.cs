using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class ArizaNedeniGrubu : IEntity
    {
        public int ArizaNedeniGrubuID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
