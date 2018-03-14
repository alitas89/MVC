using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeAltGrupDal : DpEntityRepositoryBase<MalzemeAltGrup>, IMalzemeAltGrupDal
    {
        public List<MalzemeAltGrup> GetList()
        {
            return GetListQuery("select * from MalzemeAltGrup where Silindi=0", new { });
        }

        public MalzemeAltGrup Get(int Id)
        {
            return GetQuery("select * from MalzemeAltGrup where MalzemeAltGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeAltGrup malzemealtgrup)
        {
            return AddQuery("insert into MalzemeAltGrup(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", malzemealtgrup);
        }

        public int Update(MalzemeAltGrup malzemealtgrup)
        {
            return UpdateQuery("update MalzemeAltGrup set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where MalzemeAltGrupID=@MalzemeAltGrupID", malzemealtgrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeAltGrup where MalzemeAltGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeAltGrup set Silindi = 1 where MalzemeAltGrupID=@Id", new { Id });
        }

        public List<MalzemeAltGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM MalzemeAltGrup where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeAltGrup where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
