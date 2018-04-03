using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class VarlikSablon : IEntity
    {
        public int VarlikSablonID { get; set; }
        public string Ad { get; set; }
        public int VarlikTuruID { get; set; }
        public bool Silindi { get; set; }
    }
}
