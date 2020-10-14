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
        //System.Drawing.Image leftFoto;
        //System.Drawing.Image rightFoto;
        //System.Drawing.Image topFoto;
        byte[] leftFoto;
        byte[] rightFoto;
        byte[] topFoto;
        //public string Image1FromRowTab { set; get; }
        //public string Image2FromRowTab { set; get; }
        //public string Image3FromRowTab { set; get; }
        public int idx; // индекс строки
        private Global global;
        public AttestationPage()
        {
            InitializeComponent();
            global = Global.getInstance();
            //Image1FromRowTab = "C:/Projects/АРМ_ОТК/ImageFromRowTab/Bitmap1.bmp";
            //Image2FromRowTab = "C:/Projects/АРМ_ОТК/ImageFromRowTab/Bitmap2.bmp";
            //Image3FromRowTab = "C:/Projects/АРМ_ОТК/ImageFromRowTab/Bitmap3.bmp";
            

        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e)
        {
            //global.DATA.Clear();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }
        private void Foto_Click(object sender, RoutedEventArgs e)
        {
            global.Idx = DataGridMain.SelectedIndex;
            global.photo = global.getPhoto(global.part.Part_id, global.DATA[global.Idx].Car_id);
            if (global.photo.Left != null & global.photo.Right != null & global.photo.Top != null)
            {
                ShowPhotos showPhotos = new ShowPhotos();
                showPhotos.image1.Source = ByteArraytoBitmap(global.photo.Left);
                showPhotos.image2.Source = ByteArraytoBitmap(global.photo.Right);
                showPhotos.image3.Source = ByteArraytoBitmap(global.photo.Top);
                showPhotos.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("У строки " + global.DATA[idx].Car_id.ToString() + " нет фотографий");

            }



            /*
            global.Idx = DataGridMain.SelectedIndex;
            RowTab row = global.DATA[global.Idx];
            if(row.LeftFoto != null & row.RightFoto != null & row.TopFoto != null)
            {
                ShowPhotos showPhotos = new ShowPhotos();
                showPhotos.image1.Source = ByteArraytoBitmap(row.LeftFoto);
                showPhotos.image2.Source = ByteArraytoBitmap(row.RightFoto);
                showPhotos.image3.Source = ByteArraytoBitmap(row.TopFoto);
                showPhotos.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("У строки " + row.Id.ToString() + " нет фотографий");
                
            }
            */
        }
        public static BitmapImage ByteArraytoBitmap(Byte[] byteArray)
        {
            MemoryStream stream = new MemoryStream(byteArray);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
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

        private void Change_VagNum(object sender, RoutedEventArgs e)
        {
            ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
            global.Idx = DataGridMain.SelectedIndex;
            //showChange_VagNum.oldVagNum.Content = global.DATA[global.Idx].VagNum;



            showChange_VagNum.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
        }

        private void Change_isOk(object sender, RoutedEventArgs e)
        {
            ShowChange_isOk showChange_IsOk = new ShowChange_isOk();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_IsOk.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.DATA;
            
        }
    }
}
