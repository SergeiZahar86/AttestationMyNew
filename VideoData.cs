using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attestation
{
    class VideoData
    {
        private byte[] LeftFoto { get; set; }
        private byte[] RightFoto { get; set; }
        private byte[] TopFoto { get; set; }


        public bool IsVideo()
        {
            if (LeftFoto != null & RightFoto != null & TopFoto != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
