using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class YetkiGrupKullanici : IEntity
    {
        public int YetkiGrupKullaniciID { get; set; }
        public int YetkiGrupID { get; set; }
        public int KullaniciID { get; set; }
        public bool Silindi { get; set; }
    }
}