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
    public class DpMalzemeSeriNoDal : DpEntityRepositoryBase<MalzemeSeriNo>, IMalzemeSeriNoDal
    {
        public List<MalzemeSeriNo> GetList()
        {
            return GetListQuery("select * from MalzemeSeriNo where Silindi=0", new { });
        }

        public MalzemeSeriNo Get(int Id)
        {
            return GetQuery("select * from MalzemeSeriNo where SeriNoID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeSeriNo malzemeserino)
        {
            return AddQuery("insert into MalzemeSeriNo(SeriNo,OzelKod,MalzemeID,Aciklama,Silindi) values (@SeriNo,@OzelKod,@MalzemeID,@Aciklama,@Silindi)", malzemeserino);
        }

        public int Update(MalzemeSeriNo malzemeserino)
        {
            return UpdateQuery("update MalzemeSeriNo set SeriNo=@SeriNo,OzelKod=@OzelKod,MalzemeID=@MalzemeID,Aciklama=@Aciklama,Silindi=@Silindi where SeriNoID=@SeriNoID", malzemeserino);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeSeriNo where SeriNoID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeSeriNo set Silindi = 1 where SeriNoID=@Id", new { Id });
        }

        public List<MalzemeSeriNo> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM MalzemeSeriNo where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeSeriNo where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
