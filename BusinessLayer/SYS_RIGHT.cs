using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_RIGHT
    {
        Entities db;
        public SYS_RIGHT() { 
        db = Entities.CreateEntities();
        }
        public tb_SYS_RIGHT GetRIGHT(int idUser , string func_code)
        {
            return db.tb_SYS_RIGHT.FirstOrDefault(x => x.IDUser == idUser && x.FUNC_Code == func_code);
        }

        public void update(int idUser, string func_code, int right)
        {
            tb_SYS_RIGHT sright = db.tb_SYS_RIGHT.FirstOrDefault(x=> x.IDUser == idUser&&x.FUNC_Code ==func_code);
            try
            {
                sright.User_RIGHT = right;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi"+ex.Message);
            }
        }
    }
}
