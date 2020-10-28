﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Attestation
{
    public partial class ShowChange_Consigner_String : Window
    {
        private Global global;
        private List<Consigners> consignersVal;

        public ShowChange_Consigner_String()
        {
            InitializeComponent();
            global = Global.getInstance();
            consignersVal = global.consigners;
            consigner_Value.ItemsSource = consignersVal;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (global.client.setShipper(global.part.Part_id, global.ROWS[global.Idx].Car_id, global.ROWS[global.Idx].Shipper))
            {
                this.Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void zona_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            global.ROWS[global.Idx].Consigner = global.consigners[consigner_Value.SelectedIndex].Id - 1;
            global.ROWS[global.Idx].Consigner_String = global.consigners[consigner_Value.SelectedIndex].Name;

        }
    }
}
