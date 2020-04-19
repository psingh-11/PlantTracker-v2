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
        static Dictionary<int, string> ColumnNames = new Dictionary<int, string>()
        {
            {0, "ID"},
            {1,  "Name" },
            {2, "Type" },
            {3, "Species" },
            {4, "Count" },
            {5, "Description" },

        };

        public static void Insert(Plant plant)
        {
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.Plant.Add(plant);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }     
        }

        public static void Update(Plant plant)
        {
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.Plant.Attach(plant);
                    ctx.Entry(plant).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        public static void Delete()
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {

            }
        }


        public static Plant GetByID(Guid plantId)
        {
            Plant plant = new Plant();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                plant = ctx.Plant.FirstOrDefault(x => x.ID == plantId);
            }

            return plant;
        }


        public static List<Plant> GetByUserID(string userID)
        {
            List<Plant> plants = new List<Plant>();
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                plants = ctx.Plant.Where(x => x.UserID == userID).ToList();
            }

            return plants;
        }

        public static List<Plant> Search(string userID, string search, string orderby, string col, 
            int startRec , int pageSize)
        {
            List<Plant> plants = new List<Plant>();
            string query = "SELECT * FROM Plant WHERE UserID = '" + userID + "' ";

            if(!string.IsNullOrEmpty(search))
            {
                foreach (var srch in search.Split(','))
                {
                    var sch = srch.Trim();
                    query += " AND ( ";

                    if (IsOperator(sch))
                    {
                        query += "[Count] " + sch + " ) ";
                    }
                    else
                    {
                        query += "[Name] Like '%" + sch + "%'  OR ";
                        query += "[Type] Like '%" + sch + "%' OR ";
                        query += "[Genus] Like '%" + sch + "%'  OR ";
                        query += "[Species] Like '%" + sch + "%'  OR ";
                        query += "[SubSpecies] Like '%" + sch + "%' ) ";
                    }

                }

            }
            var colName = GetCloumnName(Convert.ToInt32(col));

            query += " ORDER BY " + colName + " " + orderby;

            query += " OFFSET " + startRec.ToString() + " ROWS ";
            query += " FETCH NEXT " + pageSize.ToString() + " ROWS ONLY ";

            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                plants = ctx.Plant.SqlQuery(query).ToList();
            }

            return plants;
        }




        static bool IsOperator(string query)
        {
            if (query.IndexOf("<=") != -1)
            {
                if (query.Length == 2)
                    return false;
                return true;
            }
            else if (query.IndexOf(">=") != -1)
            {
                if (query.Length == 2)
                    return false;
                return true;
            }
            else if (query.IndexOf(">") != -1)
            {
                if (query.Length == 1)
                    return false;
                return true;
            }
            else if (query.IndexOf("<") != -1)
            {
                if (query.Length == 1)
                    return false;
                return true;
            }
            else if (query.IndexOf("=") != -1)
            {
                if (query.Length == 1)
                    return false;
                return true;
            }

            else
                return false;

        }

        static string GetCloumnName(int Idx)
        {
            string value = "";
            ColumnNames.TryGetValue(Idx, out value);
            return value;
        }

    }
}
