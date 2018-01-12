using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class BakimOncelik : IEntity
    {
        public int BakimOncelikID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int TamamlanmaZamani { get; set; }
        public int BirimID { get; set; }
        public string Aciklama { get; set; }
        public int TeminSureleriID { get; set; }
        public bool IsEmriVarsayilani { get; set; }
        public bool IsTalepVarsayilani { get; set; }
        public bool PeriyodikBakimVarsayilani { get; set; }
        public bool Silindi { get; set; }
    }
}
