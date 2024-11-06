using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManejoDeTiempos;

namespace TpSube
{
    public class Gratuito_Estudiantes : Tarjeta
    {
        private int viajes_del_dia = 0; // Contador de viajes por día
        private DateTime ultima_fecha_viaje;

        public Gratuito_Estudiantes(float saldo_inicial) : base(saldo_inicial) { }

        public override bool PuedeUsarse(Tiempo tiempo)
        {


            // Si es un nuevo día, reinicio el contador de viajes
            if (tiempo.Now() != ultima_fecha_viaje)
            {
                viajes_del_dia = 0;
                ultima_fecha_viaje = tiempo.Now();
            }

            // Verifico si se pueden hacer más viajes
            if (viajes_del_dia < 2)
            {
                return true;
            }

            Console.WriteLine("Límite diario de viajes alcanzado.");
            return false;
        }

        public override void RegistrarUso(Tiempo tiempo)
        {
            viajes_del_dia++;
        }
    }
}
