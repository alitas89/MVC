using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class DurusNedeni : IEntity
    {
        public int DurusNedeniID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool BakimDurusu { get; set; }
        public string Aciklama { get; set; }
    }
}
