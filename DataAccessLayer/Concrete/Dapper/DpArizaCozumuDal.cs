using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpArizaCozumuDal : DpEntityRepositoryBase<ArizaCozumu>, IArizaCozumuDal
    {
        public List<ArizaCozumu> GetList()
        {
            return GetListQuery("select * from ArizaCozumu where Silindi=0", new { });
        }

        public ArizaCozumu Get(int Id)
        {
            return GetQuery("select * from ArizaCozumu where ArizaCozumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaCozumu arizacozumu)
        {
            return AddQuery("insert into ArizaCozumu(Kod,Ad,TekNoktaEgitimiOlustur,Aciklama,Silindi) values (@Kod,@Ad,@TekNoktaEgitimiOlustur,@Aciklama,@Silindi)", arizacozumu);
        }

        public int Update(ArizaCozumu arizacozumu)
        {
            return UpdateQuery("update ArizaCozumu set Kod=@Kod,Ad=@Ad,TekNoktaEgitimiOlustur=@TekNoktaEgitimiOlustur,Aciklama=@Aciklama,Silindi=@Silindi where ArizaCozumuID=@ArizaCozumuID", arizacozumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaCozumu where ArizaCozumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaCozumu set Silindi = 1 where ArizaCozumuID=@Id", new { Id });
        }
    }
}
