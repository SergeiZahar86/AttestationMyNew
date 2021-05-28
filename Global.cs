using System;
using System.Collections.Generic;
using System.IO;
using Thrift.Transport;
using Thrift.Protocol;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Configuration;
using System.Text;
using System.Collections.ObjectModel;
using Attestation.DialogWindows;

namespace Attestation
{
    class Global
    {

        public bool ArmAttestation;                                          // разграничение работы армов
        public bool getStartAtt;                                             // можно ли начинать аттестацию
        public bool isColor;                                                 // флаг для кнопки начала и завершения аттестации
        public string OldPart;                                               // результат вызова метода getOldPart()
        public string mainButtonAttestation;                                 // для кнопки начала и завершения аттестации
        public SolidColorBrush GreenColor;                              // цвет для кнопки начала и завершения аттестации
        public SolidColorBrush RedColor;                                  // цвет для кнопки начала и завершения аттестации
        public SolidColorBrush GreyColor;                              // цвет для кнопки начала и завершения аттестации
        public SolidColorBrush YellowColor;                              // цвет для кнопки начала и завершения аттестации
        public bool isEnabled;                                               // флаг кликабельности datagrid
        public bool is_ok_close_att;                                         // проверка при закрытии аттестации
        public bool normal_att;                                              // нормальный режим аттестации
        public int how_many_wagons;                                          // количество вагонов при аварийном режиме аттестации


        private static Global instance;

        public string startTimeStr;                     // Начало аттестации партии вагонов (String) для страницы Аттестации
        public string endTimeStr;                       //  Окончание аттестации (String) для страницы Аттестации
        public string deltaTimeStr;                     // Продолжительность прохождения аттестации (String) для страницы Аттестации 
        public DateTime startTime;                      // Начало аттестации партии вагонов
        public DateTime endTime;                        // Окончание аттестации 
        public TimeSpan deltaTime;                      // Продолжительность прохождения аттестации

        public List<car_t> DATA;                        // Данные по вагонам
        public photo_t photo;                           // класс содержащий поля фотографий вагонов
        public part_t part;                             // партии вагонов
        public string user;                             // имя пользователя (ФИО)
        public string Login;
        public string numberCard;            // Номер карты пользователя
        public int? IdShipper;               // id Грузоотправителя  для диалогового окна input_Of_Initial_Data при начале аттестации
        public int? IdConsigner;             // id Грузополучателя для диалогового окна input_Of_Initial_Data при начале аттестации
        public int? IdMat;                   // id материала для диалогового окна input_Of_Initial_Data при начале аттестации
        public string MatName;               // Название материала для страницы Аттестации
        public string PartId;                // Номер партии вагонов для страницы Аттестации
        public string Shipper;               // Грузоотправитель для страницы Аттестации
        public string Consignee;             // Грузополучатель для страницы Аттестации


        public List<cause_t> cause;                     // справочник причин неаттестации
        public List<contractor_t> contractors;          // справочник контрагентов
        public List<Shippers> shippers;                 // справочник Грузоотправителя
        public List<Consigners> consigners;             // справочник Грузополучателя
        public List<mat_t> mats;                        // справочник материалов
        public List<string> IsOk_Val;                   // справочник итогов аттестации
        public List<string> Att_codeFonts;              // справочник элементов шрифта для итогов аттестации
        public List<Zona> zonas;                        // справочник Зоны вагонов

        public TextBox pubTextBox;
        public ObservableCollection<RowTab> ROWS;                       // внутренний список вагонов 
        // сохранение объекта вагона для изменения значения по клику по таблице
        public RowTab rowTab;
        public string PLC_topic;
        public string Mqtt_host;
        public string Mqtt_user;
        public string Mqtt_password;


        public TTransport transport;
        public DataProviderService.Client client; // DataProviderService - Название заглушки
        public int Port;
        public string Host;
        Waiting_Download_data waiting_;
        public int Idx { set; get; } // для получения номера строки datagrid и combobox

        private Global()
        {

            //////////////////////////////////////////////////////////////////////////
            var appSettings = ConfigurationManager.AppSettings;
            PLC_topic = appSettings["PLC_topic"];
            Mqtt_host = appSettings["Mqtt_host"];
            Mqtt_user = appSettings["Mqtt_user"];
            Mqtt_password = appSettings["Mqtt_password"];
            /////////////// подключение к серверу ///////////////////////////////////////////////////////////////////////////////////
            Port = int.Parse(appSettings["port"] ?? "9090");
            Host = appSettings["host"] ?? "localhost";
            this.transport = new TSocket(Host, Port); //  IP адрес сервера
            TProtocol proto = new TBinaryProtocol(transport);
            this.client = new DataProviderService.Client(proto);
            try
            {
                transport.Open();
            }
            catch(Exception sa)
            {
                ExClose exClose = new ExClose(sa.ToString());
                exClose.ShowDialog();
            }
            ///////////////////////////////////////////////////////////////////////////////

            IdShipper = null;     // Инициализация для проверки на  Null
            IdShipper = null;     // Инициализация для проверки на  Null
            IdMat = null;         // Инициализация для проверки на  Null

            photo = new photo_t();
            user = "qqqq";                 // имя пользователя
            MatName = "";              // Название материал
            PartId = "";               // Номер партии вагонов для страницы Аттестации
            Shipper = "";              // Грузоотправитель
            Consignee = "";            // Грузополучатель
            isEnabled = true;          // флаг кликабельности datagrid
            /////////////////////////////////////////////////////////////////////////////////////////////
            isColor = true;                      // для кнопки начала и завершения аттестации
            mainButtonAttestation = "Начать";    // для кнопки начала и завершения аттестации
            GreenColor = new SolidColorBrush(Color.FromRgb(4, 158, 129)); 
            RedColor = new SolidColorBrush(Color.FromRgb(222, 102, 96));
            GreyColor = new SolidColorBrush(Color.FromRgb(98, 107, 111));
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///
            normal_att = true;          // нормальный режим аттестации
        }


        public void GetGlobalPart(string user) // Получение партии вагонов перед Началом аттестации 
        {
            //part = beginAtt(user);
            DATA = part.Cars;        // серверный список вагонов
            ROWS = GetRows();        // внутренний список вагонов
        }
        public static Global getInstance() // возвращает singleton объекта Global
        {
            if (instance == null)
                instance = new Global();
            return instance;
        }
        public ObservableCollection<RowTab> GetRows() // внутренний список вагонов 
        {
            ObservableCollection<RowTab> rows = new ObservableCollection<RowTab>();
            
            foreach (car_t cars in DATA)
            {
                string Part_id__ = cars.Part_id;                        
                int Car_id__ = cars.Car_id;
                string Num__ = cars.Num;
                int shipper__ = cars.Shipper;
                string shipperString__ = "";
                if (shipper__ != 0)
                {
                    foreach (Shippers shipp in shippers)
                    {
                        if (shipp.Id == shipper__)
                        {
                            shipperString__ = shipp.Name;
                        }
                    }
                }
                int consigner__ = cars.Consigner;
                string consignerString__ = "";
                if (consigner__ != 0)
                {
                    foreach (Consigners cons in consigners)
                    {
                        if (cons.Id == consigner__)
                        {
                            consignerString__ = cons.Name;
                        }
                    }
                }
                int mat__ = cars.Mat;
                string matString__ = "";
                if (mat__ != 0)
                {
                    foreach (mat_t mat in mats)
                    {
                        if (mat.Id == mat__)
                        {
                            matString__ = mat.Name;
                        }
                    }
                }
                int Att_code__ = cars.Att_code;
                string Att_codeString__ = "";
                
                    switch (Att_code__)
                        {
                        case 0:
                            Att_codeString__ = Att_codeFonts[0];
                            break;
                        case 1:
                            Att_codeString__ = Att_codeFonts[1];
                            break;
                        /*case 2:
                            Att_codeString__ = Att_codeFonts[2];
                            break;*/
                    }
                
                double Tara__ = Math.Round( cars.Tara, 3);
                double Tara_e__ = cars.Tara_e;
                double Tara_delta__ = Math.Round(Tara__ - Tara_e__,3);
                int Zone_e__ = cars.Zone_e;
                string Zone_eString__ = "";
                switch (Zone_e__)
                {
                    case 0: Zone_eString__ = zonas[0].Name; break;
                    case 1: Zone_eString__ = zonas[1].Name; break;
                    case 2: Zone_eString__ = zonas[2].Name; break;
                }
                int Cause_id__ = cars.Cause_id;
                string Cause_idString__ = "";
                if (Cause_id__ != 0)
                {
                    foreach (cause_t cc in cause)
                    {
                        if (cc.Id == Cause_id__)
                        {
                            Cause_idString__ = cc.Name;
                        }
                    }
                }
                double Carrying__ = cars.Carrying_e;
                string Att_time__ = cars.Att_time;

                rows.Add(new RowTab(Part_id__, Car_id__, Num__, shipper__, shipperString__, consigner__,
                        consignerString__, mat__, matString__, Att_code__, Att_codeString__, Tara__, Tara_e__,
                        Tara_delta__, Zone_e__, Zone_eString__, Cause_id__, Cause_idString__, Carrying__, Att_time__));
            }
            
            return rows;
        }
        public static BitmapImage ByteArraytoBitmap(Byte[] byteArray) //позволяет передавать картинки в виде массивов байт в объект Image окна
        {
            MemoryStream stream = new MemoryStream(byteArray);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }
        public static string ShortName(string fio) // из полного "Фамилии Имени, Отчества" получить "Фамилию, инициалы"
        {
            try
            {
                string[] str = new string[3];
                str = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length == 3)
                {
                    //if (str.Length != 3)  throw new ArgumentException("ФИО задано в неверно формате");
                    return string.Format("{0} {1}. {2}.", str[0], str[1][0], str[2][0]);
                }
                return fio;
            }
            catch
            {
                return null;
            }
        }
        public photo_t getPhoto(string part_id, int car_id) // Получение фотографий вагона
        {
            return this.client.getPhoto(part_id, car_id);
        }
        public bool setUser(string part_id, string user) // запись имени оператора
        {
            return this.client.setUser(part_id, user); 
        }
        public bool exitAtt(string part_id) // Завершение аттестации
        {
            return this.client.endAtt(part_id);
        }
        public bool checkSum(string number) // проверка номера вагона на правдивость
        {
            byte[] buf = Encoding.ASCII.GetBytes(number);
            if (buf.Length == 8 && number != "00000000")
            {
                for (int i = 0; i < number.Length; i++)
                {
                    buf[i] = (byte)(buf[i] - 48);
                }
                byte[] K = { 2, 1, 2, 1, 2, 1, 2 };
                int sum = 0;
                for (int i = 0; i < 7; i++)
                {
                    int p = buf[i] * K[i];
                    p = p > 9 ? p = p % 10 + p / 10 : p;
                    sum += p;
                }
                int c = sum % 10 == 0 ? 0 : 10 - sum % 10;
                if (c == buf[7])
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public void workAfterShutdown() // возобновление работы после закрытия программы
        {
            try
            {
                waiting_ = new Waiting_Download_data();
                waiting_.Show();
                cause = client.getCauses();               // Запрос справочника причин неаттестации
                contractors = client.getContractors();    // Запрос справочника контрагентов
                mats = client.getMat();                   // Запрос справочника материалов
                //mats.Sort();
                GetConsignees(contractors);                      // получение справочника Грузополучателя
                GetShippers(contractors);                        // получение справочника Грузоотправителей
                GetZonas();                                             // получение справочника Зоны вагонов
                IsOk_Val = GetIsOk_Val();                        // справочник итогов аттестации
                Att_codeFonts = GetAtt_codeFonts();              // справочник элементов шрифта для итогов аттестации

                //part = client.getPart();                 // получаем незавершенную партию
                //startTimeStr = part.Start_time_att;                        // получение времени начала аттестации
                //PartId = part.Part_id;                                 // получение номера партии
                //isColor = false;                                              // для кнопки начала и завершения аттестации 
                waiting_.Close();
            }
            catch (Exception ss)
            {
                waiting_.Close();
                /*ExClose exClose = new ExClose(ss.ToString());
                exClose.ShowDialog();
                waiting_.Close();*/
            }
        }
        public void GetShippers(List<contractor_t> contr) // получение справочника Грузоотправителей
        {
            shippers = new List<Shippers>();
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Shipper)
                {
                    shippers.Add(new Shippers(contractor_.Id, contractor_.Name));
                }
            }
            //shippers.Sort();
        }
        public void GetConsignees(List<contractor_t> contr) // получение справочника Грузополучателя
        {
            consigners = new List<Consigners>();
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Consigner)
                {
                    consigners.Add(new Consigners(contractor_.Id, contractor_.Name));
                }
            }
            //consigners.Sort();
        }
        public void GetZonas() // получение справочника Зоны вагонов
        {
            zonas = new List<Zona>();
            zonas.Add(new Zona(1, "Зелёная"));
            zonas.Add(new Zona(2, "Жёлтая"));
            zonas.Add(new Zona(3, "Красная"));
        }
        public List<string> GetIsOk_Val() // справочник итогов аттестации
        {
            List<string> str = new List<string>
            {
                "Не аттестован",
                "Аттестован",
                //"Условно аттестован"
            };
            return str;
        }
        public List<string> GetAtt_codeFonts() // справочник элементов шрифта для итогов аттестации
        {
            List<string> fonts = new List<string>();
            fonts.Add("WindowClose");
            fonts.Add("CheckCircle");
            //fonts.Add("Asterisk");
            return fonts;
        }

        // новые функции ==========================================================================================================
        public bool EndAtt(string user_login) // вызов функции endAtt
        {

            return client.endAtt(user_login);
        }
        public string CreateTask(string user_login) // вызов функции createTask
        {
            return client.createTask(user_login);
        }
        public void EndTask(string user_login) // вызов функции endTask
        {
            //throw new Exception("Не получилось завершить задание");
            client.endTask(user_login);
        }
        public void RemoveTask(string user_login) // вызов функции removeTask
        {
            //throw new Exception("Не получилось удалить задание");
            client.removeTask(user_login);
        }
        public state_bits GetStatusBits() // вызов функции getStatusBits
        {
            //throw new Exception("Сломались лампочки");
            //return new state_bits() { Task = 2, Inspection = 0, Weight = 0, Load = 0 };
            return client.getStatusBits();
        }
        public part_t GetPart() // вызов функции getPart
        {
            //throw new Exception("Не получилось удалить задание");
            /*part_t part_ = new part_t();
            car_t car_ = new car_t();
            List<car_t> car_s = new List<car_t>();
            car_s.Add(car_);
            part_.Cars = car_s;
            part_.Start_time_att = "e";
            part_.End_time_att = "e";
            return part_;*/
            return client.getPart();
        }

    }
}
