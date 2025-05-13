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
    /// Логика взаимодействия для Добавить_мастера.xaml
    /// </summary>
    public partial class Добавить_мастера : Window
    {
        public Добавить_мастера(Master selectedMaster)
        {
            InitializeComponent();
            
            ((addmasterVM)this.DataContext).SetClose(Close);
            ((addmasterVM)this.DataContext).SetMaster(selectedMaster);
        }
    }
}
