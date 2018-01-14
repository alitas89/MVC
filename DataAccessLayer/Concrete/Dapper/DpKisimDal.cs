﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpKisimDal : DpEntityRepositoryBase<Kisim>, IKisimDal
    {
        public List<Kisim> GetList()
        {
            return GetListQuery($"select * from Kisim where Silindi=0", new { });
        }

        public Kisim Get(int Id)
        {
            return GetQuery("select * from Kisim where KisimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Kisim kisim)
        {
            return AddQuery("insert Kisim(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,SarfYeriID,Aciklama) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@SarfYeriID,@Aciklama)", kisim);
        }

        public int Update(Kisim kisim)
        {
            return UpdateQuery("update Kisim set Kod=@Kod,Ad=@Ad,Butce=@Butce,HedeflenenButce=@HedeflenenButce,VardiyaSinifID=@VardiyaSinifID,SarfYeriID=@SarfYeriID,Aciklama=@Aciklama where KisimID=@KisimID", kisim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kisim where KisimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kisim set Silindi = 1 where KisimID=@Id", new { Id });
        }
    }
}