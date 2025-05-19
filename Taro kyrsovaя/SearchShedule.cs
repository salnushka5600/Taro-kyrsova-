using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class SearchShedule
    {
        private readonly DBconnection myConnection;


        public List<Shedule> SearchShedules(string search)
        {
            List<Shedule> result = new();

            List<Master> masters = new();
            List<Service> services = new();
            List<Client> clients = new();

            string query = $"SELECT sh.ID AS 'sheduled', sh.IDClients, sh.IDService, sh.IDMaster, sh.Date, m.Name, m.Workexperience, m.SurName, c.Name, c.Email, c.Dateregistration, c.Age, s.Title, s.Description, s.Price, s.Sessionduration\r\nFROM Shedule sh \r\nJOIN Master m ON sh.IDMaster = m.ID \r\nJOIN Clients c ON sh.IDClients = c.ID \r\nJOIN Service s ON sh.IDService = s.ID\r\nWHERE m.SurName LIKE @search ";

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
                            var shedule = new Shedule();
                            shedule.Id = dr.GetInt32("sheduled");
                            shedule.IDClients = dr.GetInt32("IDClients");
                            shedule.IDService = dr.GetInt32("IDService");
                            shedule.IDMaster = dr.GetInt32("IDMaster");
                            shedule.Date = dr.GetDateTime("Date");
                           

                            // поиск объекта-автора в коллекции authors по ID
                            var master = masters.FirstOrDefault(s => s.Id == shedule.IDMaster);
                            if (master == null)
                            {
                                // создание автора, если его не было в коллекции
                                master = new Master();
                                master.Id = shedule.IDMaster;
                                master.Name = dr.GetString("name");
                                master.Workexperience = dr.GetInt32("workexperience");
                                master.SurName = dr.GetString("surname");
                               
                                // добавление автора в коллекцию
                                masters.Add(master);
                            }
                            // добавление книги автору
                            master.Shedules.Add(shedule);
                            // указание книге автора
                            shedule.Master = master;



                            var client = clients.FirstOrDefault(s => s.Id == shedule.IDClients);
                            if (client == null)
                            {
                                // создание автора, если его не было в коллекции
                                client = new Client();
                                client.Id = shedule.IDClients;
                                client.Name = dr.GetString("name");
                                client.Email = dr.GetString("email");
                                client.Dateregistration = dr.GetDateTime("dateregistration");
                                client.Age = dr.GetInt32("age");

                                // добавление автора в коллекцию
                                clients.Add(client);
                            }
                            // добавление книги автору
                            client.Shedules.Add(shedule);
                            // указание книге автора
                            shedule.Client = client;



                            var service = services.FirstOrDefault(s => s.Id == shedule.IDService);
                            if (service == null)
                            {
                                // создание автора, если его не было в коллекции
                                service = new Service();
                                service.Id = shedule.IDService;
                                service.Title = dr.GetString("title");
                                service.Description = dr.GetString("description");
                                service.Price = dr.GetInt32("price");
                                service.Sessionduration = dr.GetInt32("sessionduration");

                                // добавление автора в коллекцию
                                services.Add(service);
                            }
                            // добавление книги автору
                            service.Shedules.Add(shedule);
                            // указание книге автора
                            shedule.Service = service;

                            // добавление книги в результат запроса
                            result.Add(shedule);
                        }
                    }
                }
                myConnection.CloseConnection();
            }
            return result;

        }

        // синглтон start
        static SearchShedule table;
        private SearchShedule(DBconnection myConnection)
        {
            this.myConnection = myConnection;
        }
        public static SearchShedule GetTable()
        {
            if (table == null)
                table = new SearchShedule(DBconnection.GetDbConnection());
            return table;
        }
    }
}
