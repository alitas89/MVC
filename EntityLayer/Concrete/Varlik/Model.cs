using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Model : IEntity
    {
        public int ModelID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public int MarkaID { get; set; }
    }
}
