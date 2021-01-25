using DSAccess;
using System;
using System.Threading;
using System.Windows;
using Newtonsoft.Json.Linq;


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
        string session;


        private static string numberCard;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер
        //ivate  System.Timers.Timer aTimer;

        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {

            // Подключение к очереди
            while (agent.Init() == false)
            {
                //Console.WriteLine("[Init] " + agent.getLastError());
                Thread.Sleep(200);
            }



            numberCard = global.getNumberCard();
            NewEmplId.Password = numberCard;

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
            agent = DSAccessLib.getInstance();
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {




            login = Login.Text;
            oldPassword = OldPassword.Password;
            newPassword = NewPassword.Password;
            global.numberCard = global.getNumberCard();  // получение номера карты
            if(global.numberCard == null)
            {
                global.numberCard = "";
            }

            if (login.Length > 0 && oldPassword.Length > 0 && (global.numberCard.Length > 0 || newPassword.Length >0))
            {
                try
                {
                    session = agent.change(login, oldPassword, newPassword, 4000);
                    Thread.Sleep(100);
                    JObject data = agent.getResult(session, 200);
                    result.Text = $"{data["data"]}";
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();



                    /*ChangeResult data = agent.change(login, oldPassword, newPassword, 1000);
                    if (data.code != 0)
                    {
                        result.Text = $"{data.code.ToString()} {data.message}";
                    }
                    else
                    {
                        result.Text = $"{data.message}";
                        dispatcherTimer.Stop(); // остановить таймер
                        this.Close();
                    }*/
                }
                catch(Exception ass)
                {
                    MessageBox.Show(ass.Message);
                }
            }
            else
            {
                result.Text = "Введите логин,старый пароль и новое значение пароля или карты, или все вместе";
            }


            /*if (login.Length > 0 && oldPassword.Length > 0)
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
            }*/
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop(); // остановить таймер
            this.Close();
        }
    }
}
