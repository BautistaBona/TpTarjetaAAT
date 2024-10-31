using System;
using System.Collections.Generic;

namespace TpSube
{
    public class Colectivo
    {
        public string Linea { get; }
        private const float tarifa_medio = 470;
        private const float tarifa_gratuito = 0;
        private const float tarifa_basica = 940;
        private const float saldo_tarifa_min = (-480) + tarifa_basica;

        // Lista para almacenar las lineas del programa
        private static List<string> lineasRegistradas = new List<string>();

        //Metodo para registrar la linea de colectivo
        public Colectivo(string linea)
        {
            Linea = linea;
            lineasRegistradas.Add(linea);
        }

        protected bool EstaEnHorarioPermitido(Tarjeta tarjeta)
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

        public float Aplicar_descuentos_x_usos(Tarjeta tarjeta){ 

            int usos = tarjeta.Obtener_cant_usos_mes();
            if (usos > 29 && usos <= 79)
            {
                return (float)(tarifa_basica * 0.2);
            }
            if (usos > 79 && usos <= 80)
            {
                return (float)(tarifa_basica * 0.25);
            }
            else
            {
                return tarifa_basica;
            }
        }


        //Se encarga el colectivo de cobrar el pasaje
        public bool PagarPasaje(Tarjeta tarjeta)
        {
            if (!tarjeta.PuedeUsarse())           //Primero corroboramos que la tarjeta sea cual sea se puede usar.
            {                           
                return false;
            }

            float tarifa;  
            
            if (tarjeta.GetType() == typeof(Tarjeta))       
            {
            	tarifa = tarifa_basica;
                tarifa = tarifa - (tarifa * Aplicar_descuentos_x_usos(tarjeta)) 
                  //Segundo, se corrobora que la tarjeta sea una tarjeta normal
            }                                                         //y poder aplicar descuentos x uso
            else                                                      //En caso de que no, se busca a que franquicia pertenece y si esta en el 
            {                                                         //horario permitido
                if (EstaEnHorarioPermitido(tarjeta))
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
                float monto_a_actualizar = tarjeta.saldo - tarifa;
                tarjeta.actualizar_saldo(monto_a_actualizar);
                tarjeta.RegistrarUso();
                return true;
            }

            return false;
        }
    }
}

