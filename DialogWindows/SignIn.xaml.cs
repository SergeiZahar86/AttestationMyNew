using DSAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Attestation
{

    public class Roles // класс для парсинга json
    {
        public List<string> roles { get; set; }
    }


    public partial class SignIn : Window
    {
        string role;                                              // роль необходимая для входа
        private Global global;
        public static bool isCloseProgram;
        string password;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер
        DSAccessLib agent;
        string session;
        List<string> roles; // список ролей
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
            role = "ArmOtk";
            List<string> roles = new List<string>();
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
                Thread.Sleep(2000);
                JObject data = agent.getResult(session, 4000);
                // проверяем роль
                JToken list = data["data"]["roles"];
                List<string> rls = list.ToObject<List<string>>();
                foreach(string rl in rls)
                {
                    if(rl == role)
                    {
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
                    else
                    {
                        error.Text = ("У вас недостаточно прав");
                    }
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
