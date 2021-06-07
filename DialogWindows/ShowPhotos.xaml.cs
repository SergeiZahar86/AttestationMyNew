using System.Windows;

namespace Attestation
{
    public partial class ShowPhotos : Window
    {
        private Global global;
        public ShowPhotos()
        {
            InitializeComponent();
            global = Global.getInstance();
            if (!global.ArmAttestation)
                chengeNumVag.IsEnabled = false;
        }
        private void close_ShowPhotos(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chengeNumVag_Click(object sender, RoutedEventArgs e)
        {
            if (global.ArmAttestation)
            {
                try
                {
                    ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
                    showChange_VagNum.Owner = Window.GetWindow(this);
                    //global.Idx = DataGridMain.SelectedIndex;
                    showChange_VagNum.oldVagNum.Content = global.ROWS[global.Idx].Num;
                    showChange_VagNum.Number.Text = global.ROWS[global.Idx].Car_id.ToString();
                    showChange_VagNum.ShowDialog();
                    //DataGridMain.ItemsSource = null;
                    //DataGridMain.ItemsSource = global.ROWS;
                }
                catch { }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
