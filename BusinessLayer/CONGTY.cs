using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CONGTY
    {
        Entities db;

        public CONGTY() 
        {
            db = Entities.CreateEntities();        
        }

        public DataLayer.CONGTY getItem(string maCTy)
        {
            return db.CONGTies.FirstOrDefault(x=>x.MaCTy==maCTy);
        }

        public List<DataLayer.CONGTY> getAll() 
        {
            return db.CONGTies.ToList();
        }
        public void add(DataLayer.CONGTY cty)
        {
            try
            {
                db.CONGTies.Add(cty);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
            
        }
        public void update(DataLayer.CONGTY cty)
        {
            DataLayer.CONGTY _cty = db.CONGTies.FirstOrDefault(x => x.MaCTy == cty.MaCTy);
            _cty.TenCTy = cty.TenCTy;
            _cty.SDT = cty.SDT;
            _cty.Email = cty.Email;
            _cty.Fax = cty.Fax;
            _cty.DiaChi = cty.DiaChi;
            _cty.Disabled = cty.Disabled;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string maCTy)
        {
            DataLayer.CONGTY _cty = db.CONGTies.FirstOrDefault(x => x.MaCTy == maCTy);
            _cty.Disabled = true;
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
