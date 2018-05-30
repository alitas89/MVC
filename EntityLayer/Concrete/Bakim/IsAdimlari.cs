using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsAdimlari : IEntity
    {
        public int IsAdimlariID { get; set; }
        public int BakimPlaniID { get; set; }
        public string IsAdimlariTanim { get; set; }
        public int Sure { get; set; }
        public int TekrarSayisi { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}