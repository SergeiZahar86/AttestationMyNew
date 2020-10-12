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
    /// <summary>
    /// Логика взаимодействия для ShowChange_isOk.xaml
    /// </summary>
    public partial class ShowChange_isOk : Window
    {
        String cloce = "WindowClose";
        String check = "CheckCircle";
        String question = "Question";
        private Global global;
        public ShowChange_isOk()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            

            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ComboBox comboBox = (ComboBox)sender;
            String key = ((ComboBoxItem)comboBox.SelectedItem).Name;
            if(key == ok.Name)
            {
                global.DATA[global.Idx].isOk = check;
                //var converter = new System.Windows.Media.BrushConverter();
                //ok.Foreground = (Brush)converter.ConvertFromString("#38E05D");
            }
            else if(key == nothing.Name)
            {
                global.DATA[global.Idx].isOk = question;
            }
            else
            {
                global.DATA[global.Idx].isOk = cloce;
            }
            
        }
    }
}
