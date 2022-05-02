using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatingLog.Database;

namespace RatingLog
{
    public class RatingLogic
    {
        public static event Action AuthorizationSuccess;
        public static event Action AuthorizationFailure;

        private static RatingLogic instance;

        private RatingLogic()
        {

        }

        public static RatingLogic GetInstance()
        {
            if (instance == null)
                instance = new RatingLogic();
            return instance;
        }

        public void TryToAuth(string login, string password)
        {
            if (DBManip.HasUser(login, password))
                AuthorizationSuccess?.Invoke();
            else
                AuthorizationFailure?.Invoke();
        }


        public string[] GetGroups()
        {
            return DBManip.GetAllGroups();
        }

        public string[] GetAllDates(string group)
        {
            var dates = DBManip.GetAllDates(group);
            string[] result = new string[dates.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                result[i] = dates[i].ToString("dd.MM.yyyy");
                Trace.WriteLine("Date: " + result[i]);
            }
            return result;
        }

        public string[] GetNames(string group)
        {
            return DBManip.GetAllNames(group);
        }

        public int[] GetGrades(string name)
        {
            return DBManip.GetGrades(name);
        }
    }
}
