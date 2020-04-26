﻿using PlantDAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDAL.Repository
{
    public static class CustomValueCRUD
    {
        public static void Insert()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }

        public static void Update()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }

        public static void Delete(CustomValues cv)
        {
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.CustomValues.Attach(cv);
                    ctx.CustomValues.Remove(cv);
                    ctx.Entry(cv).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public static CustomValues GetByID(Guid? id)
        {
            CustomValues cv = new CustomValues();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                cv = ctx.CustomValues.FirstOrDefault(x => x.ID == id);
            }

            return cv;
        }
    }
}
