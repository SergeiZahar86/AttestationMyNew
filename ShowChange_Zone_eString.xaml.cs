using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            if (global.client.setZone(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Zone_e))
            {
                global.ROWS[global.Idx].Zone_eString = global.rowTab.Zone_eString;
                this.Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void zona_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.rowTab.Zone_e = global.zonas[zona_Value.SelectedIndex ].Id-1;
            global.rowTab.Zone_eString = global.zonas[zona_Value.SelectedIndex ].Name;
            
        }
    }
}
