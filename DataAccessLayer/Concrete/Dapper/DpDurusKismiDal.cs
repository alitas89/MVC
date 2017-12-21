using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpDurusKismiDal : DpEntityRepositoryBase<DurusKismi>, IDurusKismiDal
    {
        public List<DurusKismi> GetList()
        {
            return GetListQuery($"select * from DurusKismi where Silindi=0", new { });
        }

        public DurusKismi Get(int Id)
        {
            return GetQuery("select * from DurusKismi where DurusKismiID= @Id and Silindi=0", new { Id });
        }

        public int Add(DurusKismi duruskismi)
        {
            return AddQuery("insert DurusKismi(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", duruskismi);
        }

        public int Update(DurusKismi duruskismi)
        {
            return UpdateQuery("update DurusKismi set Kod=@Kod,Ad=@Ad,BakimDurusu=@BakimDurusu,Aciklama=@Aciklama where DurusKismiID=@DurusKismiID", duruskismi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from DurusKismi where DurusKismiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update DurusKismi set Silindi = 1 where DurusKismiID=@Id", new { Id });
        }
    }
}