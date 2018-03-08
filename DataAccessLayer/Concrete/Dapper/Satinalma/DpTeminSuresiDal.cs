using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpTeminSuresiDal : DpEntityRepositoryBase<TeminSuresi>, ITeminSuresiDal
    {
        public List<TeminSuresi> GetList()
        {
            return GetListQuery("select * from TeminSuresi where Silindi=0", new { });
        }

        public TeminSuresi Get(int Id)
        {
            return GetQuery("select * from TeminSuresi where TeminSuresiID= @Id and Silindi=0", new { Id });
        }

        public int Add(TeminSuresi teminsuresi)
        {
            return AddQuery("insert into TeminSuresi(Kod,Ad,Aciklama,SatinalmaVarsayilan,IsEmriVarsayilan,MalzemeVarsayilan,Silindi) values (@Kod,@Ad,@Aciklama,@SatinalmaVarsayilan,@IsEmriVarsayilan,@MalzemeVarsayilan,@Silindi)", teminsuresi);
        }

        public int Update(TeminSuresi teminsuresi)
        {
            return UpdateQuery("update TeminSuresi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,SatinalmaVarsayilan=@SatinalmaVarsayilan,IsEmriVarsayilan=@IsEmriVarsayilan,MalzemeVarsayilan=@MalzemeVarsayilan,Silindi=@Silindi where TeminSuresiID=@TeminSuresiID", teminsuresi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from TeminSuresi where TeminSuresiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update TeminSuresi set Silindi = 1 where TeminSuresiID=@Id", new { Id });
        }

        public List<TeminSuresi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM TeminSuresi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeminSuresi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}