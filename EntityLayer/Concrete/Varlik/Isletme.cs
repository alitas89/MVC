using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Isletme : IEntity
    {
        public int IsletmeID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string HaritaResmiYolu { get; set; }
        public string Aciklama { get; set; }
    }
}
