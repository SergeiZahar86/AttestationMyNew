using System;
using System.Collections.Generic;
using System.Windows;

namespace Attestation
{
    public partial class changePassword : Window
    {
        public string Login;
        private Global global;

        private string oldPassword;
        private string newPassword;
        private string newPasswordRepead;
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            oldPassword = OldPassword.Password;
            newPassword = NewPassword.Password;
            newPasswordRepead = NewPasswordRepead.Password;
            if(oldPassword.Length > 0 && newPassword.Length > 0 &&
                newPasswordRepead.Length > 0 && newPassword == newPasswordRepead)
            {
                bool ret=false;
                try
                {
                    ret = global.changePass(global.Login, oldPassword, newPassword, null); // changePass() - Смена данных учетной записи 
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
