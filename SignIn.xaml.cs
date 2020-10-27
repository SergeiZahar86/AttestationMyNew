using System.Windows;

namespace Attestation
{
    public partial class SignIn : Window
    {
        private Global global;
        public static bool isCloseProgram;
        public SignIn()
        {
            InitializeComponent();
            global = Global.getInstance();
            isCloseProgram = false;
        }
        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        { 
            try
            {
                global.Login = tbLogin.Text;
                string password = passwordBox.Password;
                global.user = global.getUser(global.Login, password, ""); // Global.getUser (261)
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
        private void close_Click(object sender, RoutedEventArgs e) // закрыть программу
        {
            isCloseProgram = false;
            VerificationCloseProgram closeProgram = new VerificationCloseProgram();
            closeProgram.ShowDialog();
            if (isCloseProgram)
            {
                Application.Current.Shutdown();
            }

        }
    }
}
