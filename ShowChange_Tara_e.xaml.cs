using System;
using System.Windows;

namespace Attestation
{
    public partial class ShowChange_Tara_e : Window
    {
        private Global global;
        public ShowChange_Tara_e()
        {
            InitializeComponent();
            global = Global.getInstance();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            String tar = textboxVag.Text;
            try
            {
                global.ROWS[global.Idx].Tara_e = Convert.ToDouble(tar);
                global.ROWS[global.Idx].Tara_delta = Math.Round((global.ROWS[global.Idx].Tara - global.ROWS[global.Idx].Tara_e),
                    3, MidpointRounding.AwayFromZero);
                this.Close();
            }
            catch
            {
                result.Text = "Вы ввели не число";
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
