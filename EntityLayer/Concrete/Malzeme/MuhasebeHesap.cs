using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MuhasebeHesap : IEntity
    {
        public int MuhasebeHesapID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
