using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BilgilendirmeGrubu : IEntity
    {
        public int BilgilendirmeGrubuID { get; set; }
        public int BilgilendirmeTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string YetkiKodu { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
