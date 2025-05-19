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
    /// Логика взаимодействия для Просмотр_расписания_услуг.xaml
    /// </summary>
    public partial class Просмотр_расписания_услуг : Window
    {
        public Просмотр_расписания_услуг()
        {
            InitializeComponent();
        }
        private void SheduleEdit(object sender, RoutedEventArgs e)
        {
            var shedule = (sender as Button).CommandParameter as Shedule;
            if (shedule != null)
            {
                new Добавить_расписание_услуг(shedule).ShowDialog();
                var vm = DataContext as TaroMain;
                vm.SearchShedules(vm.Searchs); 
            }
            
        }
    }
}
