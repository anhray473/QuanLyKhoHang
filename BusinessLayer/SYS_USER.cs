using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_USER
    {
        Entities db;
        public SYS_USER() 
        { 
            db =  Entities.CreateEntities();
        
        }
        public tb_SYS_USER getItem(int idUser)
        {
            return db.tb_SYS_USER.FirstOrDefault(x=> x.IDUser == idUser);
        }
        public tb_SYS_USER getItem(string username, string macty, string madvi)
        {
            return db.tb_SYS_USER.FirstOrDefault(x => x.Username == username && x.MaCTy == macty && x.MaDVi == madvi);
        }
        public List<tb_SYS_USER> getAll()
        {
            return db.tb_SYS_USER.ToList();
        }
        public List<tb_SYS_USER> getUserByDVi(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x=> x.MaCTy == macty && x.MaDVi== madvi).ToList();
        }
        public List<tb_SYS_USER> getUserByDViFunc(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MaCTy == macty && x.MaDVi == madvi && x.Disabled==false).OrderByDescending(x=>x.Isgroup).ToList();
        }
        public bool checkUserExist(string macty, string madvi, string username)
        {
            var us = db.tb_SYS_USER.FirstOrDefault(x => x.MaCTy == macty && x.MaDVi == madvi && x.Username == username);
            if(us != null)
            {
                return true;
            }
            else
                return false;
        }
        public tb_SYS_USER add(tb_SYS_USER us)
        {
            try
            {
                db.tb_SYS_USER.Add(us);
                db.SaveChanges();
                return us;
            }
            catch(Exception ex)
            {
                throw new Exception("Lỗi " + ex.Message);
            }
        }
        public tb_SYS_USER update(tb_SYS_USER us)
        {
            var _us = db.tb_SYS_USER.FirstOrDefault(x=>x.IDUser== us.IDUser);
            _us.Username = us.Username;
            _us.HoVaTen = us.HoVaTen;
            _us.Isgroup = us.Isgroup;
            _us.Disabled = us.Disabled;
            _us.MaCTy = us.MaCTy;
            _us.MaDVi = us.MaDVi;
            _us.Password = us.Password;
            try
            {
                db.SaveChanges();
                return us;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi " + ex.Message);
            }
        }
    }
}
