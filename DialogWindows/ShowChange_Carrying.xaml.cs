using System;
using System.Windows;
using System.Windows.Input;

namespace Attestation
{
    public partial class ShowChange_Carrying : Window
    {
        private Global global;
        public ShowChange_Carrying()
        {
            InitializeComponent();
            global = Global.getInstance();
            FocusManager.SetFocusedElement(this, textboxCarrying);     // set logical focus
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // Валидация ввода, можно только цифры
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!textboxCarrying.Text.Contains(".")
               && textboxCarrying.Text.Length != 0)))
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
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String car = textboxCarrying.Text;
                string carRepl = car.Replace(".", ",");
                global.ROWS[global.Idx].Carrying = Math.Round(Convert.ToDouble(carRepl), 3, MidpointRounding.AwayFromZero);
                if (global.client.setCarry(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Carrying))
                {
                    this.Close();
                }
            }catch
            {
                result.Text = "Ошибка отправки на сервер";
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
