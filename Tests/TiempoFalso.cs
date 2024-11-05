using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManejoDeTiempos
{
    public class TiempoFalso : Tiempo
    {
        private DateTime tiempo;

        public TiempoFalso()
        {
            //Crea un objeto DateTime con la fecha 14/10/2024(Lunes) con la hora en 00:00:00
            tiempo = new DateTime(2024, 10, 14, 12, 0, 0);
        }
        public void cambiarTiempo()
        {
            tiempo = new DateTime(2024, 10, 14, 23, 0, 0);
        }
        public override DateTime Now()
        {
            return tiempo;
        }

        public void AgregarDias(int cantidad)
        {
            tiempo = tiempo.AddDays(cantidad);
        }

        public void AgregarMinutos(int cantidad)
        {
            tiempo = tiempo.AddMinutes(cantidad);
        }
    }
}