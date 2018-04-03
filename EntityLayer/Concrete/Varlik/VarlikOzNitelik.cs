using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class VarlikOzNitelik : IEntity
    {
        public int VarlikOzNitelikID { get; set; }
        public int VarlikID { get; set; }
        public int OzNitelikID { get; set; }
        public string Deger { get; set; }
        public bool Silindi { get; set; }
    }
}
