using System;
using System.Collections.Generic;
using System.IO;
using Thrift.Transport;
using Thrift.Protocol;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Attestation
{
    class Global
    {
        //public bool isLoadAttestation; // флаг для загрузки страницы аттестации

        public bool isColor;                                                 // флаг для кнопки начала и завершения аттестации
        public string OldPart;                                               // результат вызова метода getOldPart()
        public string mainButtonAttestation;                                 // для кнопки начала и завершения аттестации
        public System.Windows.Media.SolidColorBrush GreenColorStart;         // цвет для кнопки начала и завершения аттестации
        public System.Windows.Media.SolidColorBrush RedColorEnd;             // цвет для кнопки начала и завершения аттестации
        public bool isEnabled;                                               // флаг кликабельности datagrid


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

        public List<RowTab> ROWS;                       // внутренний список вагонов 

        /// для соединения с сервером ///////////////////////////////////////////////////////
        TTransport transport;
        public DataProviderService.Client client;
        /// /////////////////////////////////////////////////////
        public int Idx { set; get; } // для получения номера строки datagrid и combobox

        private Global()
        {
            this.transport = new TSocket("10.90.90.5", 9090);
            TProtocol proto = new TBinaryProtocol(transport);
            transport.Open();
            this.client = new DataProviderService.Client(proto);
            ///////////////////////////////////////////////////////////////////////////////

            IdShipper = null;     // Инициализация для проверки на  Null
            IdShipper = null;     // Инициализация для проверки на  Null
            IdMat = null;         // Инициализация для проверки на  Null

            photo = new photo_t();
            user = "";                 // имя пользователя
            MatName = "";              // Название материал
            PartId = "";               // Номер партии вагонов для страницы Аттестации
            Shipper = "";              // Грузоотправитель
            Consignee = "";            // Грузополучатель
            isEnabled = true;          // флаг кликабельности datagrid
            /////////////////////////////////////////////////////////////////////////////////////////////
            isColor = true;                      // для кнопки начала и завершения аттестации
            mainButtonAttestation = "Начать";    // для кнопки начала и завершения аттестации
            GreenColorStart = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1));  /*зеленый, для кнопки
                                                                                                 * начала и завершения аттестации */
            RedColorEnd = new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 33, 23)); /* красный, для кнопки
                                                                                                     * начала и завершения аттестации*/
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }
        public void GetGlobalPart(string user) // Получение партии вагонов перед Началом аттестации 
        {
            part = beginAtt(user);
            if (part.Cars != null) DATA = part.Cars;
            else DATA = new List<car_t>();
            ROWS = GetRows();
        }
        public static Global getInstance() // возвращает singleton объекта Global
        {
            if (instance == null)
                instance = new Global();
            return instance;
        }
        public List<RowTab> GetRows() // внутренний список вагонов 
        {
            List<RowTab> rows = new List<RowTab>();
            
            for(int i = 0; i<25; i++)
            {
                double Tar = 33.3;
                double Tar_e = 43.3;
                double Tar_delta = Math.Round((Tar - Tar_e), 3, MidpointRounding.AwayFromZero);
                rows.Add(new RowTab("hello", i+1, "34556644",1, "",1, "",1, "", 1, "", Tar, Tar_e, Tar_delta, 1, "", 1, "", 55, ""));
            }
            

            /*
            foreach (car_t cars in DATA)
            {
                string Part_id__ = cars.Part_id;                        
                int Car_id__ = cars.Car_id;
                string Num__ = cars.Num;
                int shipper__ = cars.Shipper;
                string shipperString__ = "";
                int consigner__ = cars.Consigner;
                string consignerString__ = "";
                int mat__ = cars.Mat;
                string matString__ = "";
                int Att_code__ = cars.Att_code;
                string Att_codeString__ = "";
                double Tara__ = cars.Tara;
                double Tara_e__ = cars.Tara_e;
                double Tara_delta__ = Math.Round((Tara__ - Tara_e__),3, MidpointRounding.AwayFromZero);
                int Zone_e__ = cars.Zone_e;
                string Zone_eString__ = "";
                switch (cars.Zone_e)
                {
                    case 0: Zone_eString__ = "Зелёная"; break;
                    case 1: Zone_eString__ = "Жёлтая"; break;
                    default: Zone_eString__ = "Красная"; break;
                }
                int Cause_id__ = cars.Cause_id;
                string Cause_idString__ = "";
                double Carrying__ = cars.Carrying_e;
                string Att_time__ = cars.Att_time;
                
                    rows.Add(new RowTab(Part_id__, Car_id__, Num__, shipper__, shipperString__, consigner__,
                        consignerString__, mat__, matString__, Att_code__, Att_codeString__, Tara__, Tara_e__,
                        Tara_delta__, Zone_e__, Zone_eString__, Cause_id__, Cause_idString__, Carrying__, Att_time__));
            }
            */
            return rows;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn) // преобразует картинку в массив байтов
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public static BitmapImage ByteArraytoBitmap(Byte[] byteArray) /* позволяет передавать картинки в виде
                                                                       * массивов байт в объект Image окна*/
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
                string[] str = new string[3];
                str = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //if (str.Length != 3)  throw new ArgumentException("ФИО задано в неверно формате");
                return string.Format("{0} {1}. {2}.", str[0], str[1][0], str[2][0]);
        }
        /// ////////////////////////////////////////// Запрос данных ///////////////////////////////////////////////////////////////////////////////
        public part_t getPart(string part_id) // Запрос партии вагонов
        {
            return this.client.getPart(part_id);
        }
        /*
        public photo_t getPhoto(string part_id, int car_id) // Получение фотографий вагона
        {
            return this.client.getPhoto(part_id, car_id);
        }
        */
        public String getUser(String Login, String Password, String Emple_id) // Получение имени пользователя
        {
            return this.client.getUser(Login, Password, Emple_id);
        }
        ///////////////////////////////////////////////// запись значений ////////////////////////////////////////////////////////////////////////////////
        public bool setNum(string part_id, int car_id, string num) // Корректировка номера вагона	
        {
            return this.client.setNum( part_id, car_id, num);
        }
        public bool setAtt(string part_id, int car_id, int att_code) // Корректировка признака аттестации
        {
            return this.client.setAtt(part_id, car_id, att_code);
        }
        public bool setUser(string part_id, string user) // запись имени оператора
        {
            return this.client.setUser(part_id, user); 
        }
        //////////////////////////////////////////////////// Функции  //////////////////////////////////////////////////////////////////////////
        public bool exitAtt(string part_id) // Завершение аттестации
        {
            return this.client.endAtt(part_id);
        }
        public part_t beginAtt( string user) // Начало аттестации и получение партии вагонов
        {
            return this.client.startAtt(user);
        }
        public bool changePass(string login, string oldPass, string newPass, string newEmpl_id) // Смена данных учетной записи 
        {
            return this.client.changePass(login, oldPass, newPass, newEmpl_id);
        }

    }
}
