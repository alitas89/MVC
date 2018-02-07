using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpIsSektoruDal : DpEntityRepositoryBase<IsSektoru>, IIsSektoruDal
    {
        public List<IsSektoru> GetList()
        {
            return GetListQuery("select * from IsSektoru where Silindi=0", new { });
        }

        public IsSektoru Get(int Id)
        {
            return GetQuery("select * from IsSektoru where IsEmriID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsSektoru ıssektoru)
        {
            return AddQuery("insert into IsSektoru(Kod,Ad,Aciklama,VarsayilanDeger,Silindi) values (@Kod,@Ad,@Aciklama,@VarsayilanDeger,@Silindi)", ıssektoru);
        }

        public int Update(IsSektoru ıssektoru)
        {
            return UpdateQuery("update IsSektoru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,VarsayilanDeger=@VarsayilanDeger,Silindi=@Silindi where IsEmriID=@IsEmriID", ıssektoru);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsSektoru where IsEmriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsSektoru set Silindi = 1 where IsEmriID=@Id", new { Id });
        }

        public List<IsSektoru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM IsSektoru where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsSektoru where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}