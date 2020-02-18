using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantDAL.EDMX;

namespace PlantDAL.Repository
{
    public static class PlantCRUD
    {
        public static void Insert(Plant plant)
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                ctx.Plant.Add(plant);
                ctx.SaveChanges();
            }
        }

        public static void Update()
        {

        }

        public static void Delete()
        {

        }


        public static void GetByID()
        {

        }
    }
}
