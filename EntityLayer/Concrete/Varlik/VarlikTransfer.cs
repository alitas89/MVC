using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class VarlikTransfer : IEntity
    {
        public int VarlikTransferID { get; set; }
        public int TransferNo { get; set; }
        public int VarlikID { get; set; }
        public int MevcutKisimID { get; set; }
        public int MevcutSahipVarlikID { get; set; }
        public int YeniSahipVarlikID { get; set; }
        public int YeniKisimID { get; set; }
        public int IslemiYapanID { get; set; }
        public DateTime Tarih { get; set; }
        public string Saat { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
