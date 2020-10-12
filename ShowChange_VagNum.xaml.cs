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
    /// Логика взаимодействия для ShowChange_VagNum.xaml
    /// </summary>
    public partial class ShowChange_VagNum : Window
    {
        private Global global;
        public ShowChange_VagNum()
        {
            InitializeComponent();
            global = Global.getInstance();

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            String vag = textboxVag.Text;
            global.DATA[global.Idx].VagNum = vag;
            
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
