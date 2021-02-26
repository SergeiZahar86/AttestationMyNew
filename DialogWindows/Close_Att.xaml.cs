using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Attestation
{
    public partial class Close_Att : Window
    {
        private Global global;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;   // Таймер

        public Close_Att()
        {
            InitializeComponent();
            global = Global.getInstance();
            //this.Close();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //ok.IsEnabled = false;
            global.is_ok_close_att = false;             // флаг для закрытия аттестации
            dispatcherTimer.Start();                    // Стартуем таймер

        }

        private void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            try
            {

                if (global.exitAtt(global.part.Part_id) && global.setUser(global.part.Part_id, global.user))            // метод bool exitAtt() подтверждение окончания аттестации
                {
                    //ok.IsEnabled = true;
                    dispatcherTimer.Stop();
                    global.is_ok_close_att = true;
                    this.Close();
                }
            }
            catch
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Ошибка при закрытии аттестации");
                this.Close();
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        {
            this.Close();
            /*Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);*/
        }
        private void close_Click(object sender, RoutedEventArgs e) // закрыть программу
        {
            VerificationCloseProgram closeProgram = new VerificationCloseProgram();
            closeProgram.ShowDialog();
            this.Close();
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);
        }


       /* public Close_Att()
        {
            InitializeComponent();
            global = Global.getInstance();
            //this.Close();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //ok.IsEnabled = false;
            global.is_ok_close_att = false;             // флаг для закрытия аттестации
            dispatcherTimer.Start();                    // Стартуем таймер

        }

        private void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            try
            {

                if (global.exitAtt(global.part.Part_id) && global.setUser(global.part.Part_id, global.user))            // метод bool exitAtt() подтверждение окончания аттестации
                {
                    //ok.IsEnabled = true;
                    dispatcherTimer.Stop();
                    global.is_ok_close_att = true;
                    this.Close();
                }
            }
            catch
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Ошибка при закрытии аттестации");
                this.Close();
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e) // кнопка Применить
        {
            this.Close();
            *//*Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);*//*
        }
        private void close_Click(object sender, RoutedEventArgs e) // закрыть программу
        {
            VerificationCloseProgram closeProgram = new VerificationCloseProgram();
            closeProgram.ShowDialog();
            this.Close();
            Application.Current.Shutdown(); // выход из программы
            Environment.Exit(0);
        }*/

    }
}
