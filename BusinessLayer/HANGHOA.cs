using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HANGHOA
    {
        Entities db;
        public HANGHOA() 
        { 
            db = Entities.CreateEntities();
        }
        public DataLayer.HANGHOA getItem(string barcode)
        {
            return db.HANGHOAs.FirstOrDefault(x=>x.Code == barcode);
        }
        public List<DataLayer.HANGHOA> getListByNhom(int idNhom) 
        {
            return db.HANGHOAs.Where(x=>x.IDNhom == idNhom).OrderBy(o=>o.NgayTao).ToList();
        }
        public DataLayer.HANGHOA add(DataLayer.HANGHOA hh)
        {
            try
            {
                db.HANGHOAs.Add(hh);
                db.SaveChanges();
                return hh;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: "+ex.Message);
            }
            
        }
        public DataLayer.HANGHOA update(DataLayer.HANGHOA hh)
        {
            DataLayer.HANGHOA _hh = db.HANGHOAs.FirstOrDefault(x => x.Code == hh.Code);
            _hh.TenHang = hh.TenHang;
            _hh.TenTat = hh.TenTat;
            _hh.DVT = hh.DVT;
            _hh.DonGia = hh.DonGia;
            _hh.MoTa = hh.MoTa;
            _hh.MaXX = hh.MaXX;
            _hh.MaNCC = hh.MaNCC;
            _hh.Disabled = hh.Disabled;
            try
            {
                db.SaveChanges();
                return hh;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string barcode)
        {
            DataLayer.HANGHOA _hh = db.HANGHOAs.FirstOrDefault(x => x.Code == barcode);
            _hh.Disabled = true;
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
