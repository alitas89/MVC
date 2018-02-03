using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class Ambar : IEntity
    {
        public int AmbarID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int KisimID { get; set; }
        public string Aciklama { get; set; }
        public decimal IsEmriMalzemeFiyatKatsayi { get; set; }
        public int SanalAmbarID { get; set; }
        public int HurdaAmbarID { get; set; }
        public bool SanalAmbar { get; set; }
        public bool VarsayilanDeger { get; set; }
        public string Semt { get; set; }
        public string Sehir { get; set; }
        public string Ulke { get; set; }
        public string Adres { get; set; }
        public bool Silindi { get; set; }
    }
}
