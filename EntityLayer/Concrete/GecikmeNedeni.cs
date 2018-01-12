using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class GecikmeNedeni : IEntity
    {
        public int GecikmeNedeniID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}