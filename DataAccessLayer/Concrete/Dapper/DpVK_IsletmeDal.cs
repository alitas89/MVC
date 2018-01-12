using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVK_IsletmeDal : DpEntityRepositoryBase<VK_Isletme>, IIsletmeDal
    {
        public List<VK_Isletme> GetList()
        {
            return GetListQuery($"select * from Isletme where Silindi=0", new { });
        }

        public VK_Isletme Get(int Id)
        {
            return GetQuery("select * from Isletme where IsletmeID= @Id and Silindi=0", new { Id });
        }

        public int Add(VK_Isletme ısletme)
        {
            return AddQuery("insert Isletme(Kod,Ad,HaritaResmiYolu,Aciklama) values (@Kod,@Ad,@HaritaResmiYolu,@Aciklama)", ısletme);
        }

        public int Update(VK_Isletme ısletme)
        {
            return UpdateQuery("update Isletme set Kod=@Kod,Ad=@Ad,HaritaResmiYolu=@HaritaResmiYolu,Aciklama=@Aciklama where IsletmeID=@IsletmeID", ısletme);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Isletme where IsletmeID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Isletme set Silindi = 1 where IsletmeID=@Id", new { Id });
        }
    }
}