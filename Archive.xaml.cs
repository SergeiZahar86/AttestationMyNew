using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class Archive : Page
    {
        private Global global;
        public Archive()
        {
            InitializeComponent();
            global = Global.getInstance();

            timeStart.Text = global.startTimeStr; // время начала
            timeEnd.Text = global.endTimeStr; // Время окончания
            timeDelta.Text = global.deltaTimeStr; // Время затраченное на аттестации

            part_idTextBlock.Text = global.PartId; // Номер партии
            matTextBlock.Text = global.MatName; // Название материала
            shippersTextBlock.Text = global.Shipper; // Грузоотправитель
            consigneesTextBlock.Text = global.Consignee; // Грузополучатель
        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e) /* загрузка 
        данных в DataGrid*/
        {
            DataGridArchive.ItemsSource = null;
            DataGridArchive.ItemsSource = global.ROWS;
        }
        private void Foto_Click(object sender, RoutedEventArgs e) /* выводит окно
        с фотографиями вагонов */
        {
            global.Idx = DataGridArchive.SelectedIndex;
            global.photo = global.getPhoto(global.part.Part_id, global.ROWS[global.Idx].Car_id);
            if (global.photo.Left != null & global.photo.Right != null & global.photo.Top != null)
            {
                ShowPhotos showPhotos = new ShowPhotos();
                showPhotos.image1.Source = Global.ByteArraytoBitmap(global.photo.Left);
                showPhotos.image2.Source = Global.ByteArraytoBitmap(global.photo.Right);
                showPhotos.image3.Source = Global.ByteArraytoBitmap(global.photo.Top);
                showPhotos.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("У строки " + global.ROWS[global.Idx].Car_id.ToString() + " нет фотографий");
            }
        }
        private void GetArchive_Click(object sender, RoutedEventArgs e)
        {
            DatePickerWindow datePicker = new DatePickerWindow();
            datePicker.ShowDialog();
        }
    }
}
