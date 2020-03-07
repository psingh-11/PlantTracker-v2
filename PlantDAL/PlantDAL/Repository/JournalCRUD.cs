using PlantDAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDAL.Repository
{
    public static class JournalCRUD
    {
        public static void Insert(Journal journal)
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                ctx.Journal.Add(journal);
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


        public static object GetByID(Guid journalID)
        {
            Journal journal = new Journal();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                journal = ctx.Journal.FirstOrDefault(x => x.ID == journalID);
            }

            return journal;
        }


        public static List<Journal> GetByPlantID(Guid plantID)
        {
            List<Journal> journals = new List<Journal>();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                journals = ctx.Journal.Where(x => x.PlantID == plantID).ToList();
            }

            return journals;
        }
    }
}
