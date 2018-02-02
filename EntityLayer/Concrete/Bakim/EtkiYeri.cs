using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class EtkiYeri : IEntity
    {
        public int EtkiYeriID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
