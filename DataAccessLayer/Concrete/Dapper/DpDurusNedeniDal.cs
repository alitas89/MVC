using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpDurusNedeniDal : DpEntityRepositoryBase<DurusNedeni>, IDurusNedeniDal
    {
        public List<DurusNedeni> GetList()
        {
            return GetListQuery($"select * from DurusNedeni where Silindi=0", new { });
        }

        public DurusNedeni Get(int Id)
        {
            return GetQuery("select * from DurusNedeni where DurusNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(DurusNedeni durusnedeni)
        {
            return AddQuery("insert DurusNedeni(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", durusnedeni);
        }

        public int Update(DurusNedeni durusnedeni)
        {
            return UpdateQuery("update DurusNedeni set Kod=@Kod,Ad=@Ad,BakimDurusu=@BakimDurusu,Aciklama=@Aciklama where DurusNedeniID=@DurusNedeniID", durusnedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from DurusNedeni where DurusNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update DurusNedeni set Silindi = 1 where DurusNedeniID=@Id", new { Id });
        }
    }
}