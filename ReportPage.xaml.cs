using System;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Guid s = Guid.NewGuid();
            TextBlock.Text = s.ToString();
        }
    }
}
