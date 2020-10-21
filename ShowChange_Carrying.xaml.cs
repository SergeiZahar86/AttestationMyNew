using System;
using System.Windows;

namespace Attestation
{
    public partial class ShowChange_Carrying : Window
    {
        private Global global;
        public ShowChange_Carrying()
        {
            InitializeComponent();
            global = Global.getInstance();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            String car = textboxCarrying.Text;
            try
            {
                global.ROWS[global.Idx].Carrying = Convert.ToDouble(car);
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
