using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHOMHH
    {
        Entities db;

        public NHOMHH()

        {
            db = Entities.CreateEntities();
        }

        public DataLayer.NHOMHH getItem(int maNhom)
        {
            return db.NHOMHHs.FirstOrDefault(x => x.IDNhom == maNhom);
        }

        public List<DataLayer.NHOMHH> getAll()
        {
            return db.NHOMHHs.ToList();
        }
        public void add(DataLayer.NHOMHH nhom)
        {
            try
            {
                db.NHOMHHs.Add(nhom);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public void update(DataLayer.NHOMHH nhh)
        {
            DataLayer.NHOMHH _nhh = db.NHOMHHs.FirstOrDefault(x => x.IDNhom == nhh.IDNhom);
            _nhh.TenNhom = nhh.TenNhom;
            
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int idn)
        {
            DataLayer.NHOMHH _nhh = db.NHOMHHs.FirstOrDefault(x => x.IDNhom == idn);
            try
            {
                db.NHOMHHs.Remove(_nhh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
