using System;
using System.Windows;

namespace Attestation
{
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
            global.ROWS[global.Idx].Num = vag;
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
