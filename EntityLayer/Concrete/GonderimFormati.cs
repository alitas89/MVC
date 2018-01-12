using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class GonderimFormati : IEntity
    {
        public int GonderimFormatiID { get; set; }
        public int GonderimTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Konu { get; set; }
        public string Format { get; set; }
        public bool Silindi { get; set; }
    }
}
