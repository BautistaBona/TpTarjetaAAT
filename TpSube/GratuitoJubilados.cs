using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManejoDeTiempos;

namespace TpSube
{
    public class Gratuito_Jubilados : Tarjeta
    {
        private DateTime ultima_fecha_viaje;
        public Gratuito_Jubilados(float saldo_inicial) : base(saldo_inicial) { }

        public override bool PuedeUsarse(Tiempo tiempo)
        {
            return true;
        }

        public override void RegistrarUso(Tiempo tiempo)
        {
            ultima_fecha_viaje = tiempo.Now();
        }
    }
}

