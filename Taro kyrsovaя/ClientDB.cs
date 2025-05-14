using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Taro_kyrsovaя
{
    internal class ClientDB
    {
        DBconnection connection;

        private ClientDB(DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(Client client)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Clients` Values (0, @Name, @Email, @Dateregistration, @Age );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Name", client.Name));
                cmd.Parameters.Add(new MySqlParameter("Email", client.Email));
                cmd.Parameters.Add(new MySqlParameter("Dateregistration", client.Dateregistration));
                cmd.Parameters.Add(new MySqlParameter("Age", client.Age));
                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        client.Id = id;
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

        internal List<Client> SelectAll()
        {
            List<Client> clients = new List<Client>();
            if (connection == null)
                return clients;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `name`, `email`, `dateregistration`, `age` from `Clients` ");
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string name = string.Empty;
                        if (!dr.IsDBNull(1))
                            name = dr.GetString("name");
                        string email = string.Empty;
                        if (!dr.IsDBNull(2))
                            email = dr.GetString("email");
                        string dateregistration = string.Empty;
                        if (!dr.IsDBNull(2))
                            dateregistration = dr.GetString("dateregistration");
                        int age = 0;
                        if (!dr.IsDBNull(2))
                            age = dr.GetInt32("age");



                        clients.Add(new Client
                        {
                            Id = id,
                            Name = name,
                            Email = email,
                            Dateregistration = dateregistration,
                            Age = age,

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return clients;
        }

        internal bool Update(Client edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Clients` set `name`=@name, `email`=@email, `dateregistration`=@dateregistration, `age`=@age where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("name", edit.  Name));
                mc.Parameters.Add(new MySqlParameter("email", edit.Email));
                mc.Parameters.Add(new MySqlParameter("dateregistration", edit.Dateregistration));
                mc.Parameters.Add(new MySqlParameter("age", edit.Age));

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


        internal bool Remove(Client remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Clients` where `id` = {remove.Id}");
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

        static ClientDB db;
        public static ClientDB GetDb()
        {
            if (db == null)
                db = new ClientDB(DBconnection.GetDbConnection());
            return db;
        }
    }
}
