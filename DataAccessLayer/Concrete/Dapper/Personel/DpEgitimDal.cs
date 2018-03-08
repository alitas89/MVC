using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpEgitimDal : DpEntityRepositoryBase<Egitim>, IEgitimDal
    {
        public List<Egitim> GetList()
        {
            return GetListQuery("select * from Egitim where Silindi=0", new { });
        }

        public Egitim Get(int Id)
        {
            return GetQuery("select * from Egitim where EgitimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Egitim egitim)
        {
            return AddQuery("insert into Egitim(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", egitim);
        }

        public int Update(Egitim egitim)
        {
            return UpdateQuery("update Egitim set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where EgitimID=@EgitimID", egitim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Egitim where EgitimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Egitim set Silindi = 1 where EgitimID=@Id", new { Id });
        }

        public List<Egitim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Egitim where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Egitim where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
