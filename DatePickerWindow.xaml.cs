using System;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class DatePickerWindow : Window
    {
        public DatePickerWindow()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = calendar1.SelectedDate;

            MessageBox.Show(selectedDate.Value.Date.ToShortDateString());
        }
    }
}
