using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsTipi : IEntity
    {
        public int IsTipiID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int BakimOnceligiID { get; set; }
        public int IsEmriTuruID { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
