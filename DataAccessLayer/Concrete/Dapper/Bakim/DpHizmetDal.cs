using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpHizmetDal : DpEntityRepositoryBase<Hizmet>, IHizmetDal
    {
        public List<Hizmet> GetList()
        {
            return GetListQuery("select * from Hizmet where Silindi=0", new { });
        }

        public Hizmet Get(int Id)
        {
            return GetQuery("select * from Hizmet where HizmetID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hizmet hizmet)
        {
            return AddQuery("insert into Hizmet(Kod,Ad,BirimFiyat,ParaBirimID,Aciklama,Silindi) values (@Kod,@Ad,@BirimFiyat,@ParaBirimID,@Aciklama,@Silindi)", hizmet);
        }

        public int Update(Hizmet hizmet)
        {
            return UpdateQuery("update Hizmet set Kod=@Kod,Ad=@Ad,BirimFiyat=@BirimFiyat,ParaBirimID=@ParaBirimID,Aciklama=@Aciklama,Silindi=@Silindi where HizmetID=@HizmetID", hizmet);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hizmet where HizmetID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hizmet set Silindi = 1 where HizmetID=@Id", new { Id });
        }

        public List<Hizmet> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Hizmet where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Hizmet {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}