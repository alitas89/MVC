using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
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
            return AddQuery("insert into DurusKismi(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", duruskismi);
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
        public List<DurusKismi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM DurusKismi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM DurusKismi {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}