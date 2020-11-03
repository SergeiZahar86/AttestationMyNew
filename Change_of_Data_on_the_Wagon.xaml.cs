using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Attestation
{
    public partial class Change_of_Data_on_the_Wagon : Window
    {
        private Global global;
        private List<Zona> zona_Val;
        private List<Shippers> shippersVal;
        private List<Consigners> consignersVal;
        private List<mat_t> matsVal;
        private List<String> isOk_Val;



        public Change_of_Data_on_the_Wagon()
        {
            global = Global.getInstance();
            InitializeComponent();

            zona_Val = global.zonas;
            zona_Value.ItemsSource = zona_Val;

            shippersVal = global.shippers;
            shipper_Value.ItemsSource = shippersVal;

            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;

            matsVal = global.mats;
            mat_Value.ItemsSource = matsVal;

            isOk_Val = global.IsOk_Val;
            isOk_Value.ItemsSource = isOk_Val;
        }

        private void Vag_PreviewTextInput(object sender, TextCompositionEventArgs e) // Валидация ввода, можно только цифры
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void Vag_PreviewKeyDown(object sender, KeyEventArgs e) // Валидация ввода, нельзя пробел
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }
        private void point_PreviewTextInput(object sender, TextCompositionEventArgs e) // Валидация ввода, можно только цифры
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!textboxTara.Text.Contains(".")
               && textboxTara.Text.Length != 0)))
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void point_PreviewKeyDown(object sender, KeyEventArgs e) // Валидация ввода, нельзя пробел
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }
        private void zona_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор зоны из справочника
        {
            global.ROWS[global.Idx].Zone_e = global.zonas[zona_Value.SelectedIndex].Id;
            global.ROWS[global.Idx].Zone_eString = global.zonas[zona_Value.SelectedIndex].Name;

        }
        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузоотправитель из справочника
        {
            global.ROWS[global.Idx].Shipper = global.shippers[shipper_Value.SelectedIndex].Id - 1;
            global.ROWS[global.Idx].Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;

        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузополучателя из справочника
        {
            global.ROWS[global.Idx].Consigner = global.consigners[consigner_Value.SelectedIndex].Id - 1;
            global.ROWS[global.Idx].Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;

        }
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор вида материала из справочника
        {
            global.ROWS[global.Idx].Mat = global.mats[mat_Value.SelectedIndex].Id - 1;
            global.ROWS[global.Idx].Mat_String = global.mats[mat_Value.SelectedIndex].Name;

        }
        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор итогов аттестации
        {
            global.ROWS[global.Idx].Att_code = isOk_Value.SelectedIndex;
            global.ROWS[global.Idx].Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];
        }
















        private void ok_Click(object sender, RoutedEventArgs e)
        {

            String vag = textboxVag.Text;
            global.ROWS[global.Idx].Num = vag;

            if (global.client.setNum(global.part.Part_id, global.ROWS[global.Idx].Car_id, vag)) // Корректировка номера вагона на сервере	
            {
                this.Close();
            }
            //////////////////////////////
            String tar = textboxTara.Text;
            string tarRepl = tar.Replace(".", ",");
            global.ROWS[global.Idx].Tara_e = Convert.ToDouble(tarRepl);
            global.ROWS[global.Idx].Tara_delta = Math.Round((global.ROWS[global.Idx].Tara - global.ROWS[global.Idx].Tara_e),
                3, MidpointRounding.AwayFromZero);
            if (global.client.setTara(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Tara_e))
            {
                this.Close();
            }
            /////////////////////////////////////////
            String car = textboxCarrying.Text;
            string carRepl = car.Replace(".", ",");
            global.ROWS[global.Idx].Carrying = Convert.ToDouble(carRepl);
            if (global.client.setCarry(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Carrying))
            {
                this.Close();
            }
            /////////////////////////////////////////
            if (global.client.setZone(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Zone_e))
            {
                this.Close();
            }
            /////////////////////////////////////////////
            if (global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Shipper))
            {
                this.Close();
            }
            /////////////////////////////////////////////////
            if (global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Shipper))
            {
                this.Close();
            }
            /////////////////////////////////////////////////////////
            if (global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Shipper))
            {
                this.Close();
            }
            ///////////////////////////////////////////////
            if (global.client.setAtt(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Att_code))
            {
                this.Close();
            }
            ///////////////////////////////////////////////////////
            
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
