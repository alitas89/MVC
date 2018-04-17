using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimEkibiUye : IEntity
    {
        public int BakimEkibiUyeID { get; set; }
        public int BakimEkibiID { get; set; }
        public int KaynakID { get; set; }
        public bool Silindi { get; set; }
    }
}
