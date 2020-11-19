using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;

namespace Attestation
{
    public partial class changePassword : Window

    { 
        private string login;
        private string oldPassword;
        private string newPassword;
        public string LoginBool;
        private Global global;

        private static string numberCard;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер
        //ivate  System.Timers.Timer aTimer;

        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {
            numberCard = global.getNumberCard();
            NewEmplId.Text = numberCard;

        }
        
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();

            // Таймер для работы считывателя///
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            ///////////////////////////////////
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
           
            login = Login.Password;
            oldPassword = OldPassword.Password;
            newPassword = NewPassword.Password;
            if(login.Length > 0 && oldPassword.Length > 0)
            {
                bool ret=false;
                try
                {
                    ret = global.changePass(global.Login, oldPassword, newPassword, NewEmplId.Text); // changePass() - Смена данных учетной записи 
                } catch(DataProviderException ex)
                {
                    string t=ex.Message;
                }
                if (ret) 
                {
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();
                }
                else { result.Text = "Старый пароль введен неверно"; }
            }
            else
            {
                result.Text = "Новый или старый пароль введен некорректно";
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop(); // остановить таймер
            this.Close();
        }
    }
}
