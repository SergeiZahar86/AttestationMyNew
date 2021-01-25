using System.Windows;

namespace Attestation
{
    public partial class ShowPhotos : Window
    {
        private Global global;
        public ShowPhotos()
        {
            InitializeComponent();
        }
        private void close_ShowPhotos(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
