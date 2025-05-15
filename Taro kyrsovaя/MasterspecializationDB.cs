using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{
    internal class MasterspecializationDB
    {
        DBconnection connection;

        private MasterspecializationDB (DBconnection db)
        {
            this.connection = db;
        }

        public bool Insert(Masterspecialization masterspecialization)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Master-Specialization` Values (0, @IDMaster, @IDSpecialization );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("IDMaster", masterspecialization.IdMaster));
                cmd.Parameters.Add(new MySqlParameter("IDSpecialization", masterspecialization.Idspecialization));
              

                try
                {
                    // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                    // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        // назначаем полученный id обратно в объект для дальнейшей работы
                        masterspecialization.Id = id;
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

        internal List<Masterspecialization> SelectByMaster(int master)
        {
            List<Masterspecialization> masterspecializations = new List<Masterspecialization>();
            if (connection == null)
                return masterspecializations;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `idmaster`, `IDSpecialization` from `Master-Specialization` where IDMaster = " + master);
                try
                {

                    MySqlDataReader dr = (MySqlDataReader)command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int idmaster = 0;
                        if (!dr.IsDBNull(1))
                            idmaster = dr.GetInt32("idmaster");
                        int idspecialization = 0;
                        if (!dr.IsDBNull(2))
                            idspecialization = dr.GetInt32("IDSpecialization");
                      




                        masterspecializations.Add(new Masterspecialization
                        {
                            Id = id,
                            IdMaster = idmaster,
                            Idspecialization = idspecialization,
                           


                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return masterspecializations;
        }

        internal bool Update(Masterspecialization edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Master-Specialization` set `idmaster`=@idmaster, `IDSpecialization`=@idspecialization where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("idmaster", edit.IdMaster));
                mc.Parameters.Add(new MySqlParameter("idservice", edit.Idspecialization));
                


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


        internal bool RemoveByMaster(int master)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Master-Specialization` where `IDMaster` = {master}");
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

        static MasterspecializationDB db;
        public static MasterspecializationDB GetDb()
        {
            if (db == null)
                db = new MasterspecializationDB(DBconnection.GetDbConnection());
            return db;
        }
    }
}
