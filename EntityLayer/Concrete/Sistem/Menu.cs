using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class Menu : IEntity
    {
        public int MenuID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int ParentID { get; set; }
        public int ModulTip { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Sira { get; set; }
        public bool Silindi { get; set; }
    }
}
