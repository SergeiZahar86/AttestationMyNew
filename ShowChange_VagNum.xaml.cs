using System;
using System.Windows;
using System.Windows.Input;

namespace Attestation
{
    public partial class ShowChange_VagNum : Window
    {
        private Global global;
        public ShowChange_VagNum()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // Валидация ввода, можно только цифры
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) // Валидация ввода, нельзя пробел
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e) // Корректировка номера вагона
        {
            String vag = textboxVag.Text;
            global.ROWS[global.Idx].Num = vag;

            global.client.setNum(global.part.Part_id, global.ROWS[global.Idx].Car_id, vag);   // Корректировка номера вагона на сервере	
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
