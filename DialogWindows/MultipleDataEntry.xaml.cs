using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Attestation.DialogWindows
{
    public partial class MultipleDataEntry : Window
    {
        private Global global;
        private List<Zona> zona_Val;
        private List<Shippers> shippersVal;
        private List<Consigners> consignersVal;
        private List<mat_t> matsVal;
        private List<String> isOk_Val;
        private List<cause_t> Cause;
        ObservableCollection<RowTab> rows_num;

        private int shipper;
        private string shipperStr;
        private bool isChengeShipper;

        private int consigner;
        private string consignerStr;
        private bool isChengeConsigner;

        private int mat;
        private string matStr;
        private bool isChengeMat;

        private int att;
        private string attStr;
        private bool isChengeAtt;


        public MultipleDataEntry(string number, ObservableCollection<RowTab> rows)
        {
            global = Global.getInstance();
            InitializeComponent();
            rows_num = new ObservableCollection<RowTab>();

            shippersVal = global.shippers;
            shipper_Value.ItemsSource = shippersVal;

            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;

            matsVal = global.mats;
            mat_Value.ItemsSource = matsVal;

            isOk_Val = global.IsOk_Val;
            isOk_Value.ItemsSource = isOk_Val;

            Number.Text = number;
            rows_num = rows;

            if (global.ArmAttestation)
            {
                shipper_Value.IsEnabled = false;
                consigner_Value.IsEnabled = false;
                mat_Value.IsEnabled = false;
                isOk_Value.IsEnabled = true;
            }
            else
            {
                shipper_Value.IsEnabled = true;
                consigner_Value.IsEnabled = true;
                mat_Value.IsEnabled = true;
                isOk_Value.IsEnabled = false;
            }
        }

        private void shipper_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузоотправитель из справочника
        {
            //global.rowTab.Shipper = global.shippers[shipper_Value.SelectedIndex].Id;
            //global.rowTab.Shipper_String = global.shippers[shipper_Value.SelectedIndex].Name;
                shipper = global.shippers[shipper_Value.SelectedIndex].Id;
                shipperStr = global.shippers[shipper_Value.SelectedIndex].Name;
                isChengeShipper = true;
            /*int p = global.shippers[shipper_Value.SelectedIndex].Id;
            string ship = global.shippers[shipper_Value.SelectedIndex].Name;
            foreach (RowTab row in rows_num)
            {
                row.Shipper = p;
                row.Shipper_String = ship;
            }*/

        }
        private void consigner_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор Грузополучателя из справочника
        {
            //global.rowTab.Consigner = global.consigners[consigner_Value.SelectedIndex].Id;
            //global.rowTab.Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;
                consigner = global.consigners[consigner_Value.SelectedIndex].Id;
                consignerStr = global.consigners[consigner_Value.SelectedIndex].Name;
                isChengeConsigner = true;


            /*int p = global.consigners[consigner_Value.SelectedIndex].Id;
            string cons = global.consigners[consigner_Value.SelectedIndex].Name;
            foreach (RowTab row in rows_num)
            {
                row.Consigner = p;
                row.Consigner_String = cons;
            }*/
        }
        private void mat_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор вида материала из справочника
        {
            //global.rowTab.Mat = global.mats[mat_Value.SelectedIndex].Id;
            //global.rowTab.Mat_String = global.mats[mat_Value.SelectedIndex].Name;
                mat = global.mats[mat_Value.SelectedIndex].Id;
                matStr = global.mats[mat_Value.SelectedIndex].Name;
                isChengeMat = true;


            /*int p = global.mats[mat_Value.SelectedIndex].Id;
            string mat = global.mats[mat_Value.SelectedIndex].Name;
            foreach (RowTab row in rows_num)
            {
                row.Mat = p;
                row.Mat_String = mat;
            }*/

        }
        private void isOk_Value_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор итогов аттестации
        {
            //global.rowTab.Att_code = isOk_Value.SelectedIndex;
            //global.rowTab.Att_codeString = global.Att_codeFonts[isOk_Value.SelectedIndex];

            att = isOk_Value.SelectedIndex;
            attStr = global.Att_codeFonts[isOk_Value.SelectedIndex];
            isChengeAtt = true;


            /*int p = isOk_Value.SelectedIndex;
            string att = global.Att_codeFonts[isOk_Value.SelectedIndex];
            foreach (RowTab row in rows_num)
            {
                row.Att_code = p;
                row.Att_codeString = att;
            }*/
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < rows_num.Count; i++)
                {
                    //bool a5 = global.client.setShipper(global.part.Part_id, rows_num[i].Car_id, rows_num[i].Shipper);
                    //bool a6 = global.client.setConsigner(global.part.Part_id, rows_num[i].Car_id, rows_num[i].Consigner);
                    //bool a7 = global.client.setMat(global.part.Part_id, rows_num[i].Car_id, rows_num[i].Mat);
                    //bool a8 = global.client.setAtt(global.part.Part_id, rows_num[i].Car_id, rows_num[i].Att_code);

                    //global.ROWS[rows_num[i].Car_id - 1] = rows_num[i];

                    if (isChengeShipper)
                    {
                        bool a5 = global.client.setShipper(global.part.Part_id, rows_num[i].Car_id, shipper);
                        if (a5)
                            global.ROWS[rows_num[i].Car_id - 1].Shipper = shipper;
                    }
                    if (isChengeConsigner)
                    {
                        bool a6 = global.client.setConsigner(global.part.Part_id, rows_num[i].Car_id, consigner);
                        if (a6)
                            global.ROWS[rows_num[i].Car_id - 1].Consigner = consigner;
                    }
                    if (isChengeMat)
                    {
                        bool a7 = global.client.setMat(global.part.Part_id, rows_num[i].Car_id, mat);
                        if (a7)
                            global.ROWS[rows_num[i].Car_id - 1].Mat = mat;
                    }
                    if (isChengeAtt)
                    {
                        bool a8 = global.client.setAtt(global.part.Part_id, rows_num[i].Car_id, att);
                        if (a8)
                            global.ROWS[rows_num[i].Car_id - 1].Att_code = att;
                    }

                }

                /*bool a5 = global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Shipper);
                bool a6 = global.client.setConsigner(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Shipper);
                bool a7 = global.client.setMat(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Mat);
                bool a8 = global.client.setAtt(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.rowTab.Att_code);*/

                /*if (a5 && a6 && a7 && a8)
                {
                    global.ROWS[global.Idx] = global.rowTab;
                    this.Close();
                }*/

                this.Close();
            }
            catch (Exception sss)
            {
                MessageBox.Show(sss.ToString());
            }
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
