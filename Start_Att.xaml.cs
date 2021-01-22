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
    public partial class Start_Att : Window
    {
        private Global global;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;   // Таймер
        //bool is_ok;

        public Start_Att()
        {
            //is_ok = false;
            InitializeComponent();
            global = Global.getInstance();
            //this.Close();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            ok.IsEnabled = false;
            dispatcherTimer.Start();                    // Стартуем таймер

        }

        private void OnTimedEvent(Object source, EventArgs e)      // Получение вагонов с сервера по таймеру
        {
            try
            {
                global.GetGlobalPart(global.user);                     // Начало аттестации, вызов метода startAtt() и получение партии вагонов
                //is_ok = true;
                ok.IsEnabled = true;
                dispatcherTimer.Stop();
            }
            catch
            {

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
    }
}
