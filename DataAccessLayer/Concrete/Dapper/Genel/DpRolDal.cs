using System;
using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
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
            return AddQuery("insert into Rol(Ad) values (@Ad)", rol);
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

        public List<Rol> GetListPagination(PagingParams pagingParams)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string filter = "")
        {
            throw new NotImplementedException();
        }

        public List<Rol> GetRolByKullaniciId(int kullaniciId)
        {
            return GetListQuery(@"SELECT dbo.Rol.*
                FROM dbo.Kullanici INNER JOIN
                dbo.KullaniciRol ON dbo.Kullanici.KullaniciId = dbo.KullaniciRol.KullaniciId 
                INNER JOIN
                dbo.Rol ON dbo.KullaniciRol.RolId = dbo.Rol.RolId
                WHERE (dbo.Kullanici.KullaniciId = @kullaniciId)", new { kullaniciId });
        }
    }
}
