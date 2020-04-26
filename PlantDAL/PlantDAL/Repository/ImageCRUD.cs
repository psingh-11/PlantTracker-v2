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
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.Images.Add(img);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                var err = ex.Message;
            }
        }

        public static void Update()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }

        public static void Delete(Images img)
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                ctx.Images.Attach(img);
                ctx.Images.Remove(img);
                ctx.Entry(img).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static Images GetByFilePathAndPlantId(string plantId, string filePath)
        {
            Images img = new Images();
            Guid pId = Guid.Parse(plantId);

            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    img = ctx.Images.FirstOrDefault(x => x.PlantID == pId && x.ImageFilePath == filePath);
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }

            return img;
        }

        public static Images GetByFilePathAndJournalId(string journalId, string filePath)
        {
            Images img = new Images();
            Guid jId = Guid.Parse(journalId);

            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    img = ctx.Images.FirstOrDefault(x => x.JournalID == jId && x.ImageFilePath == filePath);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return img;
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
                images = ctx.Images.Where(x => x.JournalID == journalID).ToList();
            }

            return images;
        }

        public static List<Images> GetByPlantID(Guid plantID)
        {
            List<Images> images = new List<Images>();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                images = ctx.Images.Where(x => x.PlantID == plantID).ToList();
            }

            return images;
        }
    }
}
