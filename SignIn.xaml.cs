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
                string login = tbLogin.Text;
                string password = passwordBox.Password;
                global.user = global.getUser(login, password, null);
                //global.someValueString = global.getUser("oper", "oper", null);
           
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
        private void close_Click(object sender, RoutedEventArgs e)
        {
            isCloseProgram = false;
            // => Application.Current.Shutdown(); // закрыть программу

            VerificationCloseProgram closeProgram = new VerificationCloseProgram();
            closeProgram.ShowDialog();
            if (isCloseProgram)
            {
                Application.Current.Shutdown();
            }

        }
    }
}
