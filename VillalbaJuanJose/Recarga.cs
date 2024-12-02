using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillalbaJuanJose
{
    class Recarga
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
    }
}
