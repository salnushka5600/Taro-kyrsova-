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
    /// Логика взаимодействия для Добавить_специализацию.xaml
    /// </summary>
    public partial class Добавить_специализацию : Window
    {
        public Добавить_специализацию(specialization selectedspecialization)
        {
            InitializeComponent();
            ((specializationVM)this.DataContext).SetClose(Close);
            ((specializationVM)this.DataContext).Setspecialization(selectedspecialization);
        }
    }
}
