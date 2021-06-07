using System.Windows;

namespace Attestation.DialogWindows
{
    public partial class VerificationEndTask : Window
    {
        private Global global;
        public VerificationEndTask()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            global.CloseTask = true;
            this.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
