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
        public List<tb_HANGHOA> getList()
        {
            return db.tb_HANGHOA.ToList();
        }
        public tb_HANGHOA getItem(string barcode)
        {
            return db.tb_HANGHOA.FirstOrDefault(x=>x.Code == barcode);
        }
        public List<tb_HANGHOA> getListByNhom(int idNhom) 
        {
            return db.tb_HANGHOA.Where(x=>x.IDNhom == idNhom).OrderBy(o=>o.NgayTao).ToList();
        }
        public List<tb_HANGHOA>getListByKeyword(string keyword)
        {
            return db.tb_HANGHOA.Where(ts=> ts.TenHang.Contains(keyword)).ToList();
        }
        public obj_HANGHOA getItemFull(string barcode)
        {
            // Tìm mặt hàng theo barcode
            var item = db.tb_HANGHOA.FirstOrDefault(x => x.Code == barcode);

            // Nếu không tìm thấy thì trả về null
            if (item == null)
            {
                return null;
            }

            // Tạo đối tượng obj_HANGHOA để trả về
            obj_HANGHOA hh = new obj_HANGHOA();

            // Gán thông tin cơ bản từ tb_HANGHOA
            hh.Code = item.Code;
            hh.TenHang = item.TenHang;
            hh.TenTat = item.TenTat;
            hh.MoTa = item.MoTa;
            hh.IDNhom = item.IDNhom;
            var n = db.tb_NHOMHH.FirstOrDefault(x => x.IDNhom == item.IDNhom);
            hh.TenNhom = n.TenNhom;
            hh.MaNCC = item.MaNCC;
            var c = db.tb_NHACUNGCAP.FirstOrDefault(x => x.MaNCC == item.MaNCC);
            hh.TenNCC = c.TenNCC;
            hh.MaXX = item.MaXX;
            var xx = db.tb_XUATXU.FirstOrDefault(x => x.ID == item.MaXX);
            hh.TenXX = xx.Ten;
            hh.DonGia = item.DonGia;
            hh.DVT = item.DVT;
            return hh;
        }
        public List<obj_HANGHOA> getListByNhomFull(int idNhom)
        {
            var lst = db.tb_HANGHOA.Where(x => x.IDNhom == idNhom).OrderBy(o => o.NgayTao).ToList();
            List<obj_HANGHOA> lstObj = new List<obj_HANGHOA>();
            obj_HANGHOA hh;
            foreach (var item in lst)
            {
                hh =  new obj_HANGHOA();
                hh.Code = item.Code;
                hh.TenHang = item.TenHang;
                hh.TenTat = item.TenTat;
                hh.MoTa = item.MoTa;
                hh.IDNhom = item.IDNhom;
                var n=db.tb_NHOMHH.FirstOrDefault(x=>x.IDNhom==item.IDNhom);
                hh.TenNhom = n.TenNhom;
                hh.MaNCC = item.MaNCC;
                var c = db.tb_NHACUNGCAP.FirstOrDefault(x => x.MaNCC == item.MaNCC);
                hh.TenNCC = c.TenNCC;
                hh.MaXX = item.MaXX;
                var xx = db.tb_XUATXU.FirstOrDefault(x => x.ID == item.MaXX);
                hh.TenXX = xx.Ten;
                hh.DonGia = item.DonGia;
                hh.DVT = item.DVT;
                lstObj.Add(hh);


            }    
            return lstObj;
        }
        public tb_HANGHOA add(tb_HANGHOA hh)
        {
            try
            {
                db.tb_HANGHOA.Add(hh);
                db.SaveChanges();
                return hh;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: "+ex.Message);
            }
            
        }
        public tb_HANGHOA update(tb_HANGHOA hh)
        {
            tb_HANGHOA _hh = db.tb_HANGHOA.FirstOrDefault(x => x.Code == hh.Code);
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
                return _hh;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string barcode)
        {
            tb_HANGHOA _hh = db.tb_HANGHOA.FirstOrDefault(x => x.Code == barcode);
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
        public bool checkExist(string barcode)
        {
            var h = db.tb_HANGHOA.FirstOrDefault(x=>x.Code == barcode);
            if (h != null)
            {
                return true;
            }else
                return false;
        }
        public List<obj_INBARCODE> getDanhMucInBarcode(int idNhom)
        {
            var lstDM = db.tb_HANGHOA.Where(x=>x.IDNhom== idNhom).ToList();
            List<obj_INBARCODE> lsInBarcode = new List<obj_INBARCODE>();
            obj_INBARCODE obj;
            foreach (var item in lstDM) 
            {
                obj = new obj_INBARCODE();
                obj.Code = item.Code;
                obj.TenHang = item.TenHang;
                obj.Tentat = item.TenTat;
                obj.DonGia = item.DonGia;
                obj.SoTem = null;
                lsInBarcode.Add(obj);
            }
            return lsInBarcode;
        }
    }
}
