using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpArizaNedeniDal : DpEntityRepositoryBase<ArizaNedeni>, IArizaNedeniDal
    {
        public List<ArizaNedeni> GetList()
        {
            return GetListQuery("select * from ArizaNedeni where Silindi=0", new { });
        }

        public ArizaNedeni Get(int Id)
        {
            return GetQuery("select * from ArizaNedeni where ArizaNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaNedeni arizanedeni)
        {
            return AddQuery("insert into ArizaNedeni(Kod,GenelKod,Ad,UretimiDurdurur,NedenAnaliziZorunluOlmali,Aciklama,Silindi) values (@Kod,@GenelKod,@Ad,@UretimiDurdurur,@NedenAnaliziZorunluOlmali,@Aciklama,@Silindi)", arizanedeni);
        }

        public int Update(ArizaNedeni arizanedeni)
        {
            return UpdateQuery("update ArizaNedeni set Kod=@Kod,GenelKod=@GenelKod,Ad=@Ad,UretimiDurdurur=@UretimiDurdurur,NedenAnaliziZorunluOlmali=@NedenAnaliziZorunluOlmali,Aciklama=@Aciklama,Silindi=@Silindi where ArizaNedeniID=@ArizaNedeniID", arizanedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaNedeni where ArizaNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaNedeni set Silindi = 1 where ArizaNedeniID=@Id", new { Id });
        }
    }
}