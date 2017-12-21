using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVarlikTuruDal : DpEntityRepositoryBase<VarlikTuru>, IVarlikTuruDal
    {
        public List<VarlikTuru> GetList()
        {
            return GetListQuery($"select * from VarlikTuru where Silindi=0", new { });
        }

        public VarlikTuru Get(int Id)
        {
            return GetQuery("select * from VarlikTuru where VarlikTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikTuru varlikturu)
        {
            return AddQuery("insert VarlikTuru(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", varlikturu);
        }

        public int Update(VarlikTuru varlikturu)
        {
            return UpdateQuery("update VarlikTuru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where VarlikTuruID=@VarlikTuruID", varlikturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikTuru where VarlikTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikTuru set Silindi = 1 where VarlikTuruID=@Id", new { Id });
        }
    }
}