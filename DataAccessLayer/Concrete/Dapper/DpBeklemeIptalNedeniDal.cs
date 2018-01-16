using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBeklemeIptalNedeniDal : DpEntityRepositoryBase<BeklemeIptalNedeni>, IBeklemeIptalNedeniDal
    {
        public List<BeklemeIptalNedeni> GetList()
        {
            return GetListQuery("select * from BeklemeIptalNedeni where Silindi=0", new { });
        }

        public BeklemeIptalNedeni Get(int Id)
        {
            return GetQuery("select * from BeklemeIptalNedeni where BeklemeIptalNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return AddQuery("insert into BeklemeIptalNedeni(Kod,Ad,Aciklama,IsEmriniKapsayanPeriyodikBakimOlustur,IptalEdilenOtonomBakimdanIsEmriOlustur,Silindi) values (@Kod,@Ad,@Aciklama,@IsEmriniKapsayanPeriyodikBakimOlustur,@IptalEdilenOtonomBakimdanIsEmriOlustur,@Silindi)", beklemeıptalnedeni);
        }

        public int Update(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return UpdateQuery("update BeklemeIptalNedeni set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,IsEmriniKapsayanPeriyodikBakimOlustur=@IsEmriniKapsayanPeriyodikBakimOlustur,IptalEdilenOtonomBakimdanIsEmriOlustur=@IptalEdilenOtonomBakimdanIsEmriOlustur,Silindi=@Silindi where BeklemeIptalNedeniID=@BeklemeIptalNedeniID", beklemeıptalnedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BeklemeIptalNedeni where BeklemeIptalNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BeklemeIptalNedeni set Silindi = 1 where BeklemeIptalNedeniID=@Id", new { Id });
        }
    }
}