using DSAccess;
using System;
using System.Threading;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using Attestation.DialogWindows;

namespace Attestation
{
    public partial class changePassword : Window

    { 
        private string login;
        private string oldPassword;
        private string newPassword;
        public string LoginBool;
        private Global global;
        DSAccessLib agent;                                      // из библиотеки для авторизации DSAccess
        string nameComboItem;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер

        
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();
            agent = DSAccessLib.getInstance();
            if (agent.Init() == false)
            {

                DSAccessAgentWindow DSA = new DSAccessAgentWindow();
                DSA.ShowDialog();
                this.Close();
                Application.Current.Shutdown();
                Environment.Exit(0);
            }
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            login = Login.Text;
            oldPassword = OldPassword.Password;
            newPassword = NewPassword.Password;
            if(global.numberCard == null)
            {
                global.numberCard = "";
            }
            {
                if (NewPassword.Password == NewPassword1.Password)
                {
                    try
                    {
                        JObject data = agent.change(login, oldPassword, newPassword,   15000);
                        int code = int.Parse(data["code"].ToString());
                        if (code != 0)
                        {
                            /* ExClose exClose = new ExClose((data["data"]).ToString());
                             exClose.ShowDialog();*/
                            
                            result.Text = $"Ошибка обновления: код {code}, {data["data"]}";
                        }
                        else
                        {
                            this.Close();

                        }



                    }
                    catch (Exception ass)
                    {
                        //MessageBox.Show(ass.Message);
                        /*ExClose exClose = new ExClose(ass.ToString());
                        exClose.ShowDialog();*/
                        result.Text = ass.ToString();
                    }
                }
                else
                {
                    result.Text = "Две строки нового пароля должны совпадать";
                }
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop(); // остановить таймер
            this.Close();
        }

        private void choice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) // выбор операции
        {
            ComboBoxItem ComboItem = (ComboBoxItem)choice.SelectedItem;
            nameComboItem = ComboItem.Name;
            //string name1 = ComboItem.Content.ToString();
            if(nameComboItem == "pass")
            {
                Login.IsEnabled = true;
                OldPassword.IsEnabled = true;
                NewPassword.IsEnabled = true;
                NewPassword1.IsEnabled = true;
                
            }
            else
            {
                Login.IsEnabled = true;
                OldPassword.IsEnabled = true;
                NewPassword.Password = "";
                NewPassword1.Password = "";
                NewPassword.IsEnabled = false;
                NewPassword1.IsEnabled = false;
                
            }
        }
    }
}
