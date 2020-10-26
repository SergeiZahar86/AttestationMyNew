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
        public static bool isVerification;             // флаг для подтверждения окончания аттестации

        //public DateTime time;

        public AttestationPage()
        {
            InitializeComponent();
            global = Global.getInstance();
            //time = global.timeGlobal;
            isVerification = false;                                       // флаг для подтверждения окончания аттестации
            DataGridMain.IsEnabled = global.isEnabled;                    // флаг кликабельности datagrid

            //StartAttestation.Background = global.currentColorStart;     // цвет кнопки аттестации
            if (!global.isColor)
            {
                global.mainButtonAttestation = "Закончить";
                startRow_1.Text = global.mainButtonAttestation;               // текст в кнопке аттестации
                StartAttestation.Background = global.RedColorEnd;             // красный

            }

            timeStart.Text = global.startTimeStr;                         // время начала
            timeEnd.Text = global.endTimeStr;                             // Время окончания
            timeDelta.Text = global.deltaTimeStr;                         // Время затраченное на аттестации (продолжительность)

            part_idTextBlock.Text = global.PartId;                        // Номер партии
            matTextBlock.Text = global.MatName;                           // Название материала
            shippersTextBlock.Text = global.Shipper;                      // Грузоотправитель
            consigneesTextBlock.Text = global.Consignee;                  // Грузополучатель
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
                if (global.IdConsigner != null && global.IdShipper != null && global.IdMat != null)
                {
                    //////////// Установка времени ///////////////////////////////////////////////////////
                    global.startTime = DateTime.Now;                       // Запись текущего времени
                    global.startTimeStr = null;                            // Начало аттестации партии вагонов для страницы Аттестации
                    global.endTimeStr = null;                              // Окончание аттестации для страницы Аттестации
                    global.deltaTimeStr = null;                            // Продолжительность прохождения аттестации для страницы Аттестации 
                    timeDelta.Text = global.deltaTimeStr;                  // Время затраченное на аттестации (продолжительность) (вверху страницы)
                    timeEnd.Text = global.endTimeStr;                      // Время окончания аттестации (вверху страницы)
                    global.startTimeStr = global.startTime.ToString();     // Время начала аттестации (глобал)
                    timeStart.Text = global.startTimeStr;                  // Время начала аттестации (вверху страницы)
                    ///////////////////////////////////////////////////////////////////////
                    
                    //isVerification = false;                              // флаг для подтверждения окончания аттестации
                    global.isEnabled = true;                               // флаг кликабельности datagrid
                    DataGridMain.IsEnabled = global.isEnabled;             // разрешаю кликабельность в datagrid

                    /* Запрос партии вагонов */
                    global.GetGlobalPart((int)global.IdShipper, (int)global.IdConsigner, (int)global.IdMat, global.user); /* Начало аттестации
                                                                                                             *   и получение партии вагонов */

                    global.PartId = global.part.Part_id.ToString();              // Номер партии
                    part_idTextBlock.Text = global.part.Part_id.ToString();      // Номер партии
                    shippersTextBlock.Text = global.Shipper;                     // Грузоотправитель
                    consigneesTextBlock.Text = global.Consignee;                 // Грузополучатель


                    matTextBlock.Text = global.MatName;                          // Название материала

                    StartAttestation.Background = global.RedColorEnd;            // красный

                    global.mainButtonAttestation = "Закончить";
                    startRow_1.Text = global.mainButtonAttestation;

                    DataGridMain.ItemsSource = null;
                    DataGridMain.ItemsSource = global.ROWS;     // Привязка вагонов к datagrid

                    global.IdConsigner = null;                  // id Грузополучателя для диалогового окна input_Of_Initial_Data при начале аттестации
                    global.IdShipper = null;                    // id Грузоотправителя  для диалогового окна input_Of_Initial_Data при начале аттестации
                    global.IdMat = null;                        // id материала для диалогового окна input_Of_Initial_Data при начале аттестации

                    global.isColor = false;                     // флаг для кнопки начала и завершения аттестации
                }
            }
            else
            {
                VerificationEndAttestation ver = new VerificationEndAttestation();    // окно подтверждения окончания аттестации
                ver.ShowDialog();
                if (global.exitAtt(global.part.Part_id) && isVerification)            // метод bool exitAtt() подтверждение окончания аттестации
                {

                    global.endTime = DateTime.Now;                                    // Окончание аттестации 
                    global.endTimeStr = null;                                         // Окончание аттестации (String) для страницы Аттестации
                    global.endTimeStr = global.endTime.ToString();                    /* Перезапись времени окончания в Глобал в виде строки 
                                                                                       для дальнейшей записи в объект car_t и передачи на сервер*/
                    timeEnd.Text = global.endTimeStr;
                    global.deltaTime = global.endTime.Subtract(global.startTime);     // Подсчёт продолжительности аттестации

                    //timeDelta.Text = global.deltaTime.ToString(@"hh\:mm\:ss");        // Вывод затраченного времени вверху страницы
                    timeDelta.Text = global.deltaTime.ToString(@"hh\:mm\:ss");        // Вывод затраченного времени вверху страницы
                    global.deltaTimeStr = global.deltaTime.ToString(@"hh\:mm\:ss");   // затраченное время записывается в Глобал

                    StartAttestation.Background = global.GreenColorStart;             // зеленый

                    global.mainButtonAttestation = "Начать";
                    startRow_1.Text = global.mainButtonAttestation;

                    global.isColor = true;                                            // флаг для кнопки начала и завершения аттестации
                    global.isEnabled = false;                                         // флаг кликабельности datagrid
                    DataGridMain.IsEnabled = global.isEnabled;                        // убирается кликабельность с datagrid


                }
            }
        }
        private void Foto_Click(object sender, RoutedEventArgs e) /* выводит окно
        с фотографиями вагонов */
        {
            /*
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
            */
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
            showChange_Carrying.oldCarrying.Content = global.ROWS[global.Idx].Carrying;
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
