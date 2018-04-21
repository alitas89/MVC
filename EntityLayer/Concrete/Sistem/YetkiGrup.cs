using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class YetkiGrup : IEntity
    {
        public int YetkiGrupID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
