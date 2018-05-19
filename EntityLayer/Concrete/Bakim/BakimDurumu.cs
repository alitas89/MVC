using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimDurumu : IEntity
    {
        public int BakimDurumuID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}