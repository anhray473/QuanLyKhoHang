using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DVT
    {
        Entities db;
        public DVT()
        {
            db = Entities.CreateEntities();

        }
        public List<DataLayer.DVT> getList()
        {
           return db.DVTs.ToList();
        }
    }
}
