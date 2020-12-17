using DSAccessAgentAPI;
using System;
using System.Windows;

namespace Attestation
{
    public partial class SignIn : Window
    {
        private Global global;
        public static bool isCloseProgram;
        string password;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер
        DSAccessAgent agent = DSAccessAgent.getInstance(); /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {
            LoginData data = agent.login("www", "www", 5000);
            string a = data.description;
            string[] rol = data.roles; // ArmRole
            foreach(string s in rol)
            {
                if(s == "ArmRole")
                {

                }
            }
            global.numberCard = global.getNumberCard();  // получение номера карты
            NewEmplId.Password = global.numberCard;
            if (NewEmplId.Password.Length > 0) // проверяем карту и если совпадает закрываем окно и входим в систему
            {
                try
                {
                    //throw new Exception();
                    global.user = global.getUser("", "", NewEmplId.Password); // Global.getUser (261)
                    if (global.user.Length > 0)
                    {
                        dispatcherTimer.Stop(); // остановить таймер
                        this.Close();
                    }
                }catch(Exception ass)
                {
                    error.Text = "Недействительная карта";
                }
            }
        }
        public SignIn()
        {
            //numberCard = "";

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
                global.user = global.getUser(global.Login, password, NewEmplId.Password); // Global.getUser (261)
                if (global.user.Length > 0)
                {
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();
                }
                else
                {
                    mistake.Text = "Логин или пароль введён неверно";
                }
            }
            catch(Exception aa)
            {
               
                mistake.Text = (aa.Message);
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
