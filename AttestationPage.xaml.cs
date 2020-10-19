using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

using iTextSharp.text;
using System.Drawing.Imaging;

namespace Attestation
{
    public partial class AttestationPage : Page
    {
        public int idx; // индекс строки
        private Global global;
        private List<String> att;


        public AttestationPage()
        {
            InitializeComponent();
            global = Global.getInstance();
            att = global.Att;

            StartAttestation.Background = global.currentColor;
            startRow_1.Text = global.mainButtonAttestation;

        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e) /* загрузка 
        данных в DataGrid*/
        {
            //global.DATA.Clear();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }
        private void StartAttestation_Click(object sender, RoutedEventArgs e) /* Кнопка начала аттестации*/
        {
            
            //currentColor = StartAttestation.Background;
            if(global.isColor) // проверяем флаг
            {
                input_Of_Initial_Data inputOf = new input_Of_Initial_Data();
                inputOf.ShowDialog();
                // записываем цвет и текст в одиночку
                StartAttestation.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 33, 23)); // красный
                global.currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 33, 23)); 
                startRow_1.Text = "Закончить";
                global.mainButtonAttestation = "Закончить";
                global.isColor = false;
            }
            else
            {
                StartAttestation.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1)); // зеленый
                global.currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1));
                startRow_1.Text = "Начать";
                global.mainButtonAttestation = "Начать";
                global.isColor = true;
            }
            //StartAttestation.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb( 255, 75, 64));
        }
        private void Foto_Click(object sender, RoutedEventArgs e) /* выводит окно
        с фотографиями вагонов */
        {
            global.Idx = DataGridMain.SelectedIndex;
            global.photo = global.getPhoto(global.part.Part_id, global.DATA[global.Idx].Car_id);
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
                System.Windows.MessageBox.Show("У строки " + global.DATA[idx].Car_id.ToString() + " нет фотографий");
            }
        }
        
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            /*
            this.DATA.Add(new RowTab( 1, true, (88345634).ToString(), (float)(2), (float)(2), (float)(2)));
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = this.DATA;
            */
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Change_VagNum(object sender, RoutedEventArgs e)/* Изменение номера вагона*/
        {
            ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_VagNum.oldVagNum.Content = global.DATA[global.Idx].Num;
            showChange_VagNum.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }
        private void Change_isOk(object sender, RoutedEventArgs e)/* Изменение итогов аттестации*/
        {
            ShowChange_isOk showChange_IsOk = new ShowChange_isOk();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_IsOk.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }
        private void CauseButton_Click (object sender, RoutedEventArgs e)/* Установить причину неаттестации*/
        {
            ShowChange_cause_t showChange_Cause_T = new ShowChange_cause_t();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Cause_T.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }

        
    }
}
