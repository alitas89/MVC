using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimRiskiDal : DpEntityRepositoryBase<BakimRiski>, IBakimRiskiDal
    {
        public List<BakimRiski> GetList()
        {
            return GetListQuery("select * from BakimRiski where Silindi=0", new { });
        }

        public BakimRiski Get(int Id)
        {
            return GetQuery("select * from BakimRiski where BakimRiskiID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimRiski bakimriski)
        {
            return AddQuery("insert into BakimRiski(RiskTipiID,Kod,Ad,Formulu,StokNo,Telefon,Aciklama1,Aciklama2,Aciklama3,Silindi) values (@RiskTipiID,@Kod,@Ad,@Formulu,@StokNo,@Telefon,@Aciklama1,@Aciklama2,@Aciklama3,@Silindi)", bakimriski);
        }

        public int Update(BakimRiski bakimriski)
        {
            return UpdateQuery("update BakimRiski set RiskTipiID=@RiskTipiID,Kod=@Kod,Ad=@Ad,Formulu=@Formulu,StokNo=@StokNo,Telefon=@Telefon,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3,Silindi=@Silindi where BakimRiskiID=@BakimRiskiID", bakimriski);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimRiski where BakimRiskiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimRiski set Silindi = 1 where BakimRiskiID=@Id", new { Id });
        }

        public List<BakimRiski> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BakimRiski where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimRiski where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}