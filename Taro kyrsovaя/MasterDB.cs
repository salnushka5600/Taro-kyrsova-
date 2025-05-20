using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{

    internal class MasterDB
    {
        DBconnection connection;

        private MasterDB(DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(Master master)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Master` Values (0, @Name, @Workexperience, @SurName );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("Name", master.Name));
                cmd.Parameters.Add(new MySqlParameter("Workexperience", master.Workexperience));
                cmd.Parameters.Add(new MySqlParameter("SurName", master.SurName));
                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        master.Id = id;
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

        internal List<Master> SelectAll()
        {
            List<Master> master = new List<Master>();
            if (connection == null)
                return master;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `name`, `workexperience`, `surname` from `Master` ");
                try
                {
                   
                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();
                  
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string name = string.Empty;
                        if (!dr.IsDBNull(1))
                            name = dr.GetString("name");
                        int workexperience = 0;
                        if (!dr.IsDBNull(2))
                            workexperience = dr.GetInt32("workexperience");
                        string surname = string.Empty;
                        if (!dr.IsDBNull(3))
                            surname = dr.GetString("surname");


                        

                        master.Add(new Master
                        {
                            Id = id,
                            Name = name,
                            Workexperience = workexperience,
                            SurName = surname,
                           
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return master;
        }

        internal List<Master> SelectAllWithSpec()
        {
            List<Master> master = new List<Master>();
            if (connection == null)
                return master;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT m.`id`,  m.`name`,  m.`workexperience`,  m.`surname`, s.Title from `Master-Specialization` ms RIGHT JOIN Specialization s ON ms.IDSpecialization = s.ID RIGHT JOIN Master m ON ms.IDMaster = m.ID ORDER BY m.ID");
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();
                    Master last = new();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        if (id != last.Id)
                        {
                            string name = string.Empty;
                            if (!dr.IsDBNull(1))
                                name = dr.GetString("name");
                            int workexperience = 0;
                            if (!dr.IsDBNull(2))
                                workexperience = dr.GetInt32("workexperience");
                            string surname = string.Empty;
                            if (!dr.IsDBNull(3))
                                surname = dr.GetString("surname");


                            last = new Master
                            {
                                Id = id,
                                Name = name,
                                Workexperience = workexperience,
                                SurName = surname,

                            };
                            last.Specializations.Add(new specialization { Title = dr.GetString("Title") });
                            master.Add(last);
                        }
                        else
                            last.Specializations.Add(new specialization { Title = dr.GetString("Title") });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return master;
        }
        //редактирование
        internal bool Update(Master edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Master` set `name`=@name, `workexperience`=@workexperience, `surname`=@surname where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("name", edit.Name));
                mc.Parameters.Add(new MySqlParameter("workexperience", edit.Workexperience));
                mc.Parameters.Add(new MySqlParameter("surname", edit.SurName));

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


        internal bool Remove(Master remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                try
                {
                    // Удаляем связанные записи из Master-Specialization
                    var deleteSpecializations = connection.CreateCommand(
                        $"DELETE FROM `Master-Specialization` WHERE `IDMaster` = {remove.Id}");
                    deleteSpecializations.ExecuteNonQuery();

                    // Удаляем самого Master
                    var deleteMaster = connection.CreateCommand(
                        $"DELETE FROM `Master` WHERE `ID` = {remove.Id}");
                    deleteMaster.ExecuteNonQuery();

                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.CloseConnection();
                }
            }

            return result;
        }

        static MasterDB db;
        public static MasterDB GetDb()
        {
            if (db == null)
                db = new MasterDB(DBconnection.GetDbConnection());
            return db;
        }
    }
}
