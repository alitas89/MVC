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
    public class DpRolDal : DpEntityRepositoryBase<Rol>, IRolDal
    {
        public List<Rol> GetList()
        {
            return GetListQuery($"select * from Rol", new { });
        }

        public Rol Get(int Id)
        {
            return GetQuery("select * from Rol where RolId= @Id", new { Id });
        }

        public int Add(Rol rol)
        {
            return AddQuery("insert Rol(Ad) values (@Ad)", rol);
        }

        public int Update(Rol rol)
        {
            return UpdateQuery("update Rol set Ad=@Ad where RolId=@RolId", rol);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Rol where RolId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Rol set Silindi = 1 where RolId=@Id", new { Id });
        }
    }
}
