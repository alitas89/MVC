using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpGonderimFormatiDal : DpEntityRepositoryBase<GonderimFormati>, IGonderimFormatiDal
    {
        public List<GonderimFormati> GetList()
        {
            return GetListQuery("select * from GonderimFormati where Silindi=0", new { });
        }

        public GonderimFormati Get(int Id)
        {
            return GetQuery("select * from GonderimFormati where GonderimFormatiID= @Id and Silindi=0", new { Id });
        }

        public int Add(GonderimFormati gonderimformati)
        {
            return AddQuery("insert into GonderimFormati(GonderimTuruID,Kod,Ad,Konu,Format,Silindi) values (@GonderimTuruID,@Kod,@Ad,@Konu,@Format,@Silindi)", gonderimformati);
        }

        public int Update(GonderimFormati gonderimformati)
        {
            return UpdateQuery("update GonderimFormati set GonderimTuruID=@GonderimTuruID,Kod=@Kod,Ad=@Ad,Konu=@Konu,Format=@Format,Silindi=@Silindi where GonderimFormatiID=@GonderimFormatiID", gonderimformati);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GonderimFormati where GonderimFormatiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GonderimFormati set Silindi = 1 where GonderimFormatiID=@Id", new { Id });
        }
    }
}