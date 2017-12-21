using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class VarlikGrup : IEntity
    {
        public int VarlikGrupID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int IsTipiID { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
    }
}
