using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_Mat_String : Window
    {
        private Global global;
        private List<mat_t> matsVal;
        private int Mat;
        private string Mat_String;

        public ShowChange_Mat_String()
        {
            InitializeComponent();
            global = Global.getInstance();
            matsVal = global.mats;
            mat_Value.ItemsSource = matsVal;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (global.client.setMat(global.part.Part_id, global.ROWS[global.Idx].Car_id, Mat))
                {
                    global.ROWS[global.Idx].Mat_String = Mat_String;
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
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mat = global.mats[mat_Value.SelectedIndex].Id;
            Mat_String = global.mats[mat_Value.SelectedIndex].Name;

            /*global.rowTab.Mat = global.mats[mat_Value.SelectedIndex].Id;
            global.rowTab.Mat_String = global.mats[mat_Value.SelectedIndex].Name;*/

        }
    }
}
