﻿using DSAccess;
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

        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {

            // Подключение к очереди
          /*  while (agent.Init() == false)
            {
                //Console.WriteLine("[Init] " + agent.getLastError());
                Thread.Sleep(200);
            }
            */


            //numberCard = global.getNumberCard();
            //NewEmplId.Password = numberCard;

        }
        
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();

            // Таймер для работы считывателя///
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
            ///////////////////////////////////
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
            //global.numberCard = global.getNumberCard();  // получение номера карты
            if(global.numberCard == null)
            {
                global.numberCard = "";
            }

            //if (login.Length > 0 && oldPassword.Length > 0 &&  newPassword.Length >= 0))
            {
                if (NewPassword.Password == NewPassword1.Password)
                {
                    try
                    {
                        //session = agent.change(login, oldPassword, newPassword, 4000);
                        //Thread.Sleep(1000);
                        JObject data = agent.change(login, oldPassword, newPassword,   15000);
                        int code = int.Parse(data["code"].ToString());
                        if (code != 0)
                        {
                            ExClose exClose = new ExClose((data["data"]).ToString());
                            exClose.ShowDialog();
                        }
                        else
                        {
                            result.Text = $"{data["data"]}";
                            dispatcherTimer.Stop(); // остановить таймер
                            this.Close();

                        }



                    }
                    catch (Exception ass)
                    {
                        //MessageBox.Show(ass.Message);
                        ExClose exClose = new ExClose(ass.ToString());
                        exClose.ShowDialog();
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

        private void choice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
                if (dispatcherTimer.IsEnabled == true)
                {
                    dispatcherTimer.Stop();
                }
            }
            else
            {
                Login.IsEnabled = true;
                OldPassword.IsEnabled = true;
                NewPassword.Password = "";
                NewPassword1.Password = "";
                NewPassword.IsEnabled = false;
                NewPassword1.IsEnabled = false;
                if (dispatcherTimer.IsEnabled == false)
                {
                    dispatcherTimer.Start();
                }
            }
        }
    }
}
