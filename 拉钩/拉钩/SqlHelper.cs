using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Model;

namespace 拉钩
{
    public class SqlHelper
    {
        private static DbcontextDB db = new DbcontextDB();
        public static bool Insert(Lagou lagou, ref int id)
        {
            try
            {
                db.Save(lagou);
                id = lagou.Id;
                return true;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
        }

        public static void update(int id, string title, string context, string work)
        {
            db.Execute("update dbo.Lagou SET title='" + title + "',context='" + context + "',work='" + work + "' WHERE Id=" + id + "");
        }

        public static bool is_Insert(int positionId)
        {
            var lagou = db.FirstOrDefault<Lagou>("select * from Lagou where positionId=" + positionId + " ");
            return (lagou == null) ? true : false;
        }
    }
}
