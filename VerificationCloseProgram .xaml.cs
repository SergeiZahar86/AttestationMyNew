using System.Windows;

namespace Attestation
{
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
