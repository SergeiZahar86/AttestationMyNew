using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            global = Global.getInstance();
            idx = global.Idx;
            DATA = global.DATA;
            //image1.Source = DATA[idx].LeftFoto;
        }


        private void close_ShowPhotos(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
