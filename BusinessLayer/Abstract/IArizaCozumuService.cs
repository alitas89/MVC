using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IArizaCozumuService
    {
        List<ArizaCozumu> GetList();

        ArizaCozumu GetById(int id);

        int Add(ArizaCozumu arizacozumu);

        int Update(ArizaCozumu arizacozumu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}