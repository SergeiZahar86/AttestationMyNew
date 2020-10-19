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
        public bool isColor; // флаг для кнопки начала и завершения аттестации
        public string mainButtonAttestation; // для кнопки начала и завершения аттестации
        public System.Windows.Media.SolidColorBrush currentColor; // цвет для кнопки начала и завершения аттестации
        
        private static Global instance;
        
        public List<car_t> DATA; // Данные по вагонам
        public photo_t photo; // класс содержащий поля фотографий вагонов
        public part_t part; // партии вагонов
        public string user; // имя пользователя

        public List<cause_t> cause; // справочник причин неаттестации
        public List<contractor_t> contractors; // справочник контрагентов
        public List<mat_t> mats; // справочник материалов

        public List<String> Att;

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
            
            cause = getCauses(); // Запрос справочника причин неаттестации
            contractors = getContractors(); // Запрос справочника контрагентов
            mats = getMat(); // Запрос справочника материалов

            photo = new photo_t();
            part = getPart(1); // партии вагонов
            DATA = part.Cars; // Данные по вагонам
            user = ""; // имя пользователя






            Att = new List<string>();
            foreach (car_t cars in DATA)
            {
                switch (cars.Att_code)
                {
                    case 0: Att.Add("CheckCircle"); break;
                    case 1: Att.Add("Times"); break;
                    default: Att.Add("Asterisk"); break;
                }
                
            }


            /////////////////////////////////////////////////////////////////////////////////////////////
            isColor = true;                   // для кнопки начала и завершения аттестации
            mainButtonAttestation = "Начать"; // для кнопки начала и завершения аттестации
                                              //зеленый, для кнопки начала и завершения аттестации
            currentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 173, 1)); 
            ///////////////////////////////////////////////////////////////////////////////////////////////

        }
        public static Global getInstance() // возвращает singleton объекта Global
        {
            if (instance == null)
                instance = new Global();
            return instance;
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
