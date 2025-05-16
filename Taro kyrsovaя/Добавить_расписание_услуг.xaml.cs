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
    /// Логика взаимодействия для расписания_услуг.xaml
    /// </summary>
    public partial class Добавить_расписание_услуг : Window
    {
        public Добавить_расписание_услуг(Shedule shedule)
        {
            InitializeComponent();
            ((SheduleVM)this.DataContext).SetShedule(shedule);
            ((SheduleVM)this.DataContext).SetClose(Close);

        }
    }
}
