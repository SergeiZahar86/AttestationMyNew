using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_Consigner_String : Window
    {
        private Global global;
        private List<Consigners> consignersVal;
        private int Consigner;
        private string Consigner_String;
        private bool isOk;

        public ShowChange_Consigner_String()
        {
            InitializeComponent();
            global = Global.getInstance();
            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (isOk)
            {
                try
                {
                    if (global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, Consigner))
                    {
                        global.ROWS[global.Idx].Consigner_String = Consigner_String;
                        this.Close();
                    }
                }
                catch
                {
                    TextInput.Text = "Ошибка отправки на сервер";
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;
            isOk = true;
        }
    }
}
