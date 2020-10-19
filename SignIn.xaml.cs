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
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private Global global;
        public SignIn()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        { 
            
            try
            {
                string login = tbLogin.Text;
                string password = passwordBox.Password;
                global.user = global.getUser(login, password, null);
                //global.someValueString = global.getUser("oper", "oper", null);
           
                if (global.user.Length > 0)
                {
                    this.Close();
                }
                else
                {
                    mistake.Text = "Логин или пароль введён неверно";
                }
            }
            catch
            {
                mistake.Text = "Логин или пароль введён неверно";
            }
            


        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
    }
}
