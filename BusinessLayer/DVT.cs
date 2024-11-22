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

        public tb_DVT getItem(int iddv)
        {
            return db.tb_DVT.FirstOrDefault(x => x.ID == iddv);
        }

        public List<tb_DVT> getAll()
        {
            return db.tb_DVT.ToList();
        }

        public List<tb_DVT> getAll(int iddv)
        {
            return db.tb_DVT.Where(x => x.ID == iddv).ToList();
        }
        public void add(tb_DVT dvt)
        {
            try
            {
                db.tb_DVT.Add(dvt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }

        public void update(tb_DVT dvt)
        {
            tb_DVT _dvt = db.tb_DVT.FirstOrDefault(x => x.ID == dvt.ID);
            _dvt.ID = dvt.ID;
            _dvt.Ten = dvt.Ten;
            try
            {
               
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int iddv)
        {
            tb_DVT _dvt = db.tb_DVT.FirstOrDefault(x => x.ID == iddv);
            try
            {
                db.tb_DVT.Remove(_dvt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
