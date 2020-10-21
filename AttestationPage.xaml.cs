using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Attestation
{
    public partial class AttestationPage : Page
    {
        public int idx; // индекс строки
        private Global global;
        public static bool isVerification; // флаг для подтверждения окончания аттестации

        //public DateTime time;

        public AttestationPage()
        {
            InitializeComponent();
            global = Global.getInstance();
            //time = global.timeGlobal;
            isVerification = false;
            DataGridMain.IsEnabled = global.isEnabled;

            StartAttestation.Background = global.currentColor; // цвет кнопки аттестации
            startRow_1.Text = global.mainButtonAttestation; // текст в кнопке аттестации

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
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void StartAttestation_Click(object sender, RoutedEventArgs e) /* Кнопка начала аттестации*/
        {
            if(global.isColor) // проверяем флаг
            {
                input_Of_Initial_Data inputOf = new input_Of_Initial_Data();
                inputOf.ShowDialog();
                if (global.IdConsignee != null && global.IdShipper != null && global.IdMat != null)
                {
                    //////////// Установка времени ///////////////////////////////////////////////////////
                    global.startTime = DateTime.Now;
                    global.startTimeStr = null;
                    global.endTimeStr = null;
                    global.deltaTimeStr = null;
                    timeEnd.Text = global.endTimeStr;
                    global.startTimeStr = global.startTime.ToString();
                    timeStart.Text = global.startTimeStr;
                    ///////////////////////////////////////////////////////////////////////
                    
                    isVerification = false; // флаг для подтверждения окончания аттестации
                    DataGridMain.IsEnabled = true; // разрешаю кликабельность в datagrid
                    global.isEnabled = true; // флаг кликабельности datagrid

                    /* Запрос партии вагонов */
                    global.GetGlobalPart((int)global.IdShipper, (int)global.IdConsignee, (int)global.IdMat, global.user);

                    global.PartId = global.part.Part_id.ToString(); // Номер партии
                    part_idTextBlock.Text = global.part.Part_id.ToString(); // Номер партии
                    shippersTextBlock.Text = global.Shipper; // Грузоотправитель
                    consigneesTextBlock.Text = global.Consignee; // Грузополучатель


                    matTextBlock.Text = global.MatName; // Название материала

                    // записываем цвет и текст в одиночку
                    StartAttestation.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 33, 23)); // красный
                    global.currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 33, 23));
                    startRow_1.Text = "Закончить";
                    global.mainButtonAttestation = "Закончить";
                    global.isColor = false;

                    DataGridMain.ItemsSource = null;
                    DataGridMain.ItemsSource = global.ROWS;

                    global.IdConsignee = null;
                    global.IdShipper = null;
                    global.IdMat = null;
                }
            }
            else
            {
                VerificationEndAttestation ver = new VerificationEndAttestation(); // окно подтверждения окончания аттестации
                ver.ShowDialog();
                if (isVerification)
                {
                    global.endTime = DateTime.Now;
                    global.endTimeStr = null;
                    global.endTimeStr = global.endTime.ToString();
                    timeEnd.Text = global.endTimeStr;
                    global.deltaTime = global.endTime.Subtract(global.startTime);

                    timeDelta.Text = global.deltaTime.ToString(@"hh\:mm\:ss");
                    global.deltaTimeStr = global.deltaTime.ToString(@"hh\:mm\:ss");

                    StartAttestation.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1)); // зеленый
                    global.currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1));
                    startRow_1.Text = "Начать";
                    global.mainButtonAttestation = "Начать";
                    global.isColor = true;
                    DataGridMain.IsEnabled = false; // убирается кликабельность с datagrid
                    global.isEnabled = false; // флаг кликабельности datagrid
                }
            }
        }
        private void Foto_Click(object sender, RoutedEventArgs e) /* выводит окно
        с фотографиями вагонов */
        {
            global.Idx = DataGridMain.SelectedIndex;
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
        
        private void Change_VagNum(object sender, RoutedEventArgs e)/* Изменение номера вагона*/
        {
            ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_VagNum.oldVagNum.Content = global.ROWS[global.Idx].Num;
            showChange_VagNum.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void Change_isOk(object sender, RoutedEventArgs e)/* Изменение итогов аттестации*/
        {
            ShowChange_isOk showChange_IsOk = new ShowChange_isOk();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_IsOk.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void CauseButton_Click (object sender, RoutedEventArgs e)/* Установить причину неаттестации*/
        {
            ShowChange_cause_t showChange_Cause_T = new ShowChange_cause_t();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Cause_T.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void Change_Tara_e(object sender, RoutedEventArgs e) // изменение Тара НСИ
        {
            ShowChange_Tara_e showChange_Tara_e = new ShowChange_Tara_e();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Tara_e.oldTara_e.Content = global.ROWS[global.Idx].Tara_e;
            showChange_Tara_e.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void Change_Carrying(object sender, RoutedEventArgs e) // изменение Грузоподъемность
        {
            ShowChange_Carrying showChange_Carrying = new ShowChange_Carrying();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Carrying.oldCarrying.Content = global.ROWS[global.Idx].Tara_e;
            showChange_Carrying.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void Change_Zone_eString(object sender, RoutedEventArgs e) // изменение Зоны вагонов
        {
            ShowChange_Zone_eString showChange_Zone_eString = new ShowChange_Zone_eString();
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Zone_eString.ShowDialog();
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }

    }
}
