using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class SarfYeri : IEntity
    {
        public int SarfYeriID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public decimal Butce { get; set; }
        public decimal HedeflenenButce { get; set; }
        public int VardiyaSinifID { get; set; }
        public int IsletmeID { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string LogoDosyaYolu { get; set; }
        public string Aciklama { get; set; }
        public bool SatinAlmaYeri { get; set; }
        public bool Silindi { get; set; }
    }
}
