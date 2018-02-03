using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeSeriNo : IEntity
    {
        public int SeriNoID { get; set; }
        public string SeriNo { get; set; }
        public string OzelKod { get; set; }
        public int MalzemeID { get; set; }
        public int Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
