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
    /// Логика взаимодействия для ShowChange_cause_t.xaml
    /// </summary>
    public partial class ShowChange_cause_t : Window
    {
        private Global global;
        private List<cause_t> Cause;
        public ShowChange_cause_t()
        {
            InitializeComponent();
            global = Global.getInstance();
            Cause = global.cause;
            cause_Value.ItemsSource = Cause;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cause_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.ROWS[global.Idx].Cause_idString = Cause[cause_Value.SelectedIndex].Name;
            global.ROWS[global.Idx].Cause_id = Cause[cause_Value.SelectedIndex].Id;
        }
    }
}
