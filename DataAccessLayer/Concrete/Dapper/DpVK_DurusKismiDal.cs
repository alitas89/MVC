using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVK_DurusKismiDal : DpEntityRepositoryBase<VK_DurusKismi>, IDurusKismiDal
    {
        public List<VK_DurusKismi> GetList()
        {
            return GetListQuery($"select * from VK_DurusKismi where Silindi=0", new { });
        }

        public VK_DurusKismi Get(int Id)
        {
            return GetQuery("select * from VK_DurusKismi where DurusKismiID= @Id and Silindi=0", new { Id });
        }

        public int Add(VK_DurusKismi duruskismi)
        {
            return AddQuery("insert VK_DurusKismi(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", duruskismi);
        }

        public int Update(VK_DurusKismi duruskismi)
        {
            return UpdateQuery("update VK_DurusKismi set Kod=@Kod,Ad=@Ad,BakimDurusu=@BakimDurusu,Aciklama=@Aciklama where DurusKismiID=@DurusKismiID", duruskismi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VK_DurusKismi where DurusKismiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VK_DurusKismi set Silindi = 1 where DurusKismiID=@Id", new { Id });
        }
    }
}