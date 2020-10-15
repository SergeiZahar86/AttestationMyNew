using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Thrift.Transport;
using Thrift.Protocol;
using iTextSharp.text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Diagnostics;
using System.Windows;

namespace Attestation
{
    class Global
    {
        /*
        MqttClient clientMQTT;
        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            
            Debug.WriteLine("Подписан на id = " + e.MessageId);
            MessageBox.Show(e.ToString());
        }
        */


        /*
        public List<String> Reason;
        byte[] leftFoto;
        byte[] rightFoto;
        byte[] topFoto;
        */
        private static Global instance;
        public List<car_t> DATA;
        public photo_t photo;
        public part_t part;
        public List<cause_t> cause;
        
        TTransport transport;
        DataProviderService.Client client;

        public int Idx { set; get; }

        private Global()
        {
            
            this.transport = new TSocket("10.90.90.5", 9090);
            TProtocol proto = new TBinaryProtocol(transport);
            transport.Open();
            this.client = new DataProviderService.Client(proto);

            part = getPart(1);
            DATA = part.Cars;
            photo = new photo_t();
            cause = getCauses();

            /*
            try
            {
                String id = Guid.NewGuid().ToString();
                clientMQTT = new MqttClient("10.90.90.5");
                
                clientMQTT.MqttMsgSubscribed += client_MqttMsgSubscribed;
                ushort msgId = clientMQTT.Subscribe(new string[] { "/my_topic" },
                    new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                byte code = clientMQTT.Connect(id, "root", "root");
            }
            catch (Exception a)
            {

            }
            */


        }
        public static Global getInstance()//*******
        {
            if (instance == null)
                instance = new Global();
            return instance;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

        public String chLogin(String Login, String Password, String Emple_id)
        {
            return this.client.chLogin(Login, Password, Emple_id);
        }
        public List<cause_t> getCauses()
        {
            return this.client.getCauses();
        }
        public part_t getPart(int id)
        {
            return this.client.getPart(id);
        }
        public photo_t getPhoto(int part_id, int car_id)
        {
            return this.client.getPhoto(part_id, car_id);
        }
    }
}
