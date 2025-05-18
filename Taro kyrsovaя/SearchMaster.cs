using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class SearchMaster
    {
        private readonly DBconnection myConnection;


        public List<Master> SearchMasters(string search)
        {
            List<Master> result = new();

            string query = $"SELECT m.ID AS 'masterid', m.Name, m.Workexperience, m.SurName\r\nFROM Master m  \r\nWHERE m.SurName LIKE @search";

            if (myConnection.OpenConnection())
            {// using уничтожает объект после окончания блока (вызывает Dispose)
                using (var mc = myConnection.CreateCommand(query))
                {
                    // передача поиска через переменную в запрос
                    mc.Parameters.Add(new MySqlParameter("search", $"%{search}%"));
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // создание книги на каждую строку в результате
                            var master = new Master();
                            master.Id = dr.GetInt32("masterid");
                            master.Name = dr.GetString("Name");
                            master.Workexperience = dr.GetInt32("Workexperience");
                            master.SurName = dr.GetString("SurName");





                            // добавление книги в результат запроса
                            result.Add(master);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

        // синглтон start
        static SearchMaster table;
        private SearchMaster(DBconnection myConnection)
        {
            this.myConnection = myConnection;
        }
        public static SearchMaster GetTable()
        {
            if (table == null)
                table = new SearchMaster(DBconnection.GetDbConnection());
            return table;
        }
    }
}
