﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;

namespace Attestation
{
    public class RowTab
    {
        /*
        public RowTab(int Id, string isOk, string VagNum, float Tara, float TaraNSI,
            float TaraDelta, byte[] LeftFoto, byte[] RightFoto, byte[] TopFoto, List<String> Reason)
        {
            this.Id = Id;
            this.isOk = isOk;
            this.VagNum = VagNum;
            this.Tara = Tara;
            this.TaraNSI = TaraNSI;
            this.TaraDelta = TaraDelta;
            this.Video = "Ok";
            this.LeftFoto = LeftFoto;
            this.RightFoto = RightFoto;
            this.TopFoto = TopFoto;
            this.Reason = Reason;
        }
        
        public int Id { get; set; }
        public string isOk { get; set; }
        public string VagNum { get; set; }
        public float Tara { get; set; }
        public float TaraNSI { get; set; }
        public float TaraDelta { get; set; }
        public string Video { get; set; }
        public byte[] LeftFoto { get; set; }
        public byte[] RightFoto { get; set; }
        public byte[] TopFoto { get; set; }
        public List<String> Reason { get; set; }
        */
        ///////////////////////////////////////////////////////////////////////

        public RowTab(int part_id, int car_id, string num, int att_code,
            double tara, int zone_e, int cause_id, string cause_name, double carrying, string att_time)
        {
            this.Part_id = part_id;
            this.Car_id = car_id;
            this.Num = num;
            this.Att_code = att_code;
            this.Tara = tara;
            this.Zone_e = zone_e;
            this.Cause_id = cause_id;
            this.Cause_name = cause_name;
            this.Carrying = carrying;
            this.Att_time = att_time;
        }

        public int Part_id { get; set; }  // Номер партии
        public int Car_id { get; set; } // Порядковый номер вагона в партии 
        public string Num { get; set; } // Номер вагона
        public int Att_code { get; set; } // Признак аттестации (аттестован, не аттестован, условно аттестован)
        public double Tara { get; set; } // Вес тары
        public int Zone_e { get; set; } // Зона вагона
        public int Cause_id { get; set; } // Код причины неаттестации
        public string Cause_name { get; set; } // Наименование причины неаттестации
        public double Carrying { get; set; } // Грузоподъемность
        public string Att_time { get; set; } // Время аттестации
    }
}

      