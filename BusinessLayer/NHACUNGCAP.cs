using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHACUNGCAP
    {
        Entities db;
        public NHACUNGCAP()
        {
            db =Entities.CreateEntities();
        }
        public DataLayer.NHACUNGCAP getItem(int maNCC)
        {
            return db.NHACUNGCAPs.FirstOrDefault(x => x.MaNCC == maNCC);
        }
        public List<DataLayer.NHACUNGCAP> getAll() 
        {
            return db.NHACUNGCAPs.ToList();
        }
        public void add(DataLayer.NHACUNGCAP ncc)
        {
            try
            {
                db.NHACUNGCAPs.Add(ncc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public void update(DataLayer.NHACUNGCAP ncc)
        {
            DataLayer.NHACUNGCAP _ncc = db.NHACUNGCAPs.FirstOrDefault(x => x.MaNCC == ncc.MaNCC);
            _ncc.TenNCC = ncc.TenNCC;
            _ncc.SDT = ncc.SDT;
            _ncc.Email = ncc.Email;
            _ncc.Fax = ncc.Fax;
            _ncc.DiaChi = ncc.DiaChi;
            _ncc.Disabled = ncc.Disabled;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int mancc)
        {
            DataLayer.NHACUNGCAP _ncc = db.NHACUNGCAPs.FirstOrDefault(x => x.MaNCC == mancc);
            _ncc.Disabled = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
