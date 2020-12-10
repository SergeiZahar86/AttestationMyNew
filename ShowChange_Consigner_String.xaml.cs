using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_Consigner_String : Window
    {
        private Global global;
        private List<Consigners> consignersVal;

        public ShowChange_Consigner_String()
        {
            InitializeComponent();
            global = Global.getInstance();
            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Consigner))
                {
                    global.ROWS[global.Idx].Consigner_String = global.rowTab.Consigner_String;
                    this.Close();
                }
            }
            catch
            {
                TextInput.Text = "Ошибка отправки на сервер";
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.rowTab.Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            global.rowTab.Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;

        }
    }
}
