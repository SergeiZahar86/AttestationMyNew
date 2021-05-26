using Attestation.DialogWindows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Attestation
{
    public partial class AttestationPage : Page
    {
        bool allDataEntered;                    // все необходимые данные введены
        // зеленый для элипсов
        byte r_GreenE = 36;
        byte g_GreenE = 201;
        byte b_GreenE = 133;

        // желтый
        byte r_Yellow = 255;
        byte g_Yellow = 241;
        byte b_Yellow = 118;


        // серый
        byte r_Grey = 160;
        byte g_Grey = 178;
        byte b_Grey = 187;

        // красный
        byte r_Red = 244;
        byte g_Red = 110;
        byte b_Red = 104;


        state_bits state;                                           // состояние аттестации, передача цветов для индикаторов
        public int idx;                                             // индекс строки
        private Global global;
        public static bool isVerification;                          // флаг для подтверждения окончания аттестации
        DispatcherTimer dispatcherTimer;   // Таймер аттестации
        DispatcherTimer timerConnect;
        DispatcherTimer checkIsOpen;
        MqttClient client;
        RowTab row;
        private bool is_Num_close_att;                              // флаг количества цифр в номерах вагонов
        private int CountRow;                                     // для сравнения списков с целью выявления изменений
        private bool chenge;



        private void SetRedStatusAtt() // установка красного цвета для трёх индикаторов статуса аттестации
        {
            task_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
            inspection_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
            weight_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
            load_canva.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
        }

        private void GetCollorStatusAtt(state_bits state) // установка цветов трёх индикаторов статуса аттестации
        {
            int task = state.Task;
            int inst = state.Inspection;
            int weight = state.Weight;
            int load = state.Load;
            switch (task)
            {
                case 0:
                    task_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Grey, g_Grey, b_Grey));
                    break;
                case 1:
                    task_canvas.Background = new SolidColorBrush(Color.FromRgb(r_GreenE, g_GreenE, b_GreenE));
                    break;
                case 2:
                    task_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Yellow, g_Yellow, b_Yellow));
                    break;
                case 3:
                    task_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
                    break;
            }
            switch (inst)
            {
                case 0:
                    inspection_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Grey, g_Grey, b_Grey));
                    break;
                case 1:
                    inspection_canvas.Background = new SolidColorBrush(Color.FromRgb(r_GreenE, g_GreenE, b_GreenE));
                    break;
                case 2:
                    inspection_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Yellow, g_Yellow, b_Yellow));
                    break;
                case 3:
                    inspection_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
                    break;
            }
            switch (weight)
            {
                case 0:
                    weight_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Grey, g_Grey, b_Grey));
                    break;
                case 1:
                    weight_canvas.Background = new SolidColorBrush(Color.FromRgb(r_GreenE, g_GreenE, b_GreenE));
                    break;
                case 2:
                    weight_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Yellow, g_Yellow, b_Yellow));
                    break;
                case 3:
                    weight_canvas.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
                    break;
            }
            switch (load)
            {
                case 0:
                    load_canva.Background = new SolidColorBrush(Color.FromRgb(r_Grey, g_Grey, b_Grey));
                    break;
                case 1:
                    load_canva.Background = new SolidColorBrush(Color.FromRgb(r_GreenE, g_GreenE, b_GreenE));
                    break;
                case 2:
                    load_canva.Background = new SolidColorBrush(Color.FromRgb(r_Yellow, g_Yellow, b_Yellow));
                    break;
                case 3:
                    load_canva.Background = new SolidColorBrush(Color.FromRgb(r_Red, g_Red, b_Red));
                    break;
            }
        }
        private async void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            DispatcherTimer timer = (DispatcherTimer)source;
            if (null != timer.Tag)
            {
                return;
            }

            try
            {
                part_t get_part()
                {
                    state = global.client.getStatusBits();
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        GetCollorStatusAtt(state); // установка цветов трёх индикаторов статуса аттестации
                        var fff = (DateTime.Now - global.startTime);
                        timeSpend.Text = fff.ToString(@"hh\:mm\:ss");
                    });

                    return global.client.getPart(global.PartId);
                }
                if (global.transport.IsOpen) // проверяем соединение
                {
                    Task<part_t> t = Task<JObject>.Run(get_part);
                    timer.Tag = t;
                    await t;
                    global.part = t.Result;
                    global.startTimeStr = global.part.Start_time_att;
                    timeStart.Text = global.startTimeStr;


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
                        else
                        {
                            for (int i = 0; i < global.part.Cars.Count; i++)
                            {
                                if (global.DATA != null)
                                {
                                    for (int p = 0; p < global.DATA.Count; p++)
                                    {
                                        if (i == p)
                                        {
                                            var c1 = Math.Round(global.part.Cars[i].Tara, 3).ToString();
                                            var c2 = Math.Round(global.DATA[p].Tara, 3).ToString();


                                            if (c1 != c2 || global.part.Cars[i].Zone_e.ToString() != global.DATA[p].Zone_e.ToString()
                                                || global.part.Cars[i].Tara_e.ToString() != global.DATA[p].Tara_e.ToString())
                                            {
                                                global.DATA = global.part.Cars;                        // получаем серверный список вагонов
                                                global.ROWS = global.GetRows();                        // получаем внутренний список вагонов
                                                                                                       //observable = global.ROWS;
                                                DataGridMain.ItemsSource = null;
                                                DataGridMain.ItemsSource = global.ROWS;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
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
                    }
                    error.Text = "";
                }
                else
                {
                    dispatcherTimer.Stop();
                    timerConnect.Start();
                    error.Text = $"Ошибка при обмене данными с DataProvider при получение вагонов с сервера";
                    SetRedStatusAtt();
                }
            }
            catch (Exception ass)
            {
                SetRedStatusAtt();
            }
            finally
            {
                timer.Tag = null;
            }
        }

        private async void check_is_open(Object source, EventArgs e) // метод таймера при закрытой аттестации
        {
            state_bits GetState_bits()
            {
                state = global.client.getStatusBits();
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    GetCollorStatusAtt(state); // установка цветов трёх индикаторов статуса аттестации
                });
                return state;

            }
            DispatcherTimer timer = (DispatcherTimer)source;
            if (null != timer.Tag)
            {
                return;
            }
            try
            {
                if (global.transport.IsOpen == true)
                {
                    //Task<List<mat_t>> t = Task<List<mat_t>>.Run(() => global.mats = global.client.getMat());
                    Task<state_bits> t = Task<state_bits>.Run(GetState_bits);
                    timer.Tag = t;
                    await t;
                    //global.mats = t.Result;
                    state = t.Result;
                }
                else
                {
                    error.Text = "Нет связи с DataProvider \n";
                    SetRedStatusAtt();
                    checkIsOpen.Stop();
                    timerConnect.Start();
                }
            }
            catch (Exception ass)
            {
                error.Text = $"Ошибка при обмене данными с DataProvider    {ass}";
                SetRedStatusAtt();
            }
            finally
            {
                timer.Tag = null;
            }
        }

        private async void ConnectTimer(Object source, EventArgs e)      // таймер проверки соединения с сервером
        {
            DispatcherTimer timer = (DispatcherTimer)source;
            if (null != timer.Tag)
            {
                return;
            }
            if (!global.transport.IsOpen) // проверяем соединение
            {
                try
                {
                    global.transport.Close();
                    Task<String> t = Task<String>.Run(() => { global.transport.Open(); return "OK"; });
                    timer.Tag = t;
                    await t;
                    global.workAfterShutdown();                                        // восстановление после разрыва
                    error.Text = "";
                    timerConnect.Stop();
                    if(global.isColor)
                        checkIsOpen.Start();
                    else dispatcherTimer.Start();
                }
                catch (Exception ass)
                {
                    SetRedStatusAtt();
                    error.Text = ass.ToString();
                    return;
                }
                finally
                {
                    timer.Tag = null;
                }
            }
            else
            {
                error.Text = "";
            }
        }
        public AttestationPage() // конструктор
        {
            InitializeComponent();
            global = Global.getInstance();

            //label_going_att.Visibility = Visibility.Hidden;
            if (global.ArmAttestation)
            {
                NewTask.Visibility = Visibility.Hidden;
                CancelTask.Visibility = Visibility.Hidden;
                CloseProg.Visibility = Visibility.Hidden;
            }
            else
            {
                EndAtt.Visibility = Visibility.Hidden;
                textBlock_time_start.Visibility = Visibility.Hidden;
                textBlock_time_duration.Visibility = Visibility.Hidden;
                timeStart.Visibility = Visibility.Hidden;
                timeSpend.Visibility = Visibility.Hidden;
            }




            allDataEntered = true;
            CountRow = 100;                                                        // для сравнения списков с целью выявления изменений
            is_Num_close_att = true;                                            
            // Таймеры ///////////////////////////////////////
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            checkIsOpen = new System.Windows.Threading.DispatcherTimer();
            checkIsOpen.Tick += new EventHandler(check_is_open);
            checkIsOpen.Interval = new TimeSpan(0, 0, 1);

            timerConnect = new System.Windows.Threading.DispatcherTimer();
            timerConnect.Tick += new EventHandler(ConnectTimer);
            timerConnect.Interval = new TimeSpan(0, 0, 2);
            if (!global.transport.IsOpen) // проверяем соединение
            {
                //connect.Background = global.RedColorEnd;
                //textConnect.Text = "Восстановить соединение";
                //toolTipConnect.Text = "Нажмите чтобы восстановить соединение с сервером";
            }
            else
            {
                //connect.Background = global.GreenColorStart;
                //textConnect.Text = "Соединение установленно";
                //toolTipConnect.Text = "Соединение с сервером установленно";
                
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            isVerification = false;                                       // флаг для подтверждения окончания аттестации
            DataGridMain.IsEnabled = global.isEnabled;                    // флаг кликабельности datagrid
            //////// MQTT ///////////////////////////
            ///
            try
            {
                /*client = new MqttClient(global.Mqtt_host, 1883, false, null, null, MqttSslProtocols.None); // подключение к серверу ИндасХолдинг у Коли
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; // этот код запускается при получении сообщения
                client.Connect("ArmOTK", global.Mqtt_user, global.Mqtt_password); // подключение к серверу ИндасХолдинг у Коли
                string Topic = global.PLC_topic;
                client.Subscribe(new string[] { Topic }, new byte[] { 0 });*/
            }
            catch (Exception ass)
            {
                /*ExClose exClose = new ExClose(ass.ToString());
                exClose.Owner = Window.GetWindow(this);
                exClose.ShowDialog();*/
            }

            if (!global.isColor)
            {
                global.mainButtonAttestation = "Закончить";
                //================================================================================================================
                //startRow_1.Text = global.mainButtonAttestation;           // текст в кнопке аттестации
                //StartAttestation.Background = global.RedColorEnd;         // красный
                while (checkIsOpen.IsEnabled == true)
                {
                    checkIsOpen.Stop();
                    Thread.Sleep(100);
                }
                dispatcherTimer.Start();                                  // Стартуем таймер
            }
            else
            {
                while (dispatcherTimer.IsEnabled == true)
                {
                    dispatcherTimer.Stop();
                    Thread.Sleep(100);
                }
                checkIsOpen.Start();
            }
            timeStart.Text = global.startTimeStr;                         // время начала
            //part_idTextBlock.Text = global.PartId;                        // Номер партии

        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)  // этот код запускается при получении сообщения от mqtt
        {
            /*try
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
            }*/
        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e)            // загрузка данных в DataGrid
        {
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private void StartAttestation_Click(object sender, RoutedEventArgs e)   // Кнопка начала аттестации
        {
            allDataEntered = true;
            try
            {

                if (global.isColor) // проверяем флаг
                {
                    Select_Start_Att select_Start = new Select_Start_Att();
                    select_Start.Owner = Window.GetWindow(this);
                    select_Start.ShowDialog();
                    if (!global.getStartAtt)
                    {
                        return;
                    }
                    CountRow = 100;
                    checkIsOpen.Stop();
                   
                    //////////// Установка времени ///////////////////////////////////////////////////////
                    global.startTime = DateTime.Now;                       // Запись текущего времени
                    global.startTimeStr = null;                            // Начало аттестации партии вагонов для страницы Аттестации
                    global.endTimeStr = null;                              // Окончание аттестации для страницы Аттестации
                    global.deltaTimeStr = null;                            // Продолжительность прохождения аттестации для страницы Аттестации 
                    //global.startTimeStr = global.startTime.ToString();     // Время начала аттестации (глобал)
                    //timeStart.Text = global.startTimeStr;                  // Время начала аттестации (вверху страницы)
                                                                           ///////////////////////////////////////////////////////////////////////

                    isVerification = false;                              // флаг для подтверждения окончания аттестации
                    global.isEnabled = true;                               // флаг кликабельности datagrid
                    DataGridMain.IsEnabled = global.isEnabled;             // разрешаю кликабельность в datagrid


                    /* Запрос партии вагонов */
                    Start_Att start = new Start_Att();
                    start.Owner = Window.GetWindow(this);
                    start.ShowDialog();
                    if (global.part == null)
                    {
                        return;
                    }
                    try
                    {
                        global.PartId = global.part.Part_id.ToString();              // Номер партии
                        //part_idTextBlock.Text = global.part.Part_id.ToString();      // Номер партии
                        //=======================================================================================================
                        //StartAttestation.Background = global.RedColorEnd;            // красный

                        global.mainButtonAttestation = "Закончить";
                        //=======================================================================================================
                        //startRow_1.Text = global.mainButtonAttestation;

                        DataGridMain.ItemsSource = null;
                        DataGridMain.ItemsSource = global.ROWS;     // Привязка вагонов к datagrid

                        global.IdConsigner = null;                  // id Грузополучателя для диалогового окна input_Of_Initial_Data при начале аттестации
                        global.IdShipper = null;                    // id Грузоотправителя  для диалогового окна input_Of_Initial_Data при начале аттестации
                        global.IdMat = null;                        // id материала для диалогового окна input_Of_Initial_Data при начале аттестации

                        global.isColor = false;                     // флаг для кнопки начала и завершения аттестации

                        while (checkIsOpen.IsEnabled == true)
                        {
                            Thread.Sleep(100);
                        }
                        dispatcherTimer.Start();                    // Стартуем таймер

                    }
                    catch (Exception sss)
                    {
                        MessageBox.Show(sss.Message);
                    }
                }
                
                else
                {
                    verification(global.ROWS);
                    if (allDataEntered)
                    {
                        is_Num_close_att = true;
                        VerificationEndAttestation ver = new VerificationEndAttestation();    // окно подтверждения окончания аттестации
                        ver.Owner = Window.GetWindow(this);
                        ver.ShowDialog();
                        if (isVerification)
                        {
                            bool ok = true;
                            foreach (RowTab ff in global.ROWS)
                            {
                                if (ff.Num.Length != 8) // делаем проверку длины номера вагона
                                {
                                    is_Num_close_att = false;
                                    MessageBox.Show("Номера вагонов должны состоять из восьми цифр (АРМ)");
                                    ok = false;
                                    break;
                                }
                            }
                            if (ok)
                            {
                                while (dispatcherTimer.IsEnabled == true)
                                {
                                    dispatcherTimer.Stop();                                           // Останавливает таймер
                                    Thread.Sleep(100);
                                }
                                Close_Att close = new Close_Att(); // внутри отправка данных на сервер
                                close.Owner = Window.GetWindow(this);
                                close.ShowDialog();
                            }
                        }
                        if (global.is_ok_close_att && is_Num_close_att)            // метод bool exitAtt() подтверждение окончания аттестации
                        {

                            global.endTime = DateTime.Now;                                    // Окончание аттестации 
                            global.endTimeStr = null;                                         // Окончание аттестации (String) для страницы Аттестации
                            global.endTimeStr = global.endTime.ToString();                    /* Перезапись времени окончания в Глобал в виде строки 
                                                                                       для дальнейшей записи в объект car_t и передачи на сервер*/
                            global.deltaTime = global.endTime.Subtract(global.startTime);     // Подсчёт продолжительности аттестации
                            global.deltaTimeStr = global.deltaTime.ToString(@"hh\:mm\:ss");   // затраченное время записывается в Глобал
                            //==================================================================================================
                            //StartAttestation.Background = global.GreenColorStart;             // зеленый

                            global.mainButtonAttestation = "Начать";
                            //=====================================================================================================
                            //startRow_1.Text = global.mainButtonAttestation;

                            global.isColor = true;                                            // флаг для кнопки начала и завершения аттестации
                            global.isEnabled = false;                                         // флаг кликабельности datagrid
                            DataGridMain.IsEnabled = global.isEnabled;                        // убирается кликабельность с datagrid
                            timeSpend.Text = "";
                            timeStart.Text = "";
                            //part_idTextBlock.Text = "";

                            global.ROWS = null;
                            DataGridMain.ItemsSource = null;
                            DataGridMain.ItemsSource = global.ROWS;

                            checkIsOpen.Start();
                        }
                        else
                        {
                            dispatcherTimer.Start(); // если не смогли закрыть аттестацию 
                            MessageBox.Show("Ошибка при отправки данных в DataProvider");
                        }
                    }
                }
            }
            catch (Exception ass)
            {
                error.Text = $"Ошибка при обмене данными с DataProvider    {ass}";
            }
        }
        private void Foto_Click(object sender, RoutedEventArgs e)               // выводит окно с фотографиями вагонов
        {
            try
            {
                global.Idx = DataGridMain.SelectedIndex;
                global.photo = global.getPhoto(global.part.Part_id, global.ROWS[global.Idx].Car_id);
                if (global.photo.Left != null || global.photo.Right != null || global.photo.Top != null)
                {
                    ShowPhotos showPhotos = new ShowPhotos();
                    showPhotos.Owner = Window.GetWindow(this);
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
                exClose.Owner = Window.GetWindow(this);
                exClose.ShowDialog();
            }
        }
        private void Change_VagNum(object sender, RoutedEventArgs e)            // Изменение номера вагона
        {
            try
            {
                ShowChange_VagNum showChange_VagNum = new ShowChange_VagNum();
                showChange_VagNum.Owner = Window.GetWindow(this);
                global.Idx = DataGridMain.SelectedIndex;
                showChange_VagNum.oldVagNum.Content = global.ROWS[global.Idx].Num;
                showChange_VagNum.Number.Text =$"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
                showChange_VagNum.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_isOk(object sender, RoutedEventArgs e)              // Изменение итогов аттестации
        {
            ShowChange_isOk showChange_IsOk = new ShowChange_isOk();
            showChange_IsOk.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            showChange_IsOk.Number.Text = $"вагон №  {global.ROWS[global.Idx].Car_id.ToString()}";
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
            showChange_Cause_T.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Cause_T.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
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
                showChange_Tara_e.Owner = Window.GetWindow(this);
                global.Idx = DataGridMain.SelectedIndex;
                showChange_Tara_e.oldTara_e.Content = global.ROWS[global.Idx].Tara_e;
                showChange_Tara_e.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
                showChange_Tara_e.ShowDialog();
                DataGridMain.ItemsSource = null;
                DataGridMain.ItemsSource = global.ROWS;
            }
            catch { }
        }
        private void Change_Carrying(object sender, RoutedEventArgs e)          // изменение Грузоподъемность
        {
            ShowChange_Carrying showChange_Carrying = new ShowChange_Carrying();
            showChange_Carrying.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            try
            {
                global.rowTab = (RowTab)((Button)e.Source).DataContext;  // получение объекта вагона ко клику
                showChange_Carrying.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";            // порядковый номер вагона в шапке окна
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
            showChange_Zone_eString.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            showChange_Zone_eString.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
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
            Change_Shipper_String.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            Change_Shipper_String.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
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
            Change_Consigner_String.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            Change_Consigner_String.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
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
            Change_Mat_String.Owner = Window.GetWindow(this);
            global.Idx = DataGridMain.SelectedIndex;
            Change_Mat_String.Number.Text = $"вагон № {global.ROWS[global.Idx].Car_id.ToString()}";
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
            change_Of_Data.Owner = Window.GetWindow(this);
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

        private void DataGridMain_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<int> num = new List<int>();
            List<string> numStr = new List<string>();
            IList rows = DataGridMain.SelectedItems;
            ObservableCollection<RowTab> rowsNum = new ObservableCollection<RowTab>();
            foreach (RowTab row in rows)
            {
                rowsNum.Add(row);
                num.Add(row.Car_id);
                numStr.Add(row.Car_id.ToString());


            }
            //string[] array = numStr.ToArray();
            string numV_str = string.Join(",", numStr.ToArray());

            MultipleDataEntry dataEntry = new MultipleDataEntry(numV_str, rowsNum);
            dataEntry.Owner = Window.GetWindow(this);
            dataEntry.ShowDialog();


            /*MessageBox.Show(numV_str);
            foreach(int p in num)
            {
                MessageBox.Show(p.ToString());
            }*/
        }

        private void verification(ObservableCollection<RowTab> rows) // проверка полноты введенных данных в таблицу
        {
            for(int i = 0; i<rows.Count; i++)
            {

                if(rows[i].Tara_e == 0 || rows[i].Carrying == 0 || rows[i].Consigner ==0 || rows[i].Shipper == 0)
                {
                    allDataEntered = false;
                    DataIsNotEntered dataIsNot = new DataIsNotEntered(i + 1);
                    dataIsNot.Owner = Window.GetWindow(this);
                    dataIsNot.ShowDialog();
                    return;
                }
            }
            allDataEntered = true;
        }
    }
}
