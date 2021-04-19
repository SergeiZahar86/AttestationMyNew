using Attestation.DialogWindows;
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
        int code; // код ответа от DSAccessAgent
        // из библиотеки для авторизации DSAccess
        string user;
        string pass;





        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {

            JObject data = null;
            try
            {
                 data = agent.login("", "", 3000);

            }
            catch(Exception ex)
            {
                error.Text = "step1, "+ex.ToString();
                return;
            }
            int code1 = int.Parse(data["code"].ToString());

            if (code1 != 0)
            {
                error.Text = $"Ошибка: код {code1}, {data["data"]}";
            }
            else
            {
                global.user = (string)data["data"]["description"];
                error.Text = $"Срок действия пароля {data["data"]["expiration"]}";
                dispatcherTimer.Stop(); // остановить таймер
                this.Close();
            }
            
        }
        public SignIn()
        {
            InitializeComponent();
            role = "ArmOtk";
            List<string> roles = new List<string>();
            session = null;
            agent = DSAccessLib.getInstance(); // из библиотеки для авторизации DSAccess
            global = Global.getInstance();
            isCloseProgram = false;

            // Таймер для работы считывателя///
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            ///////////////////////////////////
            ///

           
            if (agent.Init() == false)
            {

                DSAccessAgentWindow DSA = new DSAccessAgentWindow();
                DSA.ShowDialog();
                this.Close();
                Application.Current.Shutdown();
                Environment.Exit(0);
            }
            //MessageBox.Show("DSAccessAgent не запущен. Программа будет закрыта");


        }
        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        {

            JObject data = null;
            try
            {
                data = agent.login(tbLogin.Text, passwordBox.Password, 3000);

            }
            catch (Exception ex)
            {
                error.Text = "step1, " + ex.ToString();
                return;
            }
            try
            {
                global.Login = tbLogin.Text;
                password = passwordBox.Password;
                //session = agent.login(global.Login, password);
                //global.user = global.getUser(global.Login, password, NewEmplId.Password); // Global.getUser (261)
                Waiting_Sign_in waiting = new Waiting_Sign_in();
                waiting.Show();
                
                //JObject data = agent.getResult(session, 4000);
                code = int.Parse(data["code"].ToString());
                if (code == 0)
                {

                    // проверяем роль
                    JToken list = data["data"]["roles"];
                    List<string> rls = list.ToObject<List<string>>();
                    foreach (string rl in rls)
                    {
                        if (rl == role)
                        {
                            if (((string)data["data"]["description"]).Length > 0)
                            {
                                global.user = (string)data["data"]["description"];
                                error.Text = $"Срок действия пароля {data["data"]["expiration"]}";
                                dispatcherTimer.Stop(); // остановить таймер
                                this.Close();
                                break;
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
                else
                {
                    error.Text = $"Ошибка - {data["data"]} "; // ("[Ошибка] " + data["data"]);
                }
                waiting.Close();
            }
            catch (Exception aa)
            {

                error.Text = (aa.Message);
            }
            finally
            {

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
