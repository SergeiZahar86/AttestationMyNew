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
