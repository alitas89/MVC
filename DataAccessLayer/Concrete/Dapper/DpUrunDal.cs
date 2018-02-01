using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpUrunDal : DpEntityRepositoryBase<Urun>, IUrunDal
    {
        public List<Urun> GetList()
        {
            return GetListQuery($"select * from Urun where Silindi=0", new { });
        }

        public Urun Get(int Id)
        {
            return GetQuery("select * from Urun where UrunID= @Id and Silindi=0", new { Id });
        }

        public int Add(Urun urun)
        {
            return AddQuery("insert into Urun(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", urun);
        }

        public int Update(Urun urun)
        {
            return UpdateQuery("update Urun set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where UrunID=@UrunID", urun);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Urun where UrunID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Urun set Silindi = 1 where UrunID=@Id", new { Id });
        }

        public List<Urun> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Urun where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Urun {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}