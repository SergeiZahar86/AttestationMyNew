using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attestation
{
    class Consigners // справочник Грузополучателя
    {
        public int Id { get; set; } // Порядковый номер 
        public string Name { get; set; } // Название грузополучателя
        public Consigners( int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
