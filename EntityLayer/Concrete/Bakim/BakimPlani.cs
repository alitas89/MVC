using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimPlani : IEntity
    {
        public int BakimPlaniID { get; set; }
        public string Kod { get; set; }
        public string BakimPlaniTanim { get; set; }
        public int ToplamBakimSuresi { get; set; }
        public int ToplamIscilikSuresi { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}