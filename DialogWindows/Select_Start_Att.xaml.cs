using System.Windows;

namespace Attestation.DialogWindows
{
    public partial class Select_Start_Att : Window
    {
        private Global global;
        public Select_Start_Att()
        {
            InitializeComponent();
            global = Global.getInstance();
        }

        private void check_att_Checked(object sender, RoutedEventArgs e)
        {
            label_textboxCarrying.IsEnabled = false;
            textboxCarrying.IsEnabled = false;
        }

        private void check_att_Unchecked(object sender, RoutedEventArgs e)
        {
            label_textboxCarrying.IsEnabled = true;
            textboxCarrying.Text = "";
            textboxCarrying.IsEnabled = true;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(check_att.IsChecked == true)
            {
                global.normal_att = true;
                Close();
            }
            else
            {
                try
                {
                    if (int.Parse(textboxCarrying.Text) > 0 && int.Parse(textboxCarrying.Text) < 26)
                    {
                        global.how_many_wagons = int.Parse(textboxCarrying.Text);
                        global.normal_att = false;
                        Close();
                    }
                    else
                    {
                        validation.Text = "Введите от 1 до 25";
                        return;
                    }
                }
                catch
                {
                    validation.Text = "Введите от 1 до 25";
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
