using System;
using System.Windows;

namespace Attestation
{
    public partial class MainWindow : Window
    {
        private Global global;
        SignIn signIn;
        public MainWindow()
        {
            InitializeComponent();
            gridDialog.Visibility = Visibility.Hidden;
            global = Global.getInstance();
            if (!global.ArmAttestation)
            {
                CloseButton.Visibility = Visibility.Hidden;
                MinButton.Visibility = Visibility.Hidden;
                passwordButton.Visibility = Visibility.Hidden;
                exitLoginButton.Visibility = Visibility.Hidden;
                label_fio.Content = global.user;
                label_login.Text = global.Login;
            }
        }
        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e) // начальная загрузка
        {
            //==========================================================================================================
            if (global.ArmAttestation)
            {
                signIn = new SignIn();
                signIn.Owner = Window.GetWindow(this);
                signIn.ShowDialog();
                label_fio.Content = Global.ShortName(global.user);
                label_login.Text = global.Login;
            }
            //==========================================================================================================


            if (global.user.Length > 0)
            {
                global.workAfterShutdown(); // восстановление после разрыва
            }
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
        }
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
        private void signIn_Click(object sender, RoutedEventArgs e) // кнопка авторизации
        {
            signIn = new SignIn();
            signIn.Owner = Window.GetWindow(this);
            signIn.ShowDialog();
            label_fio.Content = Global.ShortName(global.user);
            label_login.Text = global.Login;
        }
    }
}
