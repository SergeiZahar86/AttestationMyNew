using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_Shipper_String : Window
    {
        private Global global;
        private List<Shippers> shippersVal;
        int a;
        string s;

        public ShowChange_Shipper_String()
        {
            InitializeComponent();
            global = Global.getInstance();
            shippersVal = global.shippers;
            shipper_Value.ItemsSource = shippersVal;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Shipper))
            //if (global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Shipper))
            {
                global.ROWS[global.Idx].Shipper_String = global.rowTab.Shipper_String;
                this.Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.rowTab.Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            global.rowTab.Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;
            global.ROWS[global.Idx].Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;


            //global.ROWS[global.Idx].Shipper = global.shippers[shipper_Value.SelectedIndex].Id - 1;
            //global.ROWS[global.Idx].Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;

        }
    }
}
