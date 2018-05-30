using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeSayimi : IEntity
    {
        public int MalzemeSayimiID { get; set; }
        public string SayacNo { get; set; }
        public int MalzemeID { get; set; }
        public int AmbarID { get; set; }
        public int Miktar { get; set; }
        public DateTime Tarih { get; set; }
        public string Saat { get; set; }
        public bool Silindi { get; set; }
    }
}