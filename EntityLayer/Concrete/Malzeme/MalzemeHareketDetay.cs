using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeHareketDetay : IEntity
    {
        public int MalzemeHareketDetayID { get; set; }
        public int MalzemeHareketFisNo { get; set; }
        public int MalzemeID { get; set; }
        public decimal Miktar { get; set; }
        public int DurumID { get; set; }
        public bool Silindi { get; set; }
    }
}
