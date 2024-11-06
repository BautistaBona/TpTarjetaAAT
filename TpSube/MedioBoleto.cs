using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManejoDeTiempos;

namespace TpSube
{
    public class Medio_Boleto : Tarjeta
    {
        private DateTime? ultima_fecha_viaje;  // Registrar el último uso
        private const float costoViaje = 470;
        private int viajes_del_dia = 0;

        public Medio_Boleto(float saldo_inicial) : base(saldo_inicial)
        {
            ultima_fecha_viaje = null;
        }


        public override void RegistrarUso(Tiempo tiempo)
        {


            ultima_fecha_viaje = tiempo.Now();
            viajes_del_dia++;

        }


        // Metodo para verificar si han pasado al menos 5 minutos desde el último uso
        public override bool PuedeUsarse(Tiempo tiempo)
        {

            if (ultima_fecha_viaje != null)
            {
                if (ultima_fecha_viaje.Value.Day != tiempo.Now().Day)
                {
                    viajes_del_dia = 0;
                    ultima_fecha_viaje = tiempo.Now();
                }




                TimeSpan tiempo_transcurrido = tiempo.Now() - ultima_fecha_viaje.Value;
                if (viajes_del_dia < 4 && tiempo_transcurrido.TotalMinutes >= 5)
                {
                    return true;
                }

                return false;

            }
            else
            {
                return true;
            }
        }

    }
}
