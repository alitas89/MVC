﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.DatabaseModel
{
    public class Test : IEntity
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public bool IsDeleted { get; set; }

        //[js]
        //public User User { get; set; }
       
    }
}