﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVarlikGrupDal : DpEntityRepositoryBase<VarlikGrup>, IVarlikGrupDal
    {
        public List<VarlikGrup> GetList()
        {
            return GetListQuery($"select * from VarlikGrup where Silindi=0", new { });
        }

        public VarlikGrup Get(int Id)
        {
            return GetQuery("select * from VarlikGrup where VarlikGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikGrup varlikgrup)
        {
            return AddQuery("insert VarlikGrup(Kod,Ad,IsTipiID,Aciklama1,Aciklama2,Aciklama3) values (@Kod,@Ad,@IsTipiID,@Aciklama1,@Aciklama2,@Aciklama3)", varlikgrup);
        }

        public int Update(VarlikGrup varlikgrup)
        {
            return UpdateQuery("update VarlikGrup set Kod=@Kod,Ad=@Ad,IsTipiID=@IsTipiID,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3 where VarlikGrupID=@VarlikGrupID", varlikgrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikGrup where VarlikGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikGrup set Silindi = 1 where VarlikGrupID=@Id", new { Id });
        }
    }
}