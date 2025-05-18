using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class Searchspecialization
    {
        private readonly DBconnection myConnection;


        public List<specialization> SearchSpecialization(string search)
        {
            List<specialization> result = new();

            string query = $"SELECT s.ID AS 'specializatid', s.Title, s.Description\r\nFROM Specialization s \r\nWHERE s.Title LIKE @search";

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
                            var specialization = new specialization();
                            specialization.Id = dr.GetInt32("specializatid");
                            specialization.Title = dr.GetString("Title");
                            specialization.Description = dr.GetString("Description");
                           




                            // добавление книги в результат запроса
                            result.Add(specialization);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

        // синглтон start
        static Searchspecialization table;
        private Searchspecialization(DBconnection myConnection)
        {
            this.myConnection = myConnection;
        }
        public static Searchspecialization GetTable()
        {
            if (table == null)
                table = new Searchspecialization(DBconnection.GetDbConnection());
            return table;
        }
    }
}
