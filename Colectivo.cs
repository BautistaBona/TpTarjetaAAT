using System;
using System.Collections.Generic;

namespace TpSube
{
    public abstract class Colectivo
    {
        public string Linea { get; }
        protected readonly float tarifa_medio;
        protected readonly float tarifa_basica;
        protected readonly float saldo_tarifa_min;
        protected const float tarifa_gratuito = 0;

        // Lista para almacenar las lineas del programa
        private static List<string> lineasRegistradas = new List<string>();

        //Metodo para registrar la linea de colectivo
        public Colectivo(string linea, float tarifaBasica)
        {
            Linea = linea;
            tarifa_basica = tarifaBasica;
            tarifa_medio = tarifa_basica / 2;
            saldo_tarifa_min = tarifa_basica - 480;
            lineasRegistradas.Add(linea);
        }

        public float obtener_tarifa_medio ()
        {

            return tarifa_medio;

        }
        public bool EstaEnHorarioPermitido(Tarjeta tarjeta)
        {
            DateTime ahora = DateTime.Now;

            return ahora.DayOfWeek != DayOfWeek.Saturday &&   // Lunes a viernes de 6 a 22
                   ahora.DayOfWeek != DayOfWeek.Sunday &&
                   ahora.TimeOfDay >= new TimeSpan(6, 0, 0) &&
                   ahora.TimeOfDay <= new TimeSpan(22, 0, 0);
        }

        public float obtener_tarifa(Tarjeta tarjeta)
        {
            if (tarjeta is Medio_Boleto)
            {
                return tarifa_medio;
            }
            else
            {
                return tarifa_gratuito;
            }
        }

        public float Aplicar_descuentos_x_usos(Tarjeta tarjeta)
        {
            int usos = tarjeta.Obtener_cant_usos_mes();
            if (usos > 29 && usos <= 79)
            {
                return (float)(0.2);
            }
            if (usos > 79 && usos <= 80)
            {
                return (float)(0.25);
            }
            else
            {
                return 1;
            }
        }


        //Se encarga el colectivo de cobrar el pasaje
        public bool PagarPasaje(Tarjeta tarjeta)
        {
            float tarifa;

            if (tarjeta.GetType() == typeof(Tarjeta))
            {
                tarifa = tarifa_basica;
                tarifa = tarifa * Aplicar_descuentos_x_usos(tarjeta);  //Segundo, se corrobora que la tarjeta sea una tarjeta normal
            }                                                          //y poder aplicar descuentos x uso
            else                                                      //En caso de que no, se busca a que franquicia pertenece, si esta en el 
            {                                                         //horario permitido y si puede usarse segun sus limitaciones
                if (EstaEnHorarioPermitido(tarjeta) && tarjeta.PuedeUsarse())
                {
                    tarifa = obtener_tarifa(tarjeta);
                }
                else
                {
                    tarifa = tarifa_basica;
                }
            }

            if (tarjeta.saldo - tarifa >= tarjeta.obtener_saldo_negativo_maximo())  //Tercero, se corrobora que el saldo de la tarjeta sea suficiente
            {
         
                tarjeta.actualizar_saldo(tarifa);

                tarjeta.RegistrarUso();
                return true;
            }

            return false;
        }
    }

    public class Urbano : Colectivo
    {
        public Urbano(string linea, float tarifaBasica) : base(linea, tarifaBasica) { }

    }

    public class InterUrbano : Colectivo
    {
        public InterUrbano(string linea, float tarifaBasica) : base(linea, tarifaBasica) { }

    }
}
