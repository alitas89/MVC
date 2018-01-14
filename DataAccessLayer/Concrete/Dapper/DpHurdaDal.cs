﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpHurdaDal : DpEntityRepositoryBase<Hurda>, IHurdaDal
    {
        public List<Hurda> GetList()
        {
            return GetListQuery($"select * from Hurda where Silindi=0", new { });
        }

        public Hurda Get(int Id)
        {
            return GetQuery("select * from Hurda where HurdaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hurda hurda)
        {
            return AddQuery("insert Hurda(BarkodKod,VarlikID,OzurKod,OzurAd,OzurTip,Tarih,Miktar,Toplam,Aciklama) values (@BarkodKod,@VarlikID,@OzurKod,@OzurAd,@OzurTip,@Tarih,@Miktar,@Toplam,@Aciklama)", hurda);
        }

        public int Update(Hurda hurda)
        {
            return UpdateQuery("update Hurda set BarkodKod=@BarkodKod,VarlikID=@VarlikID,OzurKod=@OzurKod,OzurAd=@OzurAd,OzurTip=@OzurTip,Tarih=@Tarih,Miktar=@Miktar,Toplam=@Toplam,Aciklama=@Aciklama where HurdaID=@HurdaID", hurda);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hurda where HurdaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hurda set Silindi = 1 where HurdaID=@Id", new { Id });
        }
    }
}