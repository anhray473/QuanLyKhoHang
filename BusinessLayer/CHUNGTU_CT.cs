using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CHUNGTU_CT
    {
        Entities db;
        public CHUNGTU_CT()
        {
            db = Entities.CreateEntities();
        }
        public tb_CHUNGTU_CT getItem(Guid idct)
        {
            return db.tb_CHUNGTU_CT.FirstOrDefault(x => x.IDCT == idct);

        }
        public List<tb_CHUNGTU_CT> getList(Guid id)
        {
            return db.tb_CHUNGTU_CT.Where(x => x.ID == id).ToList();
        }
        public List<obj_CHUNGTU_CT> getListByIDFull(Guid id)
        {
            var lst = db.tb_CHUNGTU_CT.Where(x => x.ID == id).ToList();
            List<obj_CHUNGTU_CT> lstCT = new List<obj_CHUNGTU_CT>();
            obj_CHUNGTU_CT obj;
            foreach (var item in lst)
            {
                obj = new obj_CHUNGTU_CT();
                obj.ID = item.ID;
                obj.IDCT = item.IDCT;
                obj.Code = item.CODE;
                var h = db.tb_HANGHOA.FirstOrDefault(x => x.Code == item.CODE);
                obj.TenHang = h.TenHang;
                obj.DVT = h.DVT;
                obj.SoLuong = item.SoLuong;
                obj.DonGia = item.DonGia;
                obj.ThanhTien = item.ThanhTien;
                obj.STT = item.STT;
                obj.Ngay = item.Ngay;
                lstCT.Add(obj);
            }
            return lstCT;
        }
        public tb_CHUNGTU_CT add(tb_CHUNGTU_CT chungtuct)
        {
            try
            {
                db.tb_CHUNGTU_CT.Add(chungtuct);
                db.SaveChanges();
                return chungtuct;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public tb_CHUNGTU_CT update(tb_CHUNGTU_CT chungtuct)
        {
            tb_CHUNGTU_CT _chungtuct = db.tb_CHUNGTU_CT.FirstOrDefault(x => x.IDCT == chungtuct.IDCT);
            _chungtuct.TenHang = chungtuct.TenHang;
            _chungtuct.TenTat = chungtuct.TenTat;
            _chungtuct.DVT = chungtuct.DVT;
            _chungtuct.DonGia = chungtuct.DonGia;
            _chungtuct.MoTa = chungtuct.MoTa;
            _chungtuct.MaXX = chungtuct.MaXX;
            _chungtuct.MaNCC = chungtuct.MaNCC;
            _chungtuct.Disabled = chungtuct.Disabled;
            try
            {
                db.SaveChanges();
                return _chungtuct;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string barcode)
        {
            tb_HANGHOA _chungtuct = db.tb_HANGHOA.FirstOrDefault(x => x.Code == barcode);
            _chungtuct.Disabled = true;
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
