using System.Windows;

namespace Attestation
{
    /// <summary>
    /// Логика взаимодействия для VerificationCloseProgram.xaml
    /// </summary>
    public partial class VerificationCloseProgram : Window
    {
        public VerificationCloseProgram()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            SignIn.isCloseProgram = true;
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
