using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
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
        public List<BeklemeIptalNedeni> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM BeklemeIptalNedeni where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BeklemeIptalNedeni {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}