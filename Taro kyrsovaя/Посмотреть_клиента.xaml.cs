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
    /// Логика взаимодействия для WinList.xaml
    /// </summary>
    public partial class Посмотреть_клиента : Window
    {
        public Посмотреть_клиента()
        {
            InitializeComponent();
        }

        private void ClientEdit(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).CommandParameter as Client;
            if (client != null)
                new ДобавитьКлиента(client).Show();
        }
    }
}
