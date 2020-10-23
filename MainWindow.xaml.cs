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

        public MainWindow()
        {

            InitializeComponent();
            global = Global.getInstance();
        }
        private void GetShippers(List<contractor_t> contr) // получение справочника Грузоотправителей
        {
            global.shippers = new List<Shippers>();
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Shipper)
                {
                    global.shippers.Add(new Shippers(contractor_.Id, contractor_.Name));
                }
            }
        }
        private void GetConsignees(List<contractor_t> contr) // получение справочника Грузополучателя
        {
            global.consignees = new List<Consignees>();
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Consigner)
                {
                    global.consignees.Add(new Consignees(contractor_.Id, contractor_.Name));
                }
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
        private List<string> GetAtt_codeFonts()
        {
            List<string> fonts = new List<string>(); // справочник элементов шрифта для итогов аттестации
            fonts.Add("CheckCircle");
            fonts.Add("WindowClose");
            fonts.Add("Asterisk");
            return fonts;
        }
        // Обработка события кнопок
        private void CloseButton_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown(); // выход из программы

        private void changePassword_Click(object sender, RoutedEventArgs e) // изменение пароля
        {
            changePassword dialog = new changePassword();
            dialog.ShowDialog();
            bool? ret = dialog.DialogResult;
            if (ret == true)
            {
                string login = dialog.Login;

            }
        }
        private void MinButton_Click(object sender, RoutedEventArgs e) =>  this.WindowState = WindowState.Minimized;


        private void Attestation_Click(object sender, MouseButtonEventArgs e) // страница аттестации
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush= (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            //BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#00CC00");
            AttestationPage p = new AttestationPage();
            MainFrame.Navigate(p);
            

        }
        private void Report_Click(object sender, MouseButtonEventArgs e) // страница отчетов
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#37474F");

            ReportPage p = new ReportPage();
            MainFrame.Navigate(p);
            user.Text = "Ok";
        }
        private void Archive_Click(object sender, MouseButtonEventArgs e) // страница архивов
        {
            var converter = new System.Windows.Media.BrushConverter();
            BorderReport.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderAttestation.BorderBrush = (Brush)converter.ConvertFromString("#37474F");
            BorderArchive.BorderBrush = (Brush)converter.ConvertFromString("#CC0000");

            Archive p = new Archive();
            MainFrame.Navigate(p);
        }

        private void GlobalWindow_Loaded(object sender, RoutedEventArgs e) // начальная загрузка
        {
            //AttestationPage p = new AttestationPage();
            //MainFrame.Navigate(p);
            
            GetSignIn();
            if (global.user.Length > 0)
            {
                global.cause = global.client.getCauses();               // Запрос справочника причин неаттестации
                global.contractors = global.client.getContractors();    // Запрос справочника контрагентов
                global.mats = global.client.getMat();                   // Запрос справочника материалов
                GetConsignees(global.contractors);                      // получение справочника Грузополучателя
                GetShippers(global.contractors);                        // получение справочника Грузоотправителей
                GetZonas();                                             // получение справочника Зоны вагонов
                global.IsOk_Val = GetIsOk_Val();                        // справочник итогов аттестации
                global.Att_codeFonts = GetAtt_codeFonts();              // справочник элементов шрифта для итогов аттестации

                AttestationPage p = new AttestationPage();
                MainFrame.Navigate(p);
            }
            
        }

        private void signIn_Click(object sender, RoutedEventArgs e) // кнопка авторизации
        {
            GetSignIn();
        }
        private void GetSignIn() // Авторизация
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
        }

    }
}
