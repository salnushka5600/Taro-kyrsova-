using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Taro_kyrsovaя
{
   internal class DBconnection
    {
        MySqlConnection _connection;

        public void Config()
        {
            // пример строки подключения: "userid=student;password=student;database=Taro;server=192.168.200.13;characterset=utf8mb4";
            // конфигурация берется из файла / из окошка / из настроек / или статично
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.UserID = "student";
            sb.Password = "student";
            sb.Server = "95.154.107.102"; //"192.168.200.13"; "95.154.107.102"; 
            sb.Database = "Taro";
            sb.CharacterSet = "utf8mb4";

            // инициализация объекта для подключения к субд
            _connection = new MySqlConnection(sb.ToString());


        }

        public bool OpenConnection()
        {
            if (_connection == null)
                Config();
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        internal void CloseConnection()
        {
            if (_connection == null)
                return;

            try
            {
                _connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        internal MySqlCommand CreateCommand(string sql)
        {
            return new MySqlCommand(sql, _connection);
        }


        static DBconnection dbConnection;
        private DBconnection() { }
        public static DBconnection GetDbConnection()
        {
            if (dbConnection == null)
                dbConnection = new DBconnection();
            return dbConnection;
        }
    }
}
