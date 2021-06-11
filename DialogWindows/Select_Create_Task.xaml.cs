using System.Windows;

namespace Attestation.DialogWindows
{
    public partial class Select_Create_Task : Window
    {
        private Global global;
        public Select_Create_Task()
        {
            InitializeComponent();
            global = Global.getInstance();
        }

        private void check_att_Checked(object sender, RoutedEventArgs e)
        {
            global.pusherPosition = PusherPosition.FROM_FRONT;
        }

        private void check_att_Unchecked(object sender, RoutedEventArgs e)
        {
            global.pusherPosition = PusherPosition.FROM_BEHIND;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            global.getStartTask = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            global.getStartTask = false;
            Close();
        }

    }

}
