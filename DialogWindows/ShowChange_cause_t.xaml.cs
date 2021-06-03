using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_cause_t : Window
    {
        private Global global;
        private List<cause_t> Cause;
        private int Cause_id;
        private string Cause_idString;

        public ShowChange_cause_t()
        {
            InitializeComponent();
            global = Global.getInstance();
            Cause = global.cause;
             cause_Value.ItemsSource = Cause;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (global.client.setCause(global.part.Part_id, global.ROWS[global.Idx].Car_id, Cause_id))
                {
                    global.ROWS[global.Idx].Cause_idString = Cause_idString;
                    this.Close();
                }
            }
            catch
            {
                TextInput.Text = "Ошибка отправки на сервер";
            }
}
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cause_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор причины неаттестации
        {
            Cause_id = Cause[cause_Value.SelectedIndex].Id;
            Cause_idString = Cause[cause_Value.SelectedIndex].Name;
        }
    }
}
