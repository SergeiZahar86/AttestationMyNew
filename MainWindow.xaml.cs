using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Attestation
{
    // Главный класс приложения
    public partial class MainWindow : Window
    {
        private Global global;
        SignIn signIn;
        //System.Windows.Threading.DispatcherTimer dispatcherTimer;   // Таймер

        /*private void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            if(global.getNumberCard() != global.numberCard)
            {
                global.GetSignIn();
            }
            
        }*/
        public MainWindow()
        {
            InitializeComponent();
            global = Global.getInstance();

            // Таймер ///////////////////////////////////////
            /*dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);*/
            /////////////////////////////////////////////////
        }
        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e) // начальная загрузка
        {
            //global.GetSignIn();
            /*signIn = new SignIn();
            signIn.Owner = Window.GetWindow(this);
            signIn.ShowDialog();*/
            label_fio.Content = Global.ShortName(global.user);

            if (!global.transport.IsOpen) // проверяем соединение
            {
                try
                {
                    global.transport.Close();
                    global.transport.Open();
                    MessageBox.Show("Соединение с сервером восстановлено");
                    //global.GetSignIn();                                                    // авторизация
                    signIn = new SignIn();
                    signIn.Owner = Window.GetWindow(this);
                    signIn.ShowDialog();
                }
                catch (Exception ass)
                {
                    ExClose exClose = new ExClose(ass.ToString());
                    exClose.ShowDialog();
                    //MessageBox.Show(ass.Message);
                }
            }
            else
            {
                /*global.GetSignIn();
                label_fio.Content = Global.ShortName(global.user);*/
            }
            //dispatcherTimer.Start();                                        // запуск таймара
            if (global.user.Length > 0)
            {
                global.workAfterShutdown(); // восстановление после разрыва
                /*try
                {
                    global.cause = global.client.getCauses();               // Запрос справочника причин неаттестации
                    global.contractors = global.client.getContractors();    // Запрос справочника контрагентов
                    global.mats = global.client.getMat();                   // Запрос справочника материалов
                    GetConsignees(global.contractors);                      // получение справочника Грузополучателя
                    GetShippers(global.contractors);                        // получение справочника Грузоотправителей
                    GetZonas();                                             // получение справочника Зоны вагонов
                    global.IsOk_Val = GetIsOk_Val();                        // справочник итогов аттестации
                    global.Att_codeFonts = GetAtt_codeFonts();              // справочник элементов шрифта для итогов аттестации

                    global.OldPart = global.client.getOldPart();                   // проверяем наличие незавершенных аттестаций(метод с сервера)
                    if (global.OldPart.Length > 0)
                    {
                        global.part = global.client.getPart(global.OldPart);                 // получаем незавершенную партию
                        global.startTimeStr = global.part.Start_time;                        // получение времени начала аттестации
                                                                                             //global.Shipper = global.shippers[global.part.Shipper].Name;          // получение  Грузоотправителя
                                                                                             //global.Consignee = global.consigners[global.part.Consigner].Name;    // получение Грузополучателя
                        global.PartId = global.part.Part_id;                                 // получение номера партии
                                                                                             //global.MatName = global.mats[global.part.Mat].Name;                  // получение названия материала

                        global.isColor = false;                                              // для кнопки начала и завершения аттестации 

                        ContinuationOfAttestation ofAttestation = new ContinuationOfAttestation();      // окно напоминания о незавершенной аттестации
                        ofAttestation.ShowDialog();
                    }
                }
                catch (Exception ss)
                {
                    ExClose exClose = new ExClose(ss.ToString());
                    exClose.ShowDialog();
                    //Application.Current.Shutdown();
                }*/
            }
                    AttestationPage p = new AttestationPage();
                    MainFrame.Navigate(p);
                    //p.name.Content = Global.ShortName(global.user);   // выводим имя пользователя

        }
        /*private void GetShippers(List<contractor_t> contr) // получение справочника Грузоотправителей
        {
            global.shippers = new List<Shippers>();
            int i = 1;
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Shipper)
                {
                    global.shippers.Add(new Shippers(i, contractor_.Name));
                }
                i++;
            }
        }
        private void GetConsignees(List<contractor_t> contr) // получение справочника Грузополучателя
        {
            global.consigners = new List<Consigners>();
            int i = 1;
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Consigner)
                {
                    global.consigners.Add(new Consigners(i, contractor_.Name));
                }
                i++;
            }
        }
        private void GetZonas() // получение справочника Зоны вагонов
        {
            global.zonas = new List<Zona>();
            global.zonas.Add(new Zona(1, "Зелёная"));
            global.zonas.Add(new Zona(2, "Жёлтая"));
            global.zonas.Add(new Zona(3, "Красная"));
        }
        private List<string> GetIsOk_Val() // справочник итогов аттестации
        {
            List<string> str = new List<string>
            {
                "Аттестован",
                "Не аттестован",
                "Условно аттестован"
            };
            return str;
        }
        private List<string> GetAtt_codeFonts() // справочник элементов шрифта для итогов аттестации
        {
            List<string> fonts = new List<string>(); 
            fonts.Add("CheckCircle");
            fonts.Add("WindowClose");
            fonts.Add("Asterisk");
            return fonts;
        }*/
        // Обработка события кнопок
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        { 
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);    
        }
        private void changePassword_Click(object sender, RoutedEventArgs e) // изменение пароля
        {
            changePassword dialog = new changePassword();
            dialog.ShowDialog();
            bool? ret = dialog.DialogResult;
            if (ret == true)
            {
                string login = dialog.LoginBool;

            }
        }
        private void MinButton_Click(object sender, RoutedEventArgs e) // сворачивание окна
        { 
            this.WindowState = WindowState.Minimized;
        }
        private void Attestation_Click(object sender, MouseButtonEventArgs e) // страница аттестации
        {
            var converter = new System.Windows.Media.BrushConverter();
            //BorderReport.BorderBrush= (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            //BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            //BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#00CC00");
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
            

        }
        private void Report_Click(object sender, MouseButtonEventArgs e) // страница отчетов
        {
            /*
            var converter = new System.Windows.Media.BrushConverter();
            //BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            //BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");

            ReportPage p = new ReportPage();
            MainFrame.Navigate(p);
            user.Text = "Ok";
            */
        }
        private void Archive_Click(object sender, MouseButtonEventArgs e) // страница архивов
        {
            var converter = new System.Windows.Media.BrushConverter();
            //BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            //BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");

            Archive p = new Archive();
            MainFrame.Navigate(p);
        }
        private void signIn_Click(object sender, RoutedEventArgs e) // кнопка авторизации
        {
            //global.GetSignIn();
            signIn = new SignIn();
            signIn.Owner = Window.GetWindow(this);
            signIn.ShowDialog();
        }
        /*private void GetSignIn() // Авторизация
        {
            SignIn signIn = new SignIn();
            signIn.ShowDialog();
            try
            {
                label_fio.Content = Global.ShortName(global.user);
            }
            catch
            {
            }
        }*/

    }
}
