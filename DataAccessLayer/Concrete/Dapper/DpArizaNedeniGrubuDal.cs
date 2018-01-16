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
    public class DpArizaNedeniGrubuDal : DpEntityRepositoryBase<ArizaNedeniGrubu>, IArizaNedeniGrubuDal
    {
        public List<ArizaNedeniGrubu> GetList()
        {
            return GetListQuery("select * from ArizaNedeniGrubu where Silindi=0", new { });
        }

        public ArizaNedeniGrubu Get(int Id)
        {
            return GetQuery("select * from ArizaNedeniGrubu where ArizaNedeniGrubuID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaNedeniGrubu arizanedenigrubu)
        {
            return AddQuery("insert into ArizaNedeniGrubu(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", arizanedenigrubu);
        }

        public int Update(ArizaNedeniGrubu arizanedenigrubu)
        {
            return UpdateQuery("update ArizaNedeniGrubu set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where ArizaNedeniGrubuID=@ArizaNedeniGrubuID", arizanedenigrubu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaNedeniGrubu where ArizaNedeniGrubuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaNedeniGrubu set Silindi = 1 where ArizaNedeniGrubuID=@Id", new { Id });
        }
    }
}
