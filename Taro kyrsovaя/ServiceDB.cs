using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{
   internal class ServiceDB 
    {
        DBconnection connection;

        private ServiceDB(DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(Service service)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Service` Values (0, @Title, @Description, @Price, @Sessionduration );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Title", service.Title));
                cmd.Parameters.Add(new MySqlParameter("Description", service.Description));
                cmd.Parameters.Add(new MySqlParameter("Price", service.Price));
                cmd.Parameters.Add(new MySqlParameter("Sessionduration", service.Sessionduration));
                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        service.Id = id;
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

        internal List<Service> SelectAll()
        {
            List<Service> services = new List<Service>();
            if (connection == null)
                return services;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `title`, `description`, `price`, `sessionduration` from `Services` ");
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("title");
                        string description = string.Empty;
                        if (!dr.IsDBNull(2))
                            description = dr.GetString("description");
                        string price = string.Empty;
                        if (!dr.IsDBNull(2))
                            price = dr.GetString("price");
                        string sessionduration = string.Empty;
                        if (!dr.IsDBNull(2))
                            sessionduration = dr.GetString("sessionduration");



                        services.Add(new Service
                        {
                            Id = id,
                            Title = title,
                            Description = description,
                            Price = price,
                            Sessionduration = sessionduration,

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return services;
        }

        internal bool Update(Service edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Service` set `title`=@title, `description`=@description, `price`=@price, `sessionduration`=@sessionduration where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("description", edit.Description));
                mc.Parameters.Add(new MySqlParameter("price", edit.Price));
                mc.Parameters.Add(new MySqlParameter("sessionduration", edit.Sessionduration));

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


        internal bool Remove(Service remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Service` where `id` = {remove.Id}");
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

        static ServiceDB db;
        public static ServiceDB GetDb()
        {
            if (db == null)
                db = new ServiceDB(DBconnection.GetDbConnection());
            return db;
        }
    }
}
