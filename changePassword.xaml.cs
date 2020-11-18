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
        private string oldPassword;
        private string newPassword;
        private string newPasswordRepead;
        public string Login;
        private Global global;

        private static string numberCard;
        private  System.Timers.Timer aTimer;

        private void OnTimedEvent(Object source, EventArgs e)
        {
            numberCard = global.getNumberCard();
            NewEmplId.Text = numberCard;
        }
        
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
           
            oldPassword = OldPassword.Password;
            newPassword = NewPassword.Password;
            newPasswordRepead = NewPasswordRepead.Password;
            if(oldPassword.Length > 0 && newPassword.Length > 0 &&
                newPasswordRepead.Length > 0 && newPassword != newPasswordRepead)
            {
                bool ret=false;
                try
                {
                    ret = global.changePass(global.Login, oldPassword, newPassword, NewEmplId.Text); // changePass() - Смена данных учетной записи 
                } catch(DataProviderException ex)
                {
                    string t=ex.Message;
                }
                  if(ret) this.Close();
                     else result.Text = "Старый пароль введен неверно";
            }
            else
            {
                result.Text = "Новый или старый пароль введен некорректно";
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
