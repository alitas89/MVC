using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class BildirimIsTalebiSonuc : IEntity
    {
        public int BildirimIsTalebiSonucID { get; set; }
        public int IsEmriNoID { get; set; }
        public bool Silindi { get; set; }
    }
}