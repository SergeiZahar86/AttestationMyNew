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

namespace Attestation
{
    public partial class ExClose : Window
    {
        public ExClose()
        {
            InitializeComponent();
        }
        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        {
            this.Close();
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);
        }
        private void close_Click(object sender, RoutedEventArgs e) // закрыть программу
        {
            this.Close();
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);
        }
    }
}
