using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class Kullanici : IEntity
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public int KaynakID { get; set; }
        public bool TalepYetkisi { get; set; }
        public bool Silindi { get; set; }
    }
}
