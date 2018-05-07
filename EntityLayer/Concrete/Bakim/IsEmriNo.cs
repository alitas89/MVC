using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsEmriNo : IEntity
    {
        public int IsEmriNoID { get; set; }
        public int IsTalepID { get; set; }
        public int IsEmriID { get; set; }
        public DateTime Tarih { get; set; }
        public bool Silindi { get; set; }
    }
}