using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class ZimmetTransferDetay : IEntity
    {
        public int ZimmetTransferDetayID { get; set; }
        public int VarlikID { get; set; }
        public int ZimmetTransferID { get; set; }
        public bool Silindi { get; set; }
    }
}