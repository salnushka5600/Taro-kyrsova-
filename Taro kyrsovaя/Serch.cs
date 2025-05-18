using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class Serch
    {
        private readonly DBconnection myConnection;


        public List<Client> Search(string search)
        {
            List<Client> result = new();

            string query = $"SELECT c.ID AS 'cliented', c.Name, c.Email, c.Dateregistration, c.Age \r\nFROM Clients c \r\nWHERE c.Name LIKE @search";

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
                            var client = new Client();
                            client.Id = dr.GetInt32("cliented");
                            client.Name = dr.GetString("Name");
                            client.Email = dr.GetString("Email");
                            client.Dateregistration = dr.GetDateTime("Dateregistration");
                            client.Age = dr.GetInt32("Age");
                           

                           

                            // добавление книги в результат запроса
                            result.Add(client);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

        // синглтон start
        static Serch table;
        private Serch(DBconnection myConnection)
        {
            this.myConnection = myConnection;
        }
        public static Serch GetTable()
        {
            if (table == null)
                table = new Serch(DBconnection.GetDbConnection());
            return table;
        }


        
    }
}
