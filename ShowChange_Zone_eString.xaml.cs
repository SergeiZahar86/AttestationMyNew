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
    public partial class ShowChange_Zone_eString : Window
    {
        private Global global;
        private List<Zona> zona_Val;

        public ShowChange_Zone_eString()
        {
            InitializeComponent();
            global = Global.getInstance();
            zona_Val = global.zonas;
            zona_Value.ItemsSource = zona_Val;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void zona_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.ROWS[global.Idx].Zone_e = global.zonas[zona_Value.SelectedIndex].Id;
            global.ROWS[global.Idx].Zone_eString = global.zonas[zona_Value.SelectedIndex].Name;
        }
    }
}
