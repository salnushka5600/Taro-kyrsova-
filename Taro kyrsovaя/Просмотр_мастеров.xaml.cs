using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Taro_kyrsovaя
{
    /// <summary>
    /// Логика взаимодействия для Просмотр_мастеров.xaml
    /// </summary>
    public partial class Просмотр_мастеров : Window
    {
        public Просмотр_мастеров()
        {
            InitializeComponent();
           
        }
        private void MasterEdit(object sender, RoutedEventArgs e)
        {
            var master = (sender as Button).CommandParameter as Master;
            if (master != null) 
                new Добавить_мастера(master).Show();
        }
    }
}
