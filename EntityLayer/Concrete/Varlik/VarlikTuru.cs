using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class VarlikTuru : IEntity
    {
        public int VarlikTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }

}
