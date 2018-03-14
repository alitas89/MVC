using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
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
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

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

            return GetListQuery($@"SELECT {columnsQuery} FROM VardiyaSinifi where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new {pagingParams.filter, pagingParams.offset, pagingParams.limit});
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VardiyaSinifi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
