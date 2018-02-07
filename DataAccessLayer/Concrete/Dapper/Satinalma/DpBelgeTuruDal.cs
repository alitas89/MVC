using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpBelgeTuruDal : DpEntityRepositoryBase<BelgeTuru>, IBelgeTuruDal
    {
        public List<BelgeTuru> GetList()
        {
            return GetListQuery("select * from BelgeTuru where Silindi=0", new { });
        }

        public BelgeTuru Get(int Id)
        {
            return GetQuery("select * from BelgeTuru where BelgeTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(BelgeTuru belgeturu)
        {
            return AddQuery("insert into BelgeTuru(Kod,Ad,Aciklama,SatinalmaVarsayilan,IsEmriVarsayilan,MalzemeVarsayilan,VarlikKodu,Silindi) values (@Kod,@Ad,@Aciklama,@SatinalmaVarsayilan,@IsEmriVarsayilan,@MalzemeVarsayilan,@VarlikKodu,@Silindi)", belgeturu);
        }

        public int Update(BelgeTuru belgeturu)
        {
            return UpdateQuery("update BelgeTuru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,SatinalmaVarsayilan=@SatinalmaVarsayilan,IsEmriVarsayilan=@IsEmriVarsayilan,MalzemeVarsayilan=@MalzemeVarsayilan,VarlikKodu=@VarlikKodu,Silindi=@Silindi where BelgeTuruID=@BelgeTuruID", belgeturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BelgeTuru where BelgeTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BelgeTuru set Silindi = 1 where BelgeTuruID=@Id", new { Id });
        }

        public List<BelgeTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BelgeTuru where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BelgeTuru where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}