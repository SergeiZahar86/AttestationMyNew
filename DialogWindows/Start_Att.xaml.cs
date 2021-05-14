using System;
using System.Windows;

namespace Attestation
{
    public partial class Start_Att : Window
    {
        private Global global;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;   

        public Start_Att()
        {
            InitializeComponent();
            global = Global.getInstance();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();                   
        }

        private void OnTimedEvent(Object source, EventArgs e)      
        {
            try
            {
                    global.GetGlobalPart(global.user);                     
                    dispatcherTimer.Stop();
                    this.Close();
            }
            catch (Exception sss)
            {
                dispatcherTimer.Stop();
                MessageBox.Show($"Ошибка при открытии аттестации \n {sss}"); ;
                this.Close();
            }
        }
    }
}
