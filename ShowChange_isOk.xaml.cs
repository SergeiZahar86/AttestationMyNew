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
        /*
        String cloce = "WindowClose";
        String check = "CheckCircle";
        String question = "Question";
        */
        private Global global;
        private List<String> IsOk_Val;

        public ShowChange_isOk()
        {
            InitializeComponent();
            global = Global.getInstance();
            IsOk_Val = new List<string>();
            IsOk_Val.Add("Аттестован");
            IsOk_Val.Add("Не аттестован");
            IsOk_Val.Add("Условно аттестован");
            isOk_Value.ItemsSource = IsOk_Val;
            //IsOk_Val = new List<string> { "Аттестован", "Не аттестован", "Условно аттестован" };
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
            // = IsOk_Val[isOk_Value.SelectedIndex] ;
            global.DATA[global.Idx].Att_code = isOk_Value.SelectedIndex;







            /*
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
            */
        }
    }
}
