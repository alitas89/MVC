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
    public class DpVardiyaSinifiDal : DpEntityRepositoryBase<VardiyaSinifi>, IVardiyaSinifiDal
    {
        public List<VardiyaSinifi> GetList()
        {
            return GetListQuery("select * from VardiyaSinifi where Silindi=0", new { });
        }

        public VardiyaSinifi Get(int Id)
        {
            return GetQuery("select * from VardiyaSinifi where VardiyaSinifiID= @Id and Silindi=0", new {Id});
        }

        public int Add(VardiyaSinifi vardiyasinifi)
        {
            return AddQuery("insert into VardiyaSinifi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)",
                vardiyasinifi);
        }

        public int Update(VardiyaSinifi vardiyasinifi)
        {
            return UpdateQuery(
                "update VardiyaSinifi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where VardiyaSinifiID=@VardiyaSinifiID",
                vardiyasinifi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VardiyaSinifi where VardiyaSinifiID=@Id ", new {Id});
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VardiyaSinifi set Silindi = 1 where VardiyaSinifiID=@Id", new {Id});
        }

        public List<VardiyaSinifi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VardiyaSinifi where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new {pagingParams.filterVal, pagingParams.offset, pagingParams.limit});
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VardiyaSinifi {where}", new {filterVal}) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
