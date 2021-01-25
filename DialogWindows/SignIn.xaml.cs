using DSAccess;
using Newtonsoft.Json.Linq;
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
        DSAccessLib agent;
        string session;
        // из библиотеки для авторизации DSAccess
        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {
            while (agent.Init() == false)
            {
                Thread.Sleep(100);
            }
            global.numberCard = global.getNumberCard();  // получение номера карты
            NewEmplId.Password = global.numberCard;
            if (NewEmplId.Password.Length > 0) // проверяем карту и если совпадает закрываем окно и входим в систему
            {
                try
                {
                    session = agent.login(tbLogin.Text, passwordBox.Password);
                    JObject data = agent.getResult(session, 3000);
                    int code = int.Parse(data["code"].ToString());

                    if (code != 0)
                    {
                        error.Text = ("[Ошибка] " + data["data"]);
                    }
                    else
                    {
                        global.user = (string)data["data"]["description"];
                        error.Text = $"Срок действия пароля {data["data"]["expiration"]}";
                        dispatcherTimer.Stop(); // остановить таймер
                        this.Close();
                    }
                }
                catch(Exception ass)
                {
                    error.Text = "Недействительная карта";
                }
            }
        }
        public SignIn()
        {
            session = null;
            agent = DSAccessLib.getInstance(); // из библиотеки для авторизации DSAccess
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
                session = agent.login(global.Login, password);
                //global.user = global.getUser(global.Login, password, NewEmplId.Password); // Global.getUser (261)
                Thread.Sleep(1000);
                JObject data = agent.getResult(session, 3000);
                int code = int.Parse(data["code"].ToString());
                if (code != 0)
                {
                    error.Text = ("[Ошибка] " + data["data"]);
                }
                else if(((string)data["data"]["description"]).Length > 0)
                {
                    global.user = (string)data["data"]["description"];
                    error.Text = $"Срок действия пароля {data["data"]["expiration"]}";
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();
                }
                else
                {
                    error.Text = "Логин или пароль введён неверно";
                }
            }
            catch (Exception aa)
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
