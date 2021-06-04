using System;
using System.Windows;
using System.Windows.Input;

namespace Attestation
{
    public partial class ShowChange_Tara_e : Window
    {
        private Global global;

        public ShowChange_Tara_e()
        {
            InitializeComponent();
            global = Global.getInstance();
            FocusManager.SetFocusedElement(this, textboxVag);     // set logical focus
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // Валидация ввода, можно только цифры
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!textboxVag.Text.Contains(".")
               && textboxVag.Text.Length != 0)))
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
                String tar = textboxVag.Text;
                string tarRepl = tar.Replace(".", ",");
                global.ROWS[global.Idx].Tara_e = Convert.ToDouble(tarRepl);
                global.ROWS[global.Idx].Tara_delta = Math.Round((global.ROWS[global.Idx].Tara - global.ROWS[global.Idx].Tara_e),
                    3, MidpointRounding.AwayFromZero);
                if (global.client.setTara(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Tara_e))
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
