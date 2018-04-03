using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class OzNitelik : IEntity
    {
        public int OzNitelikID { get; set; }
        public int VarlikSablonID { get; set; }
        public int BirimID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
