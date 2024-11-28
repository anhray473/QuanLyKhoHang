﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VIEW_USER_NOTIN_GROUP
    {
        Entities db;
        public VIEW_USER_NOTIN_GROUP()
        {
            db = Entities.CreateEntities();
        }
        public List<V_USER_NOTIN_GROUP> getUserNotInGroup(string madvi, string macty)
        {
            return db.V_USER_NOTIN_GROUP.Where(x => x.MaDVi == madvi && x.MaCTy == macty && x.Isgroup == false && x.Disabled == false).ToList();
        }
    }
}
