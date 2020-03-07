using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantDAL.EDMX;

namespace PlantDAL.Repository
{
    public static class ImageCRUD
    {
        public static void Insert(Images img)
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                ctx.Images.Add(img);
                ctx.SaveChanges();
            }
        }

        public static void Update()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }

        public static void Delete()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }


        public static Images GetByID(Guid imgID)
        {
            Images img = new Images();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }

            return img;
        }

        public static List<Images> GetByJournalID(Guid journalID)
        {
            List<Images> images = new List<Images>();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }

            return images;
        }

        public static List<Images> GetByPlantID(Guid plantID)
        {
            List<Images> images = new List<Images>();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }

            return images;
        }
    }
}
