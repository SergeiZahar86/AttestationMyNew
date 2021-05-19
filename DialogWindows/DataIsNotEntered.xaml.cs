using System.Windows;

namespace Attestation.DialogWindows
{
    public partial class DataIsNotEntered : Window
    {
        public DataIsNotEntered()
        {
            InitializeComponent();
        }
        public DataIsNotEntered(int count)
        {
            InitializeComponent();
            textBlock.Text = $"Не введены все необходимые данные по вагону № {count}";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
