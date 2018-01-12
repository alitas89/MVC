using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVK_DurusNedeniDal : DpEntityRepositoryBase<VK_DurusNedeni>, IDurusNedeniDal
    {
        public List<VK_DurusNedeni> GetList()
        {
            return GetListQuery($"select * from DurusNedeni where Silindi=0", new { });
        }

        public VK_DurusNedeni Get(int Id)
        {
            return GetQuery("select * from DurusNedeni where DurusNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(VK_DurusNedeni durusnedeni)
        {
            return AddQuery("insert DurusNedeni(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", durusnedeni);
        }

        public int Update(VK_DurusNedeni durusnedeni)
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