using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{
   internal class specializationDB
    {
        DBconnection connection;

        private specializationDB(DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(specialization specialization)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Specialization` Values (0, @Title, @Description );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Title", specialization.Title));
                cmd.Parameters.Add(new MySqlParameter("Description", specialization.Description));
                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        specialization.Id = id;
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

        internal List<specialization> SelectAll()
        {
            List<specialization> specializations = new List<specialization>();
            if (connection == null)
                return specializations;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `title`, `description`, from `Specialization` ");
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
                        


                        specializations.Add(new specialization
                        {
                            Id = id,
                            Title = title,
                            Description = description,  

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return specializations;
        }

        internal bool Update(specialization edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Specialization` set `title`=@title, `description`=@description, where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("description", edit.Description));

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


        internal bool Remove(specialization remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Specialization` where `id` = {remove.Id}");
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

        static specializationDB db;
        public static specializationDB GetDb()
        {
            if (db == null)
                db = new specializationDB(DBconnection.GetDbConnection());                  
            return db;
        }
    }
}
