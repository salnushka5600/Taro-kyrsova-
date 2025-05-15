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
                var command = connection.CreateCommand("select `id`, `idclients`, `idservice`, `idmaster`, `date` from `Shedule` ");
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string idclients = string.Empty;
                        if (!dr.IsDBNull(1))
                            idclients = dr.GetString("idclients");
                        string idservice = string.Empty;
                        if (!dr.IsDBNull(2))
                            idservice = dr.GetString("idservice");
                        string idmaster = string.Empty;
                        if (!dr.IsDBNull(2))
                            idmaster = dr.GetString("idmaster");
                        DateTime date = new DateTime();
                        if (!dr.IsDBNull(2))
                            date = dr.GetDateTime("date");
                        



                        shedules.Add(new Shedule
                        {
                            Id = id,
                            IDClients = idclients,
                            IDService = idservice,
                            IDMaster = idmaster,
                            Date = date,
                           

                        
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
