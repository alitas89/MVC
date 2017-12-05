using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class ConsumptionPlace : IEntity
    {
        public int ConsumptionPlaceID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public decimal TargetedBudget { get; set; }
        public int ShiftClassID { get; set; }
        public int InstitutionID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string LogoFileName { get; set; }
        public string Explanation { get; set; }
        public bool PurchasePlace { get; set; }
        public bool IsDeleted { get; set; }
    }
}
