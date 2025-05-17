using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{
    internal class SheduleDB
    {
        DBconnection connection;

        private SheduleDB(DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(Shedule shedule)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Shedule` Values (0, @IDClients, @IDService, @IDMaster, @Date );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("IDClients", shedule.IDClients));
                cmd.Parameters.Add(new MySqlParameter("IDService", shedule.IDService));
                cmd.Parameters.Add(new MySqlParameter("IDMaster", shedule.IDMaster));
                cmd.Parameters.Add(new MySqlParameter("Date", shedule.Date));
                
                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        shedule.Id = id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        internal List<Shedule> SelectAll()
        {
            List<Shedule> shedules = new List<Shedule>();
            if (connection == null)
                return shedules;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT sh.ID, sh.IDMaster, sh.IDService, sh.IDClients, sh.Date,  m.Name, m.Workexperience, m.SurName, s.Title, s.Description, s.Price, s.Sessionduration, c.Name, c.Email, c.Dateregistration, c.Age\r\nFROM `Shedule` sh\r\nJOIN Master m ON sh.IDMaster = m.ID \r\nJOIN Service s ON sh.IDService = s.ID \r\nJOIN Clients c ON sh.IDClients = c.ID  ");
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int idmaster = dr.GetInt32(1);
                        int idservice = dr.GetInt32(2);
                        int idclients = dr.GetInt32(3); 

                        DateTime date = new DateTime();
                        if (!dr.IsDBNull(4))
                            date = dr.GetDateTime("date");
                        string name = string.Empty;
                        if (!dr.IsDBNull(5))
                            name = dr.GetString(5);
                        int workexperience = 0;
                        if (!dr.IsDBNull(6))
                            workexperience = dr.GetInt32("workexperience");
                        string surname = string.Empty;
                        if (!dr.IsDBNull(7))
                            surname = dr.GetString("surname");
                        string title = string.Empty;
                        if (!dr.IsDBNull(8))
                            title = dr.GetString("title");
                        string description = string.Empty;
                        if (!dr.IsDBNull(9))
                            description = dr.GetString("description");
                        int price = 0;
                        if (!dr.IsDBNull(10))
                            price = dr.GetInt32("price");
                        int sessionduration = 0;
                        if (!dr.IsDBNull(11))
                            sessionduration = dr.GetInt32("sessionduration");
                        string nameclient = string.Empty;
                        if (!dr.IsDBNull(12))
                            nameclient = dr.GetString(12);
                        string email = string.Empty;
                        if (!dr.IsDBNull(13))
                            email = dr.GetString("email");
                        DateTime dateregistration = new DateTime();
                        if (!dr.IsDBNull(14))
                            dateregistration = dr.GetDateTime("dateregistration");
                        int age = 0;
                        if (!dr.IsDBNull(15))
                            age = dr.GetInt32("age");



                        
                        


                        Client client = new Client
                        {
                            Id = idclients,
                            Name = nameclient,
                            Age = age,
                            Dateregistration = dateregistration,
                            Email = email,
                        };



                        Master master = new Master
                        {
                            Id = idmaster,
                            Name = name,
                            Workexperience = workexperience,
                            SurName = surname,
                           
                        };


                        Service service = new Service
                        {
                            Id = idservice,
                            Title = title,
                            Description = description,
                           
                        };

                        shedules.Add(new Shedule
                        {
                            Id = id,
                            IDClients = idclients,
                            IDService = idservice,
                            IDMaster = idmaster,
                            Date = date,
                            Service = service,
                            Master = master,
                            Client = client,
                            
                           

                        
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return shedules;
        }

        internal bool Update(Shedule edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Shedule` set `idclients`=@idclients, `idservice`=@idservice, `idmaster`=@idmaster, `date`=@date where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("idclients", edit.IDClients));
                mc.Parameters.Add(new MySqlParameter("idservice", edit.IDService));
                mc.Parameters.Add(new MySqlParameter("idmaster", edit.IDMaster));
                mc.Parameters.Add(new MySqlParameter("date", edit.Date));
                

                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }


        internal bool Remove(Shedule remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Shedule` where `id` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        static SheduleDB db;
        public static SheduleDB GetDb()
        {
            if (db == null)
                db = new SheduleDB(DBconnection.GetDbConnection());
            return db;
        }
    }
}
