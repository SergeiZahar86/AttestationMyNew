﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Attestation
{
    public partial class AttestationPage : Page
    {
        public int idx;                                             // индекс строки
        private Global global;
        public static bool isVerification;                          // флаг для подтверждения окончания аттестации
        System.Windows.Threading.DispatcherTimer dispatcherTimer;   // Таймер аттестации
        System.Windows.Threading.DispatcherTimer timerConnect;
        //private bool realNumber;                                    // флаг проверки подлинности номера вагона
        MqttClient client;
        RowTab row;
        private bool is_Num_close_att;                              // флаг количества цифр в номерах вагонов
        private int CountRow;                                     // для сравнения списков с целью выявления изменений
        private bool chenge;
        //ObservableCollection<RowTab> observable;


        private void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            try
            {
                global.part = global.client.getPart(global.PartId);    // получение партии вагонов с сервера
                if (global.part != null)
                {
                    if (global.part.Cars.Count == 1 && CountRow != global.part.Cars.Count)
                    {
                        global.DATA = global.part.Cars;                        // получаем серверный список вагонов
                        global.ROWS = global.GetRows();                        // получаем внутренний список вагонов
                                                                               //observable = global.ROWS;
                        DataGridMain.ItemsSource = null;
                        DataGridMain.ItemsSource = global.ROWS;
                        CountRow = 1;
                    }
                    else if (CountRow != global.part.Cars.Count)
                    {
                        global.DATA = global.part.Cars;                        // получаем серверный список вагонов
                        global.ROWS = global.GetRows();                        // получаем внутренний список вагонов
                                                                               //observable = global.ROWS;
                        DataGridMain.ItemsSource = null;
                        DataGridMain.ItemsSource = global.ROWS;

                        CountRow = global.ROWS.Count;
                    }
                }
            }
            catch (Exception ass)
            {
                ExClose exClose = new ExClose(ass.ToString());
                exClose.ShowDialog();
            }
        }
        private void ConnectTimer(Object source, EventArgs e)      // таймер проверки соединения с сервером
        {
            /* try
             {
                 global.client.getStatusAtt(); // Запрос статуса начала или завершения аттестации
             }
             catch { }*/
            if (!global.transport.IsOpen) // проверяем соединение
            {
                connect.Background = global.RedColorEnd;
                textConnect.Text = "Восстановить соединение";
                toolTipConnect.Text = "Нажмите чтобы восстановить соединение с сервером";

                try
                {
                    global.transport.Close();
                    global.transport.Open();
                    MessageBox.Show("Соединение с сервером восстановлено");
                    /*global.GetSignIn();                                                    // авторизация
                    name.Content = Global.ShortName(global.user);                          // выводим имя пользователя
                    if (global.user.Length > 0)
                    {*/
                        global.workAfterShutdown();                                        // восстановление после разрыва
                    //}
                    connect.Background = global.GreenColorStart;
                    textConnect.Text = "Соединение установленно";
                    toolTipConnect.Text = "Соединение с сервером установленно";
                }
                catch (Exception ass)
                {
                    //MessageBox.Show(ass.Message);
                    ExClose exClose = new ExClose(ass.ToString());
                    exClose.ShowDialog();
                }


            }
            /*else
            {
                connect.Background = global.GreenColorStart;
                textConnect.Text = "Соединение установленно";
                toolTipConnect.Text = "Соединение с сервером установленно";

            }*/
        }
        public AttestationPage() // конструктор
        {
            InitializeComponent();
            global = Global.getInstance();

            CountRow = 100;                                                        // для сравнения списков с целью выявления изменений
            is_Num_close_att = true;                                            
            //realNumber = false;                                                  // флаг проверки подлинности номера вагона
            // Таймеры ///////////////////////////////////////
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

            timerConnect = new System.Windows.Threading.DispatcherTimer();
            timerConnect.Tick += new EventHandler(ConnectTimer);
            timerConnect.Interval = new TimeSpan(0, 0, 16);
            timerConnect.Start();
            if (!global.transport.IsOpen) // проверяем соединение
            {
                connect.Background = global.RedColorEnd;
                textConnect.Text = "Восстановить соединение";
                toolTipConnect.Text = "Нажмите чтобы восстановить соединение с сервером";
            }
            else
            {
                connect.Background = global.GreenColorStart;
                textConnect.Text = "Соединение установленно";
                toolTipConnect.Text = "Соединение с сервером установленно";
                //connect.HorizontalContentAlignment = HorizontalAlignment.Center;
                /*TextBlock textBlock = new TextBlock();
                textBlock.Text = "Сервер доступен";
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.FontSize = 16;
                connect.Content = textBlock;*/
                //connect.Content = "Сервер доступен";
            }

            /////////////////////////////////////////////////
            isVerification = false;                                       // флаг для подтверждения окончания аттестации
            //DataGridMain.ItemsSource = observable;
            //DataGridMain.SelectedItem = RowTab;
            DataGridMain.IsEnabled = global.isEnabled;                    // флаг кликабельности datagrid
            //////// MQTT ///////////////////////////
            ///
            try
            {
                client = new MqttClient(global.Mqtt_host, 1883, false, null, null, MqttSslProtocols.None); // подключение к серверу ИндасХолдинг у Коли
                //client = new MqttClient("10.90.101.200", 1883, false, null, null, MqttSslProtocols.None); // подключение к серверу ИндасХолдинг у Коли
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; // этот код запускается при получении сообщения
                client.Connect("ArmOTK", global.Mqtt_user, global.Mqtt_password); // подключение к серверу ИндасХолдинг у Коли
                string Topic = global.PLC_topic;
                client.Subscribe(new string[] { Topic }, new byte[] { 0 });
            }
            catch (Exception ass)
            {
                ExClose exClose = new ExClose(ass.ToString());
                exClose.ShowDialog();
            }

            if (!global.isColor)
            {
                global.mainButtonAttestation = "Закончить";
                startRow_1.Text = global.mainButtonAttestation;           // текст в кнопке аттестации
                StartAttestation.Background = global.RedColorEnd;         // красный
                dispatcherTimer.Start();                                  // Стартуем таймер
            }
            timeStart.Text = global.startTimeStr;                         // время начала
            timeEnd.Text = global.endTimeStr;                             // Время окончания
            timeDelta.Text = global.deltaTimeStr;                         // Время затраченное на аттестации (продолжительность)

            part_idTextBlock.Text = global.PartId;                        // Номер партии
            //matTextBlock.Text = global.MatName;                         // Название материала
            //shippersTextBlock.Text = global.Shipper;                    // Грузоотправитель
            //consigneesTextBlock.Text = global.Consignee;                // Грузополучатель

        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)  // этот код запускается при получении сообщения от mqtt
        {
            try
            {
                string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
                byte b1 = e.Message[0];
                byte b2 = e.Message[1];
                switch (b1)
                {
                    case 0b01010000:
                        Dispatcher.Invoke(delegate { shippersTextBlock.Text = "Начало погрузки"; });
                        break;
                    case 0b01100000:
                        Dispatcher.Invoke(delegate { shippersTextBlock.Text = "Окончание погрузки"; });
                        break;
                    case 0b10010000:
                        Dispatcher.Invoke(delegate { shippersTextBlock.Text = "Начало взвешивания"; });
                        break;
                    case 0b10100000:
                        Dispatcher.Invoke(delegate { shippersTextBlock.Text = "Окончание взвешивания"; });
                        break;
                }
            }
            catch(Exception ass)
            {
                MessageBox.Show(ass.Message);
            }
        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e)            // загрузка данных в DataGrid
        {
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void StartAttestation_Click(object sender, RoutedEventArgs e)   // Кнопка начала аттестации
        {
            try
            {

                if (global.isColor) // проверяем флаг
                {
                    if (!global.transport.IsOpen) // проверяем соединение
                    {
                        try
                        {
                            global.transport.Close();
                            global.transport.Open();
                            MessageBox.Show("Соединение с сервером восстановлено");
                        }
                        catch(Exception ass)
                        {
                            MessageBox.Show(ass.Message);
                        }
                    }
                    /*
                    input_Of_Initial_Data inputOf = new input_Of_Initial_Data();
                    inputOf.ShowDialog();

                    if (global.IdConsigner != null && global.IdShipper != null && global.IdMat != null)
                    {
                    */
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
                    Start_Att start = new Start_Att();
                    start.ShowDialog();
                    //start.Focus();

                    //global.GetGlobalPart(global.user);                     // Начало аттестации, вызов метода startAtt() и получение партии вагонов

                    //start.Close();
                    if (global.part == null)
                    {
                        return;
                    }
                    try
                    {
                        global.PartId = global.part.Part_id.ToString();              // Номер партии
                        part_idTextBlock.Text = global.part.Part_id.ToString();      // Номер партии
                                                                                     //shippersTextBlock.Text = global.Shipper;                   // Грузоотправитель
                                                                                     //consigneesTextBlock.Text = global.Consignee;               // Грузополучатель


                        //matTextBlock.Text = global.MatName;                        // Название материала

                        StartAttestation.Background = global.RedColorEnd;            // красный

                        global.mainButtonAttestation = "Закончить";
                        startRow_1.Text = global.mainButtonAttestation;

                        DataGridMain.ItemsSource = null;
                        DataGridMain.ItemsSource = global.ROWS;     // Привязка вагонов к datagrid

                        global.IdConsigner = null;                  // id Грузополучателя для диалогового окна input_Of_Initial_Data при начале аттестации
                        global.IdShipper = null;                    // id Грузоотправителя  для диалогового окна input_Of_Initial_Data при начале аттестации
                        global.IdMat = null;                        // id материала для диалогового окна input_Of_Initial_Data при начале аттестации

                        global.isColor = false;                     // флаг для кнопки начала и завершения аттестации

                        dispatcherTimer.Start();                    // Стартуем таймер
                                                                    //}
                    }
                    catch (Exception sss)
                    {
                        MessageBox.Show(sss.Message);
                    }
                }
                
                else
                {
                    is_Num_close_att = true;
                    VerificationEndAttestation ver = new VerificationEndAttestation();    // окно подтверждения окончания аттестации
                    ver.ShowDialog();
                    Close_Att close = new Close_Att();
                    close.ShowDialog();
                    foreach(RowTab ff in global.ROWS)
                    {
                        if(ff.Num.Length != 8) // делаем проверку длины номера вагона
                        {
                            is_Num_close_att = false;
                        }
                    }
                    if (global.is_ok_close_att && is_Num_close_att)            // метод bool exitAtt() подтверждение окончания аттестации
                    {
                        //start.Close();

                        global.endTime = DateTime.Now;                                    // Окончание аттестации 
                        global.endTimeStr = null;                                         // Окончание аттестации (String) для страницы Аттестации
                        global.endTimeStr = global.endTime.ToString();                    /* Перезапись времени окончания в Глобал в виде строки 
                                                                                       для дальнейшей записи в объект car_t и передачи на сервер*/
                        timeEnd.Text = global.endTimeStr;
                        global.deltaTime = global.endTime.Subtract(global.startTime);     // Подсчёт продолжительности аттестации
                        global.deltaTimeStr = global.deltaTime.ToString(@"hh\:mm\:ss");   // затраченное время записывается в Глобал
                        timeDelta.Text = global.deltaTime.ToString(@"hh\:mm\:ss");        // Вывод затраченного времени вверху страницы

                        StartAttestation.Background = global.GreenColorStart;             // зеленый

                        global.mainButtonAttestation = "Начать";
                        startRow_1.Text = global.mainButtonAttestation;

                        global.isColor = true;                                            // флаг для кнопки начала и завершения аттестации
                        global.isEnabled = false;                                         // флаг кликабельности datagrid
                        DataGridMain.IsEnabled = global.isEnabled;                        // убирается кликабельность с datagrid

                        //global.client.setUser(global.part.Part_id, global.user);          // запись имени оператора в сервер в конце аттестации

                        dispatcherTimer.Stop();                                           // Останавливает таймер
                        
                    }
                    else
                    {
                        MessageBox.Show("Номера вагонов должны состоять из восьми цифр (АРМ)");
                    }
                }
            }
            catch (Exception ass)
            {
                ExClose exClose = new ExClose(ass.ToString());
                exClose.ShowDialog();
            }
        }
        private void Foto_Click(object sender, RoutedEventArgs e)               // выводит окно с фотографиями вагонов
        {
            try
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
            catch (Exception ass)
            {
                ExClose exClose = new ExClose(ass.ToString());
                exClose.ShowDialog();
            }
        }
        private void Change_VagNum(object sender, RoutedEventArgs e)            // Изменение номера вагона
        {
            try
            {
                ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
                global.Idx = DataGridMain.SelectedIndex;
                showChange_VagNum.oldVagNum.Content = global.ROWS[global.Idx].Num;
                showChange_VagNum.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_isOk(object sender, RoutedEventArgs e)              // Изменение итогов аттестации
        {
            ShowChange_isOk showChange_IsOk = new ShowChange_isOk();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                showChange_IsOk.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void CauseButton_Click (object sender, RoutedEventArgs e)       // Установить причину неаттестации
        {
            ShowChange_cause_t showChange_Cause_T = new ShowChange_cause_t();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                showChange_Cause_T.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_Tara_e(object sender, RoutedEventArgs e)            // изменение Тара НСИ
        {
            try
            {
                ShowChange_Tara_e showChange_Tara_e = new ShowChange_Tara_e();
                global.Idx = DataGridMain.SelectedIndex;
                showChange_Tara_e.oldTara_e.Content = global.ROWS[global.Idx].Tara_e;
                showChange_Tara_e.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_Carrying(object sender, RoutedEventArgs e)          // изменение Грузоподъемность
        {
            ShowChange_Carrying showChange_Carrying = new ShowChange_Carrying();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                showChange_Carrying.oldCarrying.Content = global.ROWS[global.Idx].Carrying;
                showChange_Carrying.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_Zone_eString(object sender, RoutedEventArgs e)      // изменение Зоны вагонов
        {
            ShowChange_Zone_eString showChange_Zone_eString = new ShowChange_Zone_eString();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                showChange_Zone_eString.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void shipperButton_Click(object sender, RoutedEventArgs e)      // изменение Грузоотправителя
        {
            ShowChange_Shipper_String Change_Shipper_String = new ShowChange_Shipper_String();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                Change_Shipper_String.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }

        }
        private void consignerButton_Click(object sender, RoutedEventArgs e)    // изменение Грузополучателя
        {
            ShowChange_Consigner_String Change_Consigner_String = new ShowChange_Consigner_String();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                Change_Consigner_String.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void matButton_Click(object sender, RoutedEventArgs e)          // изменение материала 
        {
            ShowChange_Mat_String Change_Mat_String = new ShowChange_Mat_String();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                Change_Mat_String.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void DataGridMain_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) // изменение значений строки по двойному клику
        {
            Change_of_Data_on_the_Wagon change_Of_Data = new Change_of_Data_on_the_Wagon();
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                ///////  получение объекта RowTab по двойному клику  ////////////////////////////////////
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        global.rowTab = (RowTab)dgr.DataContext;
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                change_Of_Data.Number.Text = global.ROWS[global.Idx].Car_id.ToString();            // порядковый номер вагона в шапке окна
                change_Of_Data.oldVagNum.Content = global.ROWS[global.Idx].Num;                    // старый номер вагона
                change_Of_Data.oldTara.Content = global.ROWS[global.Idx].Tara;                     // вес тары
                change_Of_Data.oldTara_e.Content = global.ROWS[global.Idx].Tara_e;                 // прежний вес тары НСИ
                change_Of_Data.oldTara_delta.Content = global.ROWS[global.Idx].Tara_delta;         // дельта тары
                change_Of_Data.oldCarrying.Content = global.ROWS[global.Idx].Carrying;             // прежняя грузоподъемность
                change_Of_Data.old_zona.Content = global.ROWS[global.Idx].Zone_eString;            // прежнее значение "зона"
                change_Of_Data.old_shipper.Content = global.ROWS[global.Idx].Shipper_String;       // прежнее значение Грузоотправитель
                change_Of_Data.old_consigner.Content = global.ROWS[global.Idx].Consigner_String;   // прежнее значение Грузополучателя
                change_Of_Data.old_mat.Content = global.ROWS[global.Idx].Mat_String;               // прежнее значение материала
                switch (global.ROWS[global.Idx].Att_codeString /* прежнее значение итогов аттестации */)
                {
                    case "CheckCircle":
                        change_Of_Data.old_isOk.Content = "Аттестован"; break;
                    case "WindowClose":
                        change_Of_Data.old_isOk.Content = "Не аттестован"; break;
                    case "Asterisk":
                        change_Of_Data.old_isOk.Content = "Условно аттестован"; break;
                }
                change_Of_Data.old_cause.Content = global.ROWS[global.Idx].Cause_idString;         // прежнее значение причины неаттестации

                change_Of_Data.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }

        private void connect_Click(object sender, RoutedEventArgs e)  // кнопка соединения с сервером
        {
            if (!global.transport.IsOpen) // проверяем соединение
            {
                try
                {
                    global.transport.Close();
                    global.transport.Open();
                    MessageBox.Show("Соединение с сервером восстановлено");
                    //global.GetSignIn();                                                    // авторизация
                    //name.Content = Global.ShortName(global.user);                          // выводим имя пользователя
                    /*if (global.user.Length > 0)
                    {*/
                        global.workAfterShutdown();                                        // восстановление после разрыва
                    //}
                    connect.Background = global.GreenColorStart;
                    textConnect.Text = "Соединение установленно";
                    toolTipConnect.Text = "Соединение с сервером установленно";
                }
                catch (Exception ass)
                {
                    //MessageBox.Show(ass.Message);
                    ExClose exClose = new ExClose(ass.ToString());
                    exClose.ShowDialog();
                }
            }
        }

        
    }
}