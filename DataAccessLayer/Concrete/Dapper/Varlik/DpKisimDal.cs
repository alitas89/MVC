using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpKisimDal : DpEntityRepositoryBase<Kisim>, IKisimDal
    {
        public List<Kisim> GetList()
        {
            return GetListQuery($"select * from Kisim where Silindi=0", new { });
        }

        public List<Kisim> GetList(int SarfYeriID)
        {
            return GetListQuery($"select * from Kisim where Silindi=0 and SarfYeriID=@SarfYeriID", new { SarfYeriID });
        }

        public Kisim Get(int Id)
        {
            return GetQuery("select * from Kisim where KisimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Kisim kisim)
        {
            return AddQuery("insert into Kisim(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,SarfYeriID,Aciklama) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@SarfYeriID,@Aciklama)", kisim);
        }

        public int Update(Kisim kisim)
        {
            return UpdateQuery("update Kisim set Kod=@Kod,Ad=@Ad,Butce=@Butce,HedeflenenButce=@HedeflenenButce,VardiyaSinifID=@VardiyaSinifID,SarfYeriID=@SarfYeriID,Aciklama=@Aciklama where KisimID=@KisimID", kisim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kisim where KisimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kisim set Silindi = 1 where KisimID=@Id", new { Id });
        }

        public List<KisimDto> GetListDto()
        {
            return new DpDtoRepositoryBase<KisimDto>().GetListDtoQuery("select KM.*, SY.Ad as SarfYeriAd from kisim KM inner join SarfYeri SY on KM.SarfYeriID = SY.SarfYeriID where KM.Silindi=0", new { });
        }

        public List<Kisim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Kisim where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public List<KisimDto> GetListPaginationDto(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                if (pagingParams.filterCol.Equals("SarfYeriAd"))
                {
                    filterQuery = $"and SY.{pagingParams.filterCol} like @filterVal";
                }
                else
                {
                    filterQuery = $"and KM.{pagingParams.filterCol} like @filterVal";
                }
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                if (arrOrder[0].ToString().Equals("SarfYeriAd"))
                {
                    orderQuery = $"ORDER BY SY.{arrOrder[0]} {arrOrder[1]}";
                }
                else
                {
                    orderQuery = $"ORDER BY KM.{arrOrder[0]} {arrOrder[1]}";
                }
            }

            return new DpDtoRepositoryBase<KisimDto>().GetListDtoQuery($@"select KM.*, SY.Ad as SarfYeriAd from kisim KM inner join SarfYeri SY on KM.SarfYeriID = SY.SarfYeriID where KM.Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Kisim where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}