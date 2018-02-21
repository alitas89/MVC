using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikGrupDal : DpEntityRepositoryBase<VarlikGrup>, IVarlikGrupDal
    {
        public List<VarlikGrup> GetList()
        {
            return GetListQuery($"select * from VarlikGrup where Silindi=0", new { });
        }

        public VarlikGrup Get(int Id)
        {
            return GetQuery("select * from VarlikGrup where VarlikGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikGrup varlikgrup)
        {
            return AddQuery("insert into VarlikGrup(Kod,Ad,IsTipiID,Aciklama1,Aciklama2,Aciklama3) values (@Kod,@Ad,@IsTipiID,@Aciklama1,@Aciklama2,@Aciklama3)", varlikgrup);
        }

        public int Update(VarlikGrup varlikgrup)
        {
            return UpdateQuery("update VarlikGrup set Kod=@Kod,Ad=@Ad,IsTipiID=@IsTipiID,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3 where VarlikGrupID=@VarlikGrupID", varlikgrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikGrup where VarlikGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikGrup set Silindi = 1 where VarlikGrupID=@Id", new { Id });
        }

        public List<VarlikGrupDto> GetListDto()
        {
            return new DpDtoRepositoryBase<VarlikGrupDto>().GetListDtoQuery("select VG.*, IT.Ad as IsTipiAd from VarlikGrup VG left join IsTipi IT on IT.IsTipiID = VG.IsTipiID", new { });
        }

        public List<VarlikGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VarlikGrup where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikGrup where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikGrupDto>().GetListDtoQuery($@"SELECT * FROM View_VarlikGrupDto where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikGrupDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}