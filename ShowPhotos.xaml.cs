using System.Collections.Generic;
using System.Windows;

namespace Attestation
{
    /// <summary>
    /// Логика взаимодействия для ShowPhotos.xaml
    /// </summary>
    public partial class ShowPhotos : Window
    {
        private Global global;
        public int idx;
        public List<RowTab> DATA;


        public ShowPhotos()
        {
            InitializeComponent();
            /*
            global = Global.getInstance();
            idx = global.Idx;
            DATA = global.DATA;
            */
        }


        private void close_ShowPhotos(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
