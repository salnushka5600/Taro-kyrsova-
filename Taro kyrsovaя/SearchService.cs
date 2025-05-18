using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class SearchService
    {
        private readonly DBconnection myConnection;


        public List<Service> SearchServices(string search)
        {
            List<Service> result = new();

            string query = $"SELECT s.ID AS 'serviced', s.Title, s.Description, s.Price, s.Sessionduration\r\nFROM Service s \r\nWHERE s.Description LIKE @search";

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
                            var service = new Service();
                            service.Id = dr.GetInt32("serviced");
                            service.Title = dr.GetString("Title");
                            service.Description = dr.GetString("Description");
                            service.Price = dr.GetInt32("Price");
                            service.Sessionduration = dr.GetInt32("Sessionduration");





                            // добавление книги в результат запроса
                            result.Add(service);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

        // синглтон start
        static SearchService table;
        private SearchService(DBconnection myConnection)
        {
            this.myConnection = myConnection;
        }
        public static SearchService GetTable()
        {
            if (table == null)
                table = new SearchService(DBconnection.GetDbConnection());
            return table;
        }
    }
}
