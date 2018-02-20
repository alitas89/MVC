﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikDurumuDal : DpEntityRepositoryBase<VarlikDurumu>, IVarlikDurumuDal
    {
        public List<VarlikDurumu> GetList()
        {
            return GetListQuery($"select * from VarlikDurumu where Silindi=0", new { });
        }

        public VarlikDurumu Get(int Id)
        {
            return GetQuery("select * from VarlikDurumu where VarlikDurumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikDurumu varlikdurumu)
        {
            return AddQuery("insert into VarlikDurumu(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", varlikdurumu);
        }

        public int Update(VarlikDurumu varlikdurumu)
        {
            return UpdateQuery("update VarlikDurumu set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where VarlikDurumuID=@VarlikDurumuID", varlikdurumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikDurumu where VarlikDurumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikDurumu set Silindi = 1 where VarlikDurumuID=@Id", new { Id });
        }
        public List<VarlikDurumu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VarlikDurumu where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikDurumu where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}