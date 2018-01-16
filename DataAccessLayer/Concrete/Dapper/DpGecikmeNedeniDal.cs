using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpGecikmeNedeniDal : DpEntityRepositoryBase<GecikmeNedeni>, IGecikmeNedeniDal
    {
        public List<GecikmeNedeni> GetList()
        {
            return GetListQuery("select * from GecikmeNedeni where Silindi=0", new { });
        }

        public GecikmeNedeni Get(int Id)
        {
            return GetQuery("select * from GecikmeNedeni where GecikmeNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return AddQuery("insert into GecikmeNedeni(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", gecikmenedeni);
        }

        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return UpdateQuery("update GecikmeNedeni set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where GecikmeNedeniID=@GecikmeNedeniID", gecikmenedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GecikmeNedeni where GecikmeNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GecikmeNedeni set Silindi = 1 where GecikmeNedeniID=@Id", new { Id });
        }
    }
}