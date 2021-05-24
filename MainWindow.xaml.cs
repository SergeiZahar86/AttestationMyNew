using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Attestation
{
    public partial class MainWindow : Window
    {
        private Global global;
        SignIn signIn;
        public MainWindow()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e) // начальная загрузка
        {
            signIn = new SignIn();
            signIn.Owner = Window.GetWindow(this);
            signIn.ShowDialog();
            label_fio.Content = Global.ShortName(global.user);
            label_login.Content = global.Login;

            
            if (global.user.Length > 0)
            {
                global.workAfterShutdown(); // восстановление после разрыва
            }
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
        }
        // Обработка события кнопок
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        { 
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);    
        }
        private void changePassword_Click(object sender, RoutedEventArgs e) // изменение пароля
        {
            changePassword dialog = new changePassword();
            dialog.ShowDialog();
            bool? ret = dialog.DialogResult;
            if (ret == true)
            {
                string login = dialog.LoginBool;
            }
        }
        private void MinButton_Click(object sender, RoutedEventArgs e) // сворачивание окна
        { 
            this.WindowState = WindowState.Minimized;
        }
        private void Attestation_Click(object sender, MouseButtonEventArgs e) // страница аттестации
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
        }
        
        private void signIn_Click(object sender, RoutedEventArgs e) // кнопка авторизации
        {
            signIn = new SignIn();
            signIn.Owner = Window.GetWindow(this);
            signIn.ShowDialog();
        }
    }
}
