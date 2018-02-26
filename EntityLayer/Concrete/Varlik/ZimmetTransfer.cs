using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class ZimmetTransfer : IEntity
    {
        public int ZimmetTransferID { get; set; }
        public string TransferNo { get; set; }
        public DateTime TeslimTarih { get; set; }
        public string TeslimSaat { get; set; }
        public int ZimmetVerenID { get; set; }
        public int ZimmetAlanID { get; set; }
        public int UstVarlikID { get; set; }
        public int YeniKisimID { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}