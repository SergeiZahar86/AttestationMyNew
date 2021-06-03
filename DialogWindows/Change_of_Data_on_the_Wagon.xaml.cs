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
        private List<string> isOk_Val;
        private List<cause_t> Cause;

        private int Shipper;
        private string Shipper_String;
        private int Consigner;
        private string Consigner_String;
        private int Mat;
        private string Mat_String;
        private int Att_code;
        private string Att_codeString;
        private int Cause_id;
        private string Cause_idString;




        public Change_of_Data_on_the_Wagon()
        {
            global = Global.getInstance();
            InitializeComponent();
            if (global.ArmAttestation)
            {
                textboxTara.IsEnabled = false;
                textboxCarrying.IsEnabled = false;
                shipper_Value.IsEnabled = false;
                consigner_Value.IsEnabled = false;
                mat_Value.IsEnabled = false;
            }
            else
            {
                textboxVag.IsEnabled = false;
                isOk_Value.IsEnabled = false;
                cause_Value.IsEnabled = false;
            }
            zona_Val = global.zonas;
            //zona_Value.ItemsSource = zona_Val;

            shippersVal = global.shippers;
            shipper_Value.ItemsSource = shippersVal;

            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;

            matsVal = global.mats;
            mat_Value.ItemsSource = matsVal;

            isOk_Val = global.IsOk_Val;
            isOk_Value.ItemsSource = isOk_Val;

            Cause = global.cause;
            cause_Value.ItemsSource = Cause;
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
            /*global.rowTab.Zone_e = global.zonas[zona_Value.SelectedIndex].Id-1;
            global.rowTab.Zone_eString = global.zonas[zona_Value.SelectedIndex].Name;*/

        }
        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузоотправитель из справочника
        {
            Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;


            /*global.rowTab.Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            global.rowTab.Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;*/

        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузополучателя из справочника
        {
            Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;

            /*global.rowTab.Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            global.rowTab.Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;*/

        }
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор вида материала из справочника
        {
            Mat = global.mats[mat_Value.SelectedIndex].Id;
            Mat_String = global.mats[mat_Value.SelectedIndex].Name;

            /*global.rowTab.Mat = global.mats[mat_Value.SelectedIndex].Id;
            global.rowTab.Mat_String = global.mats[mat_Value.SelectedIndex].Name;*/

        }
        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор итогов аттестации
        {
            Att_code = isOk_Value.SelectedIndex;
            Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];

            /*global.rowTab.Att_code = isOk_Value.SelectedIndex;
            global.rowTab.Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];*/
        }
        private void cause_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор причины неаттестации
        {
            Cause_id = Cause[cause_Value.SelectedIndex].Id;
            Cause_idString = Cause[cause_Value.SelectedIndex].Name;

            /*global.rowTab.Cause_id = Cause[cause_Value.SelectedIndex].Id;
            global.rowTab.Cause_idString = Cause[cause_Value.SelectedIndex].Name;*/
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textboxVag.Text.Length > 0 && textboxVag.Text.Length == 8)
                {
                    global.rowTab.Num = textboxVag.Text;  // меняем номер вагона
                }

                //////////////////////////////
                string tar = textboxTara.Text;
                string tarRepl = tar.Replace(".", ",");
                if (tarRepl.Length > 0)
                {
                    global.rowTab.Tara_e = Convert.ToDouble(tarRepl);
                }
                global.rowTab.Tara_delta = Math.Round((global.rowTab.Tara - global.rowTab.Tara_e),
                    3, MidpointRounding.AwayFromZero);

                /////////////////////////////////////////
                string car = textboxCarrying.Text;
                string carRepl = car.Replace(".", ",");
                if (carRepl.Length > 0)
                {
                    global.rowTab.Carrying = Convert.ToDouble(carRepl);
                }


                bool a1 = global.client.setNum(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Num);
                bool a2 = global.client.setTara(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Tara_e);
                bool a3 = global.client.setCarry(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Carrying);
                //bool a4 = global.client.setZone(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Zone_e);
                bool a5 = global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Shipper);
                bool a6 = global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Consigner);
                bool a7 = global.client.setMat(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Mat);
                bool a8 = global.client.setAtt(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Att_code);
                bool a9 = global.client.setCause(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Cause_id);

                if (a1 && a2 && a3 && a5 && a6 && a7 && a8 && a9)
                {
                    if ((textboxVag.Text.Length > 0 && textboxVag.Text.Length == 8) || textboxVag.Text.Length == 0)
                    {
                        global.rowTab.Shipper = Shipper;
                        global.rowTab.Shipper_String = Shipper_String;

                        global.rowTab.Consigner = Consigner;
                        global.rowTab.Consigner_String = Consigner_String;

                        global.rowTab.Mat = Mat;
                        global.rowTab.Mat_String = Mat_String;

                        global.rowTab.Att_code = Att_code;
                        global.rowTab.Att_codeString = Att_codeString;

                        global.rowTab.Cause_id = Cause_id;
                        global.rowTab.Cause_idString = Cause_idString;

                        global.ROWS[global.Idx] = global.rowTab;
                        this.Close();
                    }
                    else
                    {
                        TextInput.Text = "Номер вагона должен состоять из 8 цифр";
                    }
                }
            }
            catch
            {
                TextInput.Text = "Ошибка отправки на сервер";
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
