using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Attestation
{
    public class RowTab : INotifyPropertyChanged
    {
        public RowTab(string part_id, int car_id, string num, int shipper, string shipperString, int consigner,
            string consignerString, int mat, string matString, int att_code, string att_codeString,
            double tara, double tara_e, double tara_delta, int zone_e, string zone_eString, int cause_id,
            string cause_idString, double carrying, string att_time)
        {
            this.Part_id = part_id;
            this.Car_id = car_id;
            this.Num = num;
            this.Shipper = shipper;
            this.Shipper_String = shipperString;
            this.Consigner = consigner;
            this.Consigner_String = consignerString;
            this.Mat = mat;
            this.Mat_String = matString;
            this.Att_code = att_code;
            this.Att_codeString = att_codeString;
            this.Tara = tara;
            this.Tara_e = tara_e;
            this.Tara_delta = tara_delta;
            this.Zone_e = zone_e;
            this.Zone_eString = zone_eString;
            this.Cause_id = cause_id;
            this.Cause_idString = cause_idString;
            this.Carrying = carrying;
            this.Att_time = att_time;
        }


        private string part_id;
        public string Part_id                  // Номер партии
        {
            get { return part_id; }
            set
            {
                part_id = value;
                OnPropertyChanged("Part_id");
            }
        }  
        private int car_id;
        public int Car_id                      // Порядковый номер вагона в партии 
        {
            get { return car_id; }
            set
            {
                car_id = value;
                OnPropertyChanged("Car_id");
            }
        }
        private string num;
        public string Num                          // Номер вагона
        {
            get { return num; }
            set
            {
                num = value;
                OnPropertyChanged("Num");
            }
        }
        private int shipper;
        public int Shipper                           // Грузоотправитель
        {
            get { return shipper; }
            set
            {
                shipper = value;
                OnPropertyChanged("Shipper");
            }
        }
        private string shipper_String;
        public string Shipper_String                   // (изменено) Грузоотправитель
        {
            get { return shipper_String; }
            set
            {
                shipper_String = value;
                OnPropertyChanged("Shipper_String");
            }
        }
        private int consigner;
        public int Consigner                               // Грузополучатель
        {
            get { return consigner; }
            set
            {
                consigner = value;
                OnPropertyChanged("Consigner");
            }
        }
        private string consigner_String;
        public string Consigner_String                  // (изменено) Грузополучатель
        {
            get { return consigner_String; }
            set
            {
                consigner_String = value;
                OnPropertyChanged("Consigner_String");
            }
        }
        private int mat;
        public int Mat                  // Код материала 
        {
            get { return mat; }
            set
            {
                mat = value;
                OnPropertyChanged("Mat");
            }
        }
        private string mat_String;
        public string Mat_String                  // (изменено) Код материала 
        {
            get { return mat_String; }
            set
            {
                mat_String = value;
                OnPropertyChanged("Mat_String");
            }
        }
        private int att_code;
        public int Att_code                  // Признак аттестации (аттестован, не аттестован, условно аттестован)
        {
            get { return att_code; }
            set
            {
                att_code = value;
                OnPropertyChanged("Att_code");
            }
        }
        private string att_codeString;
        public string Att_codeString                 // (изменено) Признак аттестации (аттестован, не аттестован, условно аттестован)
        {
            get { return att_codeString; }
            set
            {
                att_codeString = value;
                OnPropertyChanged("Att_codeString");
            }
        }
        private double tara;
        public double Tara                 // Вес тары
        {
            get { return tara; }
            set
            {
                tara = value;
                OnPropertyChanged("Tara");
            }
        }
        private double tara_e;
        public double Tara_e                 // Вес тары по Этрану
        {
            get { return tara_e; }
            set
            {
                tara_e = value;
                OnPropertyChanged("Tara_e");
            }
        }
        private double tara_delta;
        public double Tara_delta                 // Дельта массы
        {
            get { return tara_delta; }
            set
            {
                tara_delta = value;
                OnPropertyChanged("Tara_delta");
            }
        }
        private int zone_e;
        public int Zone_e                 // Зона вагона
        {
            get { return zone_e; }
            set
            {
                zone_e = value;
                OnPropertyChanged("Zone_e");
            }
        }
        private string zone_eString;
        public string Zone_eString                 // Зона вагона
        {
            get { return zone_eString; }
            set
            {
                zone_eString = value;
                OnPropertyChanged("Zone_eString");
            }
        }
        private int cause_id;
        public int Cause_id                 // Код причины неаттестации
        {
            get { return cause_id; }
            set
            {
                cause_id = value;
                OnPropertyChanged("Cause_id");
            }
        }
        private string cause_idString;
        public string Cause_idString                 // (изменено)  причины неаттестации
        {
            get { return cause_idString; }
            set
            {
                cause_idString = value;
                OnPropertyChanged("Cause_idString");
            }
        }
        private double carrying;
        public double Carrying                 // Грузоподъемность
        {
            get { return carrying; }
            set
            {
                carrying = value;
                OnPropertyChanged("Carrying");
            }
        }
        private string att_time;
        public string Att_time                 // Время аттестации
        {
            get { return att_time; }
            set
            {
                att_time = value;
                OnPropertyChanged("Att_time");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

      