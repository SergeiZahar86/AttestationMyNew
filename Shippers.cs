using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attestation
{
    class Shippers
    {
        public int Id { get; set; } // Порядковый номер 
        public string Name { get; set; } // Название грузоотправителя
        public Shippers(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
