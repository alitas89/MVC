using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsTipiEmirTuru : IEntity
    {
        public int IsTipiEmirTuruID { get; set; }
        public int IsTipiID { get; set; }
        public int IsEmriTuruID { get; set; }
        public bool Silindi { get; set; }
    }
}
