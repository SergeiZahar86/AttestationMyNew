using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Attestation
{
    class Global
    {
        private static Global instance;
        public List<RowTab> DATA;

        byte[] leftFoto;
        byte[] rightFoto;
        byte[] topFoto;


        public int Idx { set; get; }

        private Global()
        {
            DATA = new List<RowTab>();
            for (int i = 0; i < 26; i++)
            {
                string c = "Question";
                //string c = "CheckCircle";
                //string c = "WindowClose";
                leftFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-mark-plötz-2790396.jpg"));
                rightFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-pixabay-258455.jpg"));
                topFoto = ImageToByteArray(System.Drawing.Image.FromFile("C:/VisualStudioProject/AttestationHorizontal/Resources/pexels-sergio-souza-3197995.jpg"));
                /*
                if (i % 2 == 0)
                {
                    c = "LongArrowRight";
                    leftFoto = null;
                    rightFoto = null;
                    topFoto = null;
                }
                */
                DATA.Add(new RowTab(i + 1, c, (88345634 + i).ToString(), (float)(i + 0.5),
                    (float)(i + 1.5), (float)(i + 2.5), leftFoto,
                    rightFoto, topFoto));
            }
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
    }
}
