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
        private bool isShipper;

        private int Consigner;
        private string Consigner_String;
        private bool isConsigner;

        private int Mat;
        private string Mat_String;
        private bool isMat;

        private int Att_code;
        private string Att_codeString;
        private bool isAtt_code;

        private int Cause_id;
        private string Cause_idString;
        private bool isCause_id;

        private string Num;
        private double Tara_e;
        private double Carrying;
        private double Tara_delta;

        bool a1;
        bool a2;
        bool a3;
        bool a4;
        bool a5;
        bool a6;
        bool a7;
        bool a8;


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
                FocusManager.SetFocusedElement(this, textboxVag);     // set logical focus
            }
            else
            {
                textboxVag.IsEnabled = false;
                isOk_Value.IsEnabled = false;
                cause_Value.IsEnabled = false;
                FocusManager.SetFocusedElement(this, textboxTara);     // set logical focus
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
        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузоотправитель из справочника
        {
            Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;
            isShipper = true;

            /*global.rowTab.Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            global.rowTab.Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;*/

        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузополучателя из справочника
        {
            Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;
            isConsigner = true;

            /*global.rowTab.Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            global.rowTab.Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;*/

        }
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор вида материала из справочника
        {
            Mat = global.mats[mat_Value.SelectedIndex].Id;
            Mat_String = global.mats[mat_Value.SelectedIndex].Name;
            isMat = true;

            /*global.rowTab.Mat = global.mats[mat_Value.SelectedIndex].Id;
            global.rowTab.Mat_String = global.mats[mat_Value.SelectedIndex].Name;*/

        }
        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор итогов аттестации
        {
            Att_code = isOk_Value.SelectedIndex;
            Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];
            isAtt_code = true;

            /*global.rowTab.Att_code = isOk_Value.SelectedIndex;
            global.rowTab.Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];*/
        }
        private void cause_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор причины неаттестации
        {
            Cause_id = Cause[cause_Value.SelectedIndex].Id;
            Cause_idString = Cause[cause_Value.SelectedIndex].Name;
            isCause_id = true;

            /*global.rowTab.Cause_id = Cause[cause_Value.SelectedIndex].Id;
            global.rowTab.Cause_idString = Cause[cause_Value.SelectedIndex].Name;*/
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Num = textboxVag.Text;  // меняем номер вагона

                //////////////////////////////
                string tar = textboxTara.Text;
                string tarRepl = tar.Replace(".", ",");
                if (tarRepl.Length > 0)
                {
                    Tara_e = Convert.ToDouble(tarRepl);
                }
                Tara_delta = Math.Round((global.rowTab.Tara - global.rowTab.Tara_e),
                    3, MidpointRounding.AwayFromZero);

                /////////////////////////////////////////
                string car = textboxCarrying.Text;
                string carRepl = car.Replace(".", ",");
                if (carRepl.Length > 0)
                {
                    Carrying = Convert.ToDouble(carRepl);
                }

                if (global.ArmAttestation)
                {
                    if (textboxVag.Text.Length > 0 && textboxVag.Text.Length == 8)
                        global.client.setNum(global.part.Part_id, global.ROWS[global.Idx].Car_id, Num);
                    if (textboxVag.Text.Length > 0 && textboxVag.Text.Length != 8)
                    {
                        TextInput.Text = "Номер вагона должен состоять из 8 цифр";
                        return;
                    }
                    if (isAtt_code)
                        global.client.setAtt(global.part.Part_id, global.ROWS[global.Idx].Car_id, Att_code);
                    if (isCause_id)
                        global.client.setCause(global.part.Part_id, global.ROWS[global.Idx].Car_id, Cause_id);
                }
                else
                {
                    if (Tara_e > 0)
                        global.client.setTara(global.part.Part_id, global.ROWS[global.Idx].Car_id, Tara_e);
                    if (Carrying > 0)
                        global.client.setCarry(global.part.Part_id, global.ROWS[global.Idx].Car_id, Carrying);
                    if(isShipper)
                        global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, Shipper);
                    if(isConsigner)
                        global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, Consigner);
                    if(isMat)
                        global.client.setMat(global.part.Part_id, global.ROWS[global.Idx].Car_id, Mat);
                }
                Close();
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
