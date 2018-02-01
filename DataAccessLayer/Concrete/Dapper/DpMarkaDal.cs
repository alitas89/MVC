using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpMarkaDal : DpEntityRepositoryBase<Marka>, IMarkaDal
    {
        public List<Marka> GetList()
        {
            return GetListQuery($"select * from Marka where Silindi=0", new { });
        }

        public Marka Get(int Id)
        {
            return GetQuery("select * from Marka where MarkaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Marka marka)
        {
            return AddQuery("insert into Marka(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", marka);
        }

        public int Update(Marka marka)
        {
            return UpdateQuery("update Marka set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where MarkaID=@MarkaID", marka);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Marka where MarkaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Marka set Silindi = 1 where MarkaID=@Id", new { Id });
        }
        public List<Marka> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Marka where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Marka {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}