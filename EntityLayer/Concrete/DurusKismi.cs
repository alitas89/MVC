using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class DurusKismi : IEntity
    {
        public int DurusKismiID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool BakimDurusu { get; set; }
        public string Aciklama { get; set; }
    }
}
