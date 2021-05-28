using Attestation.DialogWindows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Thrift.Protocol;
using Thrift.Transport;
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
        public void UpdateDataGrid() // обновление таблицы
        {
            global.DATA = global.part.Cars;                        // получаем серверный список вагонов
            global.ROWS = global.GetRows();                        // получаем внутренний список вагонов
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
        }
        private async void OnTimedEvent(object source, EventArgs e)      // Получение вагонов с сервера по таймеру
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
                    state = global.GetStatusBits();
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        GetCollorStatusAtt(state); // установка цветов индикаторов статуса 
                        var fff = (DateTime.Now - global.startTime);
                        timeSpend.Text = fff.ToString(@"hh\:mm\:ss");
                    });

                    part_t part_ = new part_t();
                    car_t car_ = new car_t();
                    List<car_t> car_s = new List<car_t>();
                    car_s.Add(car_);
                    part_.Cars = car_s;

                    if (state.Task == 2)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            if (state.Inspection == 0)
                            {
                                EndAtt.IsEnabled = false;
                                EndAtt.Background = global.GreyColor;
                            }
                            if (state.Inspection == 1)
                            {
                                EndAtt.IsEnabled = true;
                                EndAtt.Background = global.GreenColor;
                            }
                            if (state.Inspection == 2)
                            {
                                EndAtt.IsEnabled = false;
                                EndAtt.Background = global.GreyColor;
                            }
                            if (state.Inspection == 3)
                            {
                                EndAtt.IsEnabled = false;
                                EndAtt.Background = global.GreyColor;
                            }
                        });




                        part_ = global.GetPart();

                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            DataGridMain.IsEnabled = true;
                            if (part_.Start_time_att != null)
                            {
                                if (part_.End_time_att != null)
                                {
                                    label_going_att.Visibility = Visibility.Hidden;

                                    NewTask.IsEnabled = true;
                                    CancelTask.IsEnabled = true;
                                    NewTaskRow_1.Text = "Сохранить";
                                    NewTask.Background = global.GreenColor;
                                    CancelTask.Background = global.RedColor;
                                }
                                else
                                {
                                    label_going_att.Visibility = Visibility.Visible;

                                    NewTask.IsEnabled = false;
                                    CancelTask.IsEnabled = true;
                                    NewTaskRow_1.Text = "Сохранить";
                                    NewTask.Background = global.GreyColor;
                                    CancelTask.Background = global.RedColor;
                                }
                            }
                            else
                            {
                                label_going_att.Visibility = Visibility.Hidden;

                                NewTask.IsEnabled = false;
                                CancelTask.IsEnabled = true;
                                NewTaskRow_1.Text = "Сохранить";
                                NewTask.Background = global.GreyColor;
                                CancelTask.Background = global.RedColor;
                            }
                        });

                    }
                    else if(state.Task == 0)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            NewTask.IsEnabled = true;
                            CancelTask.IsEnabled = false;
                            NewTaskRow_1.Text = "Создать";
                            NewTask.Background = global.GreenColor;
                            CancelTask.Background = global.GreyColor;
                            label_going_att.Visibility = Visibility.Hidden;

                            EndAtt.IsEnabled = false;
                            EndAtt.Background = global.GreyColor;
                            DataGridMain.IsEnabled = false;
                        });
                        
                    }
                    else if(state.Task == 1)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            NewTask.IsEnabled = false;
                            CancelTask.IsEnabled = false;
                            NewTaskRow_1.Text = "Создать";
                            NewTask.Background = global.GreyColor;
                            CancelTask.Background = global.GreyColor;
                            label_going_att.Visibility = Visibility.Hidden;

                            EndAtt.IsEnabled = false;
                            EndAtt.Background = global.GreyColor;
                            DataGridMain.IsEnabled = false;
                        });
                    }
                    else if (state.Task == 3)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            NewTask.IsEnabled = false;
                            CancelTask.IsEnabled = false;
                            NewTask.Background = global.GreyColor;
                            CancelTask.Background = global.GreyColor;
                            error.Text = "Нет связи с контроллером (очередью)";
                            label_going_att.Visibility = Visibility.Hidden;

                            EndAtt.IsEnabled = false;
                            EndAtt.Background = global.GreyColor;
                            DataGridMain.IsEnabled = false;
                        });
                    }


                    return part_;
                }
                if (global.transport.IsOpen) // проверяем соединение
                {
                    Task<part_t> t = Task.Run(get_part);
                    timer.Tag = t;
                    await t;

                    if (state.Task == 2)
                    {
                        global.part = t.Result;
                        global.startTimeStr = global.part.Start_time_att;
                        timeStart.Text = global.startTimeStr;


                        if (global.part != null)
                        {
                            if (global.part.Cars.Count == 1 && CountRow != global.part.Cars.Count)
                            {
                                UpdateDataGrid();
                                CountRow = 1;
                            }
                            else if (CountRow != global.part.Cars.Count)
                            {
                                UpdateDataGrid();
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
                                                    UpdateDataGrid();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        UpdateDataGrid();
                                        CountRow = global.ROWS.Count;
                                    }
                                }
                            }
                        }
                        error.Text = "";
                    }
                    else if(state.Task == 0)
                    {
                        DataGridMain.ItemsSource = null;

                    }
                    if(state.Task != 3)
                    {
                        error.Text = "";
                    }


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

        private async void check_is_open(object source, EventArgs e) // метод таймера при закрытой аттестации
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

        private async void ConnectTimer(object source, EventArgs e)      // таймер проверки соединения с сервером
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
            NewTask.Background = global.GreyColor;
            CancelTask.Background = global.GreyColor;
            EndAtt.Background = global.GreyColor;
            DataGridMain.IsEnabled = false;

            label_going_att.Visibility = Visibility.Hidden;
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
                NewTask.IsEnabled = false;
                CancelTask.IsEnabled = false;
            }




            allDataEntered = true;
            CountRow = 100;                                                        // для сравнения списков с целью выявления изменений
            is_Num_close_att = true;                                               // флаг количества цифр в номерах вагонов      
            // Таймеры ///////////////////////////////////////
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();







            checkIsOpen = new DispatcherTimer();
            checkIsOpen.Tick += new EventHandler(check_is_open);
            checkIsOpen.Interval = new TimeSpan(0, 0, 1);

            timerConnect = new DispatcherTimer();
            timerConnect.Tick += new EventHandler(ConnectTimer);
            timerConnect.Interval = new TimeSpan(0, 0, 2);

            

        }
        private void DataGridMain_Loaded(object sender, RoutedEventArgs e)            // загрузка данных в DataGrid
        {
            DataGridMain.ItemsSource = null;
            DataGridMain.ItemsSource = global.ROWS;
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
                    MessageBox.Show("У строки " + global.ROWS[global.Idx].Car_id.ToString() + " нет фотографий");
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

        private void DataGridMain_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)  // обработчик множественного ввода
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

        private void NewTask_Click(object sender, RoutedEventArgs e) // Кнопка создания или сохранения задания
        {
            if (NewTaskRow_1.Text == "Создать")
            {
                Thread myThread = new Thread(new ThreadStart(NewTaskFunction));
                myThread.Start(); 
            }
            else
            {
                Thread myThread = new Thread(new ThreadStart(EndTaskFunction));
                myThread.Start(); 
            }
        }
        private void NewTaskFunction() // Функция для Кнопки создания нового задания
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                blockBatton();
                showDialogMainWindow();
            });
            global.CreateTask(global.Login);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                hideDialogMainWindow();
            });
        }
        private void EndTaskFunction() // Функция для Кнопки сохранения задания
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                blockBatton();
                showDialogMainWindow();
            });
            global.EndTask(global.Login);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                hideDialogMainWindow();
            });
        }
        private void CloseProg_Click(object sender, RoutedEventArgs e) // выход из программы
        {
            Application.Current.Shutdown(); 
            Environment.Exit(0);
        }
        private void CancelTask_Click(object sender, RoutedEventArgs e) // Кнопка отмены задания
        {
            Thread myThread = new Thread(new ThreadStart(CancelTaskFunction));
            myThread.Start(); 
        }
        private void CancelTaskFunction() // Функция для Кнопки отмены задания
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                blockBatton();
                showDialogMainWindow();
            });
            global.RemoveTask(global.Login);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                hideDialogMainWindow();
            });
        }
        private void showDialogMainWindow() // показать диалоговое окно из главного окна
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;    // получить доступ к главному окну
            mainWindow.gridDialog.Visibility = Visibility.Visible;
            mainWindow.gridMain.IsEnabled = false;
            gridMain.IsEnabled = false;
        }
        private void hideDialogMainWindow() // скрыть диалоговое окно из главного окна
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;    // получить доступ к главному окну
            mainWindow.gridDialog.Visibility = Visibility.Hidden;
            mainWindow.gridMain.IsEnabled = true;
            gridMain.IsEnabled = true;
        }
        private void blockBatton() // блокировка кнопок
        {
            NewTask.IsEnabled = false;
            CancelTask.IsEnabled = false;
            NewTask.Background = global.GreyColor;
            CancelTask.Background = global.GreyColor;
        }
        private void EndAtt_Click(object sender, RoutedEventArgs e) // Кнопка завершения аттестации
        {
            verification(global.ROWS);
            if (allDataEntered)
            {
                Thread myThread = new Thread(new ThreadStart(EndAttFunction));
                myThread.Start();
            }
        }
        private void EndAttFunction() // Функция для Кнопки завершения аттестации
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                EndAtt.IsEnabled = false;
                EndAtt.Background = global.GreyColor;
                showDialogMainWindow();
            });


            TTransport transport = new TSocket(global.Host, global.Port);
            TProtocol proto = new TBinaryProtocol(transport);
            DataProviderService.Client client = new DataProviderService.Client(proto);
            try
            {
                transport.Open();
                client.endAtt(global.Login);
            }
            catch (Exception sa)
            {
                ExClose exClose = new ExClose(sa.ToString());
                exClose.ShowDialog();
            }
            finally
            {
                try
                {
                    transport.Close();
                }
                catch { }
            }


            //global.EndAtt(global.Login);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                hideDialogMainWindow();
            });
        }
    }
}
