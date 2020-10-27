using System.Windows;

namespace Attestation
{
    public partial class ContinuationOfAttestation : Window
    {
        public ContinuationOfAttestation()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e) // Закрыть программу
        {
            this.Close();
            Application.Current.Shutdown();             
        }
    }
}
