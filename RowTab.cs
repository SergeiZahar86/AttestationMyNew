using System;
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
        public RowTab(int Id, string isOk, string VagNum, float Tara, float TaraNSI,
            float TaraDelta, byte[] LeftFoto, byte[] RightFoto, byte[] TopFoto)

            /*
            public RowTab(int Id, bool isOk, string VagNum, float Tara, float TaraNSI,
            float TaraDelta, System.Drawing.Image LeftFoto, System.Drawing.Image RightFoto, System.Drawing.Image TopFoto)
            */
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
        }
        public int Id { get; set; }
        public string isOk { get; set; }
        public string VagNum { get; set; }
        public float Tara { get; set; }
        public float TaraNSI { get; set; }
        public float TaraDelta { get; set; }
        public string Video { get; set; }
        /*
        public System.Drawing.Image LeftFoto { get; set; }
        public System.Drawing.Image RightFoto { get; set; }
        public System.Drawing.Image TopFoto { get; set; }
        */
        public byte[] LeftFoto { get; set; }
        public byte[] RightFoto { get; set; }
        public byte[] TopFoto { get; set; }
    }
}

      