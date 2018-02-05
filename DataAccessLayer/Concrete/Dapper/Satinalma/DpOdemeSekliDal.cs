﻿using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpOdemeSekliDal : DpEntityRepositoryBase<OdemeSekli>, IOdemeSekliDal
    {
        public List<OdemeSekli> GetList()
        {
            return GetListQuery("select * from OdemeSekli where Silindi=0", new { });
        }

        public OdemeSekli Get(int Id)
        {
            return GetQuery("select * from OdemeSekli where OdemeSekliID= @Id and Silindi=0", new { Id });
        }

        public int Add(OdemeSekli odemesekli)
        {
            return AddQuery("insert into OdemeSekli(Kod,Ad,GunSayisi,Aciklama,VarsayilanDeger,Silindi) values (@Kod,@Ad,@GunSayisi,@Aciklama,@VarsayilanDeger,@Silindi)", odemesekli);
        }

        public int Update(OdemeSekli odemesekli)
        {
            return UpdateQuery("update OdemeSekli set Kod=@Kod,Ad=@Ad,GunSayisi=@GunSayisi,Aciklama=@Aciklama,VarsayilanDeger=@VarsayilanDeger,Silindi=@Silindi where OdemeSekliID=@OdemeSekliID", odemesekli);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from OdemeSekli where OdemeSekliID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update OdemeSekli set Silindi = 1 where OdemeSekliID=@Id", new { Id });
        }

        public List<OdemeSekli> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM OdemeSekli where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM OdemeSekli {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}