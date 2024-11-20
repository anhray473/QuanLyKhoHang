﻿using DataLayer;
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

        public DataLayer.DVT getItem(int iddv)
        {
            return db.DVTs.FirstOrDefault(x => x.ID == iddv);
        }

        public List<DataLayer.DVT> getAll()
        {
            return db.DVTs.ToList();
        }

        public List<DataLayer.DVT> getAll(int iddv)
        {
            return db.DVTs.Where(x => x.ID == iddv).ToList();
        }
        public void add(DataLayer.DVT dvt)
        {
            try
            {
                db.DVTs.Add(dvt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }

        public void update(DataLayer.DVT dvt)
        {
            DataLayer.DVT _dvt = db.DVTs.FirstOrDefault(x => x.ID == dvt.ID);
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
            DataLayer.DVT _dvt = db.DVTs.FirstOrDefault(x => x.ID == iddv);
            try
            {
                db.DVTs.Remove(_dvt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
