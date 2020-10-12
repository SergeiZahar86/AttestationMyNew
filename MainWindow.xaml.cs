using Attestation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Attestation
{
    // Главный класс приложения
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка события кнопок
        private void CloseButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            DialogLogin dialog = new DialogLogin();
            dialog.ShowDialog();
            bool? ret = dialog.DialogResult;
            if (ret == true)
            {
                string login = dialog.Login;

            }
        }
        private void MinButton_Click(object sender, RoutedEventArgs e) =>  this.WindowState = WindowState.Minimized;


        private void Attestation_Click(object sender, MouseButtonEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush= (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            //BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#00CC00");
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
            

        }
        private void Report_Click(object sender, MouseButtonEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            
            ReportPage p = new ReportPage();
            MainFrame.Navigate(p);
            user.Text = "Ok";
        }

        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
        }
    }
}
