using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpArizaCozumuDal : DpEntityRepositoryBase<ArizaCozumu>, IArizaCozumuDal
    {
        public List<ArizaCozumu> GetList()
        {
            return GetListQuery("select * from ArizaCozumu where Silindi=0", new { });
        }

        public ArizaCozumu Get(int Id)
        {
            return GetQuery("select * from ArizaCozumu where ArizaCozumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaCozumu arizacozumu)
        {
            return AddQuery("insert into ArizaCozumu(Kod,Ad,TekNoktaEgitimiOlustur,Aciklama,Silindi) values (@Kod,@Ad,@TekNoktaEgitimiOlustur,@Aciklama,@Silindi)", arizacozumu);
        }

        public int Update(ArizaCozumu arizacozumu)
        {
            return UpdateQuery("update ArizaCozumu set Kod=@Kod,Ad=@Ad,TekNoktaEgitimiOlustur=@TekNoktaEgitimiOlustur,Aciklama=@Aciklama,Silindi=@Silindi where ArizaCozumuID=@ArizaCozumuID", arizacozumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaCozumu where ArizaCozumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaCozumu set Silindi = 1 where ArizaCozumuID=@Id", new { Id });
        }
        public List<ArizaCozumu> GetListPagination(PagingParams pagingParams)
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

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM ArizaCozumu where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaCozumu where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
