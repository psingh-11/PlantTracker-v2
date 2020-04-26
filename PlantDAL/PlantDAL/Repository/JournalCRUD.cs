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
        static Dictionary<int, string> ColumnNames = new Dictionary<int, string>()
        {
            {0, "ID"},
            {1,  "Name" },
            {2, "Date" }
        };


        public static void Insert(Journal journal)
        {
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.Journal.Add(journal);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                string mess = ex.Message;
            }

        }

        public static void Update(Journal journal)
        {
            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    ctx.Journal.Attach(journal);
                    ctx.Entry(journal).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        public static void Delete(Journal journal)
        {
            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                ctx.Journal.Attach(journal);
                ctx.Journal.Remove(journal);
                ctx.Entry(journal).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }


        public static Journal GetByID(Guid journalID)
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

            try
            {
                using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
                {
                    journals = ctx.Journal.Where(x => x.PlantID == plantID).ToList();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }


            return journals;
        }



        public static List<Journal> Search(string userID, string search, string orderby, string col,
            int startRec, int pageSize)
        {
            List<Journal> journals = new List<Journal>();
            string query = "SELECT * FROM JOURNAL WHERE UserID = '" + userID + "' ";

            if (!string.IsNullOrEmpty(search))
            {
                foreach (var srch in search.Split(','))
                {
                    string date = "", op ="";
                    var sch = srch.Trim();
                    query += " AND ( ";
                    var IsOp = IsOperator(sch, out date, out op);
                    if (IsOp && !string.IsNullOrEmpty(op))
                    {
                        DateTime dt;
                        DateTime.TryParse(date, out dt);
                        query += "[DateModified] " + op + " Convert( datetime, '" + dt.ToString("yyyy-MM-dd") + "' ) ) ";
                    }
                    else
                    {
                        query += "[Name] Like '%" + sch + "%' ) ";
                    }

                }

            }
            var colName = GetCloumnName(Convert.ToInt32(col));

            query += " ORDER BY " + colName + " " + orderby;

            query += " OFFSET " + startRec.ToString() + " ROWS ";
            query += " FETCH NEXT " + pageSize.ToString() + " ROWS ONLY ";

            using (PlantTrackerDBEntities ctx = new PlantTrackerDBEntities())
            {
                journals = ctx.Journal.SqlQuery(query).ToList();
            }

            return journals;
        }

        static bool IsOperator(string query, out string data,out string op)
        {
            if (query.IndexOf("<=") != -1)
            {
                var idx = query.IndexOf("<=");
                data = query.Substring(idx+2);
                op = query.Substring(0, idx+2);
                if (query.Length == 2)
                    return false;
                return true;
            }
            else if (query.IndexOf(">=") != -1)
            {
                var idx = query.IndexOf(">=");
                data = query.Substring(idx+2);
                op = query.Substring(0, idx+2);
                if (query.Length == 2)
                    return false;
                return true;
            }
            else if (query.IndexOf(">") != -1)
            {
                var idx = query.IndexOf(">");
                data = query.Substring(idx+1);
                op = query.Substring(0, idx+1);
                if (query.Length == 1)
                    return false;
                return true;
            }
            else if (query.IndexOf("<") != -1)
            {
                var idx = query.IndexOf("<");
                data = query.Substring(idx+1);
                op = query.Substring(0, idx+1);
                if (query.Length == 1)
                    return false;
                return true;
            }
            else if (query.IndexOf("=") != -1)
            {
                var idx = query.IndexOf("=");
                data = query.Substring(idx+1);
                op = query.Substring(0, idx+1);
                if (query.Length == 1)
                    return false;
                return true;
            }
            else
            {
                data = query;
                op = "";
                return false;
            }

        }

        static string GetCloumnName(int Idx)
        {
            string value = "";
            ColumnNames.TryGetValue(Idx, out value);
            return value;
        }

    }
}

