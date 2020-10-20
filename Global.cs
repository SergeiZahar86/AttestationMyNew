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

        public bool isColor; // флаг для кнопки начала и завершения аттестации
        public string mainButtonAttestation; // для кнопки начала и завершения аттестации
        public System.Windows.Media.SolidColorBrush currentColor; // цвет для кнопки начала и завершения аттестации
        
        private static Global instance;
        
        public List<car_t> DATA; // Данные по вагонам
        public photo_t photo; // класс содержащий поля фотографий вагонов
        public part_t part; // партии вагонов
        public string user; // имя пользователя
        public int? IdShipper; // id Грузоотправителя
        public int? IdConsignee; // id Грузополучателя
        public int? IdMat; // id материала

        public List<cause_t> cause; // справочник причин неаттестации
        public List<contractor_t> contractors; // справочник контрагентов
        public List<Shippers> shippers; // справочник Грузоотправителя
        public List<Consignees> consignees; // справочник Грузополучателя
        public List<mat_t> mats; // справочник материалов
        public List<string> IsOk_Val; // справочник итогов аттестации
        public List<string> Att_codeFonts; // справочник элементов шрифта для итогов аттестации

        public List<RowTab> ROWS; // внутренний список вагонов 

        /// для соединения с сервером ///////////////////////////////////////////////////////
        TTransport transport;
        DataProviderService.Client client;
        /// /////////////////////////////////////////////////////
        /// 
        public int Idx { set; get; }

        private Global()
        {
            this.transport = new TSocket("10.90.90.5", 9090);
            TProtocol proto = new TBinaryProtocol(transport);
            transport.Open();
            this.client = new DataProviderService.Client(proto);
            ///////////////////////////////////////////////////////////////////////////////

            //isLoadAttestation = true;

            cause = getCauses(); // Запрос справочника причин неаттестации
            contractors = getContractors(); // Запрос справочника контрагентов
            mats = getMat(); // Запрос справочника материалов
            IsOk_Val = GetIsOk_Val(); // справочник итогов аттестации
            Att_codeFonts = GetAtt_codeFonts(); // справочник элементов шрифта для итогов аттестации
            GetShippers(contractors); // получение справочника Грузоотправителей
            GetConsignees(contractors); // получение справочника Грузополучателя

            IdShipper = null;
            IdShipper = null;
            IdMat = null;

            photo = new photo_t();
            user = ""; // имя пользователя
            /////////////////////////////////////////////////////////////////////////////////////////////
            isColor = true;                   // для кнопки начала и завершения аттестации
            mainButtonAttestation = "Начать"; // для кнопки начала и завершения аттестации
                                              //зеленый, для кнопки начала и завершения аттестации
            currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1)); 
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public void GetShippers(List<contractor_t> contr) // получение справочника Грузоотправителей
        {
            shippers = new List<Shippers>();
            foreach(contractor_t contractor_ in contr)
            {
                if (contractor_.Shipper)
                {
                    shippers.Add(new Shippers(contractor_.Id, contractor_.Name));
                }
            }
        }

        public void GetConsignees(List<contractor_t> contr) // получение справочника Грузополучателя
        {
            consignees = new List<Consignees>();
            foreach (contractor_t contractor_ in contr)
            {
                if (contractor_.Consignee)
                {
                    consignees.Add(new Consignees(contractor_.Id, contractor_.Name));
                }
            }
        }

        public void GetGlobalPart(int shipper, int consignee, int mat, string user) /* Получение партии
                                                                                     * вагонов перед Началом аттестации */
        {
            part = beginAtt(shipper, consignee, mat, user);
            DATA = part.Cars;
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
            foreach (car_t cars in DATA)
            {
                int Part_id__ = cars.Part_id;
                int Car_id__ = cars.Car_id;
                string Num__ = cars.Num;
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
                double Carrying__ = cars.Carrying;
                string Att_time__ = cars.Att_time;
                rows.Add(new RowTab(Part_id__, Car_id__, Num__, Att_code__,
                    Att_codeString__, Tara__, Tara_e__, Tara_delta__, Zone_e__, Zone_eString__,
                    Cause_id__, Cause_idString__, Carrying__, Att_time__));
            }
            return rows;
        }

        public List<string> GetIsOk_Val() // справочник итогов аттестации
        {
            List<String> str = new List<string>(); 
            str.Add("Аттестован");
            str.Add("Не аттестован");
            str.Add("Условно аттестован");
            return str;
        }

        public List<string> GetAtt_codeFonts()
        {
            List<string> fonts = new List<string>(); // справочник элементов шрифта для итогов аттестации
            fonts.Add("CheckCircle");
            fonts.Add("WindowClose");
            fonts.Add("Asterisk");
            return fonts;
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn) // преобразует картинку в массив байтов
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static BitmapImage ByteArraytoBitmap(Byte[] byteArray) /* позволяет
        передавать картинки в виде массивов байт в объект Image окна*/
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

        ///////////////////////////////////////////// Справочники ////////////////////////////////////////////////////////////////////////////////
        public List<cause_t> getCauses() // Запрос справочника причин неаттестации
        {
            return this.client.getCauses();
        }
        public List<contractor_t> getContractors() // Запрос справочника контрагентов
        {
            return this.client.getContractors();
        }
        public List<mat_t> getMat() // Запрос справочника материалов
        {
            return this.client.getMat();
        }
        /// ////////////////////////////////////////// Запрос данных ///////////////////////////////////////////////////////////////////////////////
        public part_t getPart(int id) // Запрос партии вагонов
        {
            return this.client.getPart(id);
        }
        public photo_t getPhoto(int part_id, int car_id) // Получение фотографий вагона
        {
            return this.client.getPhoto(part_id, car_id);
        }
        public String getUser(String Login, String Password, String Emple_id) // Получение имени пользователя
        {
            return this.client.getUser(Login, Password, Emple_id);
        }
        ///////////////////////////////////////////////// запись значений ////////////////////////////////////////////////////////////////////////////////
        public bool setNum(int part_id, int car_id, string num) // Корректировка номера вагона	
        {
            return this.client.setNum( part_id, car_id, num);
        }
        public bool setAtt(int att_code) // Корректировка признака аттестации
        {
            return this.client.setAtt(att_code);
        }
        public bool setUser(string user) // запись имени оператора
        {
            return this.setUser(user); 
        }
        //////////////////////////////////////////////////// Функции  //////////////////////////////////////////////////////////////////////////
        public bool exitAtt() // Завершение аттестации
        {
            return this.client.exitAtt();
        }
        public part_t beginAtt(int shipper, int consignee, int mat, string user) // Начало аттестации 
        {
            return this.client.beginAtt(shipper, consignee, mat, user);
        }
        public bool changePass(string oldPass, string newPass, string newEmpl_id) // Смена данных учетной записи 
        {
            return this.client.changePass(oldPass, newPass, newEmpl_id);
        }
    }
}
