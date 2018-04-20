using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeHareket : IEntity
    {
        public int MalzemeHareketID { get; set; }
        public int MalzemeHareketFisNo { get; set; }
        public int AmbarID { get; set; }
        public int Ambar2ID { get; set; }
        public string Aciklama { get; set; }
        public int MalzemeHareketTuruID { get; set; }
        public bool IsTransfer { get; set; }
        public bool Silindi { get; set; }
    }
}
