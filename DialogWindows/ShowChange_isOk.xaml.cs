using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Attestation
{
    public partial class ShowChange_isOk : Window
    {
        private Global global;
        private List<String> isOk_Val;
        private int Att_code;
        private string Att_codeString;

        public ShowChange_isOk()
        {
            InitializeComponent();
            global = Global.getInstance();
            isOk_Val = global.IsOk_Val;
            isOk_Value.ItemsSource = isOk_Val;
        }
        private void Ok_Click(object sender, RoutedEventArgs e) // Корректировка признака аттестации на сервере
        {
            try
            {
                if (global.client.setAtt(global.part.Part_id, global.ROWS[global.Idx].Car_id, Att_code))
                {
                    global.ROWS[global.Idx].Att_codeString = Att_codeString;
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
        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Att_code = isOk_Value.SelectedIndex;
            Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];
        }
    }
}
