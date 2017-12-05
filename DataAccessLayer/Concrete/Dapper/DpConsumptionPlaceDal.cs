using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpConsumptionPlaceDal : DpEntityRepositoryBase<ConsumptionPlace>, IConsumptionPlaceDal
    {
        public List<ConsumptionPlace> GetList()
        {
            return GetListQuery("select * from ConsumptionPlace", new { });
        }

        public ConsumptionPlace Get(int Id)
        {
            return GetQuery("select *  from ConsumptionPlace where ConsumptionPlaceID = @Id", new { Id = Id });
        }

        public int Add(ConsumptionPlace consumptionPlace)
        {
            return AddQuery(@"insert ConsumptionPlace
                            (Code,Name,Budget,TargetedBudget,ShiftClassID,InstitutionID,
                            Phone1,Phone2,FaxNo,Email,WebUrl,LogoFileName,Explanation,PurchasePlace,IsDeleted)
                            values 
                            (@Code,@Name,@Budget,@TargetedBudget,@ShiftClassID,@InstitutionID,@Phone1,@Phone2,@FaxNo,
                            @Email,@WebUrl,@LogoFileName,@Explanation,@PurchasePlace,@IsDeleted)",
                            consumptionPlace);
        }

        public int Update(ConsumptionPlace consumptionPlace)
        {
            return UpdateQuery(@"update ConsumptionPlace 
                                set Code=@Code,Name=@Name,Budget=@Budget,TargetedBudget=@TargetedBudget,
                                ShiftClassID=@ShiftClassID,InstitutionID=@InstitutionID,Phone1=@Phone1,Phone2=@Phone2,
                                FaxNo=@FaxNo,Email=@Email,WebUrl=@WebUrl,LogoFileName=@LogoFileName,Explanation=@Explanation,
                                PurchasePlace=@PurchasePlace,IsDeleted=@IsDeleted where ConsumptionPlaceID=@ConsumptionPlaceID", 
                consumptionPlace);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ConsumptionPlace where ConsumptionPlaceID=@ConsumptionPlaceID",
                new { ConsumptionPlaceID = Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ConsumptionPlace set IsDeleted = 1 where ConsumptionPlaceID=@ConsumptionPlaceID",
                new { ConsumptionPlaceID = Id });
        }
    }
}
