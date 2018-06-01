using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class Firma : IEntity
    {
        public int FirmaID { get; set; }
        public string Ad { get; set; }
        public string Kod { get; set; }
        public string Sorumlu { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public bool Silindi { get; set; }
    }
}