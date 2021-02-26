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

namespace Attestation.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для Waiting_Download_data.xaml
    /// </summary>
    public partial class Waiting_Download_data : Window
    {
        public Waiting_Download_data()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }
}
