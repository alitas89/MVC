using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpModelDal : DpEntityRepositoryBase<Model>, IModelDal
    {
        public List<Model> GetList()
        {
            return GetListQuery($"select * from Model where Silindi=0", new { });
        }

        public Model Get(int Id)
        {
            return GetQuery("select * from Model where ModelID= @Id and Silindi=0", new { Id });
        }

        public int Add(Model model)
        {
            return AddQuery("insert into Model(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", model);
        }

        public int Update(Model model)
        {
            return UpdateQuery("update Model set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where ModelID=@ModelID", model);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Model where ModelID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Model set Silindi = 1 where ModelID=@Id", new { Id });
        }

        public List<ModelDto> GetListDto()
        {
            return new DpDtoRepositoryBase<ModelDto>().GetListDtoQuery("select M.*, MA.Ad as MarkaAd from Model M inner join Marka MA on MA.MarkaID = M.MarkaID", new { });
        }
        public List<Model> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Model where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Model {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}