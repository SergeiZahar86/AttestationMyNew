using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class input_Of_Initial_Data : Window
    {
        private Global global;
        private List<Shippers> shipp; // справочник Грузоотправителя
        private List<Consigners> consig; // справочник Грузополучателя
        private List<mat_t> mats_input; // справочник материалов

        private int? idSh = null;
        private int? idCon = null;
        private int? idMa = null;

        public input_Of_Initial_Data()
        {
            InitializeComponent();
            global = Global.getInstance();

            shipp = global.shippers;
            shipper_Value.ItemsSource = shipp;

            consig = global.consigners;
            consignee_Value.ItemsSource = consig;

            mats_input = global.mats;
            mat_Value.ItemsSource = mats_input;

        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (idMa != null && idCon != null && idSh != null)
            {
                global.IdShipper = idSh;
                global.IdConsigner = idCon;
                global.IdMat = idMa;
                this.Close();
            }
            else
            {
                TextInput.Text = "Выберете значения из списков";
            }
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //global.IdShipper = shipp[shipper_Value.SelectedIndex].Id;
            idSh = shipp[shipper_Value.SelectedIndex].Id - 1;
            global.Shipper = shipp[shipper_Value.SelectedIndex].Name;
        }
        private void consignee_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //global.IdConsignee = consig[consignee_Value.SelectedIndex].Id;
            idCon = consig[consignee_Value.SelectedIndex].Id - 1;
            global.Consignee = consig[consignee_Value.SelectedIndex].Name;
        }
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //global.IdMat = mats_input[mat_Value.SelectedIndex].Id;
            idMa = mats_input[mat_Value.SelectedIndex].Id - 1;
            global.MatName = mats_input[mat_Value.SelectedIndex].Name;
        }
    }
}
