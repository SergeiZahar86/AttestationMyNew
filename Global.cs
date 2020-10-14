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

namespace Attestation
{
    class Global
    {
        private static Global instance;
        public List<car_t> DATA;
        
        public List<String> Reason;

        byte[] leftFoto;
        byte[] rightFoto;
        byte[] topFoto;
        TTransport transport;
        DataProviderService.Client client;

        public int Idx { set; get; }

        private Global()
        {
            this.transport = new TSocket("10.90.90.5", 9090);
            TProtocol proto = new TBinaryProtocol(transport);
            transport.Open();
            this.client = new DataProviderService.Client(proto);

            part_t part = getPart(1);
            DATA = part.Cars;
            






            for (int i = 0; i < 25; i++)
            {




                /*
                int Part_id = part.Cars[i].Part_id;
                int Car_id = part.Cars[i].Car_id;
                string Num = part.Cars[i].Num;
                int Att_code = part.Cars[i].Att_code;
                double Tara = part.Cars[i].Tara;
                int Zone_e = part.Cars[i].Zone_e;
                int Cause_id = part.Cars[i].Cause_id;
                string Cause_name = part.Cars[i].Cause_name;
                double Carrying = part.Cars[i].Carrying;
                string Att_time = part.Cars[i].Att_time;

                DATA.Add(new RowTab(Part_id, Car_id, Num, Att_code, Tara, Zone_e, Cause_id, Cause_name, Carrying, Att_time));
                
                DATA.Add(new RowTab(part.Cars[i].Part_id, part.Cars[i].Car_id, part.Cars[i].Num), part.Cars[i].Att_code,
                    part.Cars[i].Tara, part.Cars[i].Zone_e, part.Cars[i].Cause_id, part.Cars[i].Cause_name,
                    part.Cars[i].Carrying, part.Cars[i].Att_time);
                */
            }

            /*
            Reason = new List<string>();
            Reason.Add("Причина №1");
            Reason.Add("Причина №2");
            Reason.Add("Причина №3");
            Reason.Add("Причина №4");

            DATA = new List<RowTab>();
            for (int i = 0; i < 25; i++)
            {
                string c = "Question";
                //string c = "CheckCircle";
                //string c = "WindowClose";
                leftFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-mark-plötz-2790396.jpg"));
                rightFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-pixabay-258455.jpg"));
                topFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-sergio-souza-3197995.jpg"));
                DATA.Add(new RowTab(i + 1, c, (88345634 + i).ToString(), (float)(i + 0.5),
                    (float)(i + 1.5), (float)(i + 2.5), leftFoto,
                    rightFoto, topFoto, Reason));
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
