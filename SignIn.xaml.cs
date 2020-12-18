using DSAccessAgentAPI;
using System;
using System.Threading;
using System.Windows;

namespace Attestation
{
    public partial class SignIn : Window
    {
        private Global global;
        public static bool isCloseProgram;
        string password;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер
        DSAccessAgent agent;                                      // из библиотеки для авторизации DSAccess
        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {
            /* LoginData data = agent.login("www", "www", 5000);
             string a = data.description;
             string[] rol = data.roles; // ArmRole
             foreach(string s in rol)
             {
                 if(s == "ArmRole")
                 {

                 }
             }*/
            // Подключение к очереди
            while (agent.Init() == false)
            {
                Console.WriteLine("[Init] " + agent.getLastError());
                Thread.Sleep(300);
            }
            global.numberCard = global.getNumberCard();  // получение номера карты
            NewEmplId.Password = global.numberCard;
            if (NewEmplId.Password.Length > 0) // проверяем карту и если совпадает закрываем окно и входим в систему
            {
                try
                {
                    LoginData data = agent.login(null, null, 500);

                    if (data.code != 0)
                    {
                        error.Text = $"{data.code.ToString()} {data.message}";
                    }
                    else
                    {
                        global.user = data.description;
                        dispatcherTimer.Stop(); // остановить таймер
                        this.Close();
                    }
                    /*global.user = global.getUser("", "", NewEmplId.Password); // Global.getUser (261)
                    if (global.user.Length > 0)
                    {
                        dispatcherTimer.Stop(); // остановить таймер
                        this.Close();
                    }*/
                }
                catch(Exception ass)
                {
                    error.Text = "Недействительная карта";
                }
            }
        }
        public SignIn()
        {
            //numberCard = "";

            agent = DSAccessAgent.getInstance();   // из библиотеки для авторизации DSAccess
            InitializeComponent();
            global = Global.getInstance();
            isCloseProgram = false;

            // Таймер для работы считывателя///
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            ///////////////////////////////////
        }
        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        {
            try
            {
                global.Login = tbLogin.Text;
                password = passwordBox.Password;
                LoginData data = agent.login(global.Login, password, 1000);
                //global.user = global.getUser(global.Login, password, NewEmplId.Password); // Global.getUser (261)
                if(data.code != 0)
                {

                    error.Text = $"{data.code.ToString()} {data.message}";
                }
                else if (data.user.Length > 0)
                {
                    global.user = data.user;
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();
                }
                else
                {
                    error.Text = "Логин или пароль введён неверно";
                }
            }
            catch(Exception aa)
            {

                error.Text = (aa.Message);
            }
        }
        private void close_Click(object sender, RoutedEventArgs e) // закрыть программу
        {
            isCloseProgram = false;
            VerificationCloseProgram closeProgram = new VerificationCloseProgram();
            closeProgram.ShowDialog();
            if (isCloseProgram)
            {
                dispatcherTimer.Stop(); // остановить таймер
                Application.Current.Shutdown();
                Environment.Exit(0);
            }

        }
    }
}
