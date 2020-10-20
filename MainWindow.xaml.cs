using Attestation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Attestation
{
    // Главный класс приложения
    public partial class MainWindow : Window
    {
        private Global global;

        public MainWindow()
        {

            InitializeComponent();
            global = Global.getInstance();
        }

        // Обработка события кнопок
        private void CloseButton_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown(); // выход из программы

        private void changePassword_Click(object sender, RoutedEventArgs e) // изменение пароля
        {
            changePassword dialog = new changePassword();
            dialog.ShowDialog();
            bool? ret = dialog.DialogResult;
            if (ret == true)
            {
                string login = dialog.Login;

            }
        }
        private void MinButton_Click(object sender, RoutedEventArgs e) =>  this.WindowState = WindowState.Minimized;


        private void Attestation_Click(object sender, MouseButtonEventArgs e) // страница аттестации
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush= (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            //BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#00CC00");
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
            

        }
        private void Report_Click(object sender, MouseButtonEventArgs e) // страница отчетов
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");

            ReportPage p = new ReportPage();
            MainFrame.Navigate(p);
            user.Text = "Ok";
        }
        private void Archive_Click(object sender, MouseButtonEventArgs e) // страница архивов
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");

            Archive p = new Archive();
            MainFrame.Navigate(p);
        }

        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e) // начальная загрузка
        {
            GetSignIn();
            if (global.user.Length > 0)
            {
                 AttestationPage p = new AttestationPage();
                 MainFrame.Navigate(p);
                 global.isLoadAttestation = false;
            }
        }

        private void signIn_Click(object sender, RoutedEventArgs e) // кнопка авторизации
        {
            GetSignIn();
        }
        /*
        private void exitLoginButton_Click(object sender, RoutedEventArgs e)// кнопка выхода из учетной записи
        {
            SignIn exit = new SignIn();
            exit.ShowDialog();
        }
        */
        private void GetSignIn() // Авторизация
        {
            SignIn signIn = new SignIn();
            signIn.ShowDialog();
            try
            {
                label_fio.Content = Global.ShortName(global.user);
            }
            catch
            {
            }
        }

    }
}
