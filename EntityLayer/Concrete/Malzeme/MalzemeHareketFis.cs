using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeHareketFis : IEntity
    {
        public int MalzemeHareketFisNo { get; set; }
        public DateTime FisTarih { get; set; }
        public string FisSaat { get; set; }
        public bool Silindi { get; set; }
    }
}
