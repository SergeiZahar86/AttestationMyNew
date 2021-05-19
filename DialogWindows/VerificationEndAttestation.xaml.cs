using System.Windows;

namespace Attestation
{
    public partial class VerificationEndAttestation : Window
    {
        public VerificationEndAttestation()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            AttestationPage.isVerification = true;
            this.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
    }
}
