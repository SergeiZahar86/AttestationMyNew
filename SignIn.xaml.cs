using System;
using System.Windows;

namespace Attestation
{
    public partial class SignIn : Window
    {
        private Global global;
        public static bool isCloseProgram;
        private static string numberCard;
        string password;
        System.Windows.Threading.DispatcherTimer dispatcherTimer; // Таймер

        private void OnTimedEvent(Object source, EventArgs e) // Получение номера карты
        {
            numberCard = global.getNumberCard();
            NewEmplId.Text = numberCard;
            if (NewEmplId.Text.Length > 0) // проверяем карту и если совпадает закрываем окно и входим в систему
            {
                global.user = global.getUser("", "", NewEmplId.Text); // Global.getUser (261)
                if (global.user.Length > 0)
                {
                    dispatcherTimer.Stop(); // остановить таймер
                    this.Close();
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
                global.user = global.getUser(global.Login, password, NewEmplId.Text); // Global.getUser (261)
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
            }

        }
    }
}
