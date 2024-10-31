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

        public float obtener_tarifa(Tarjeta tarjeta)
        {
            if (tarjeta is Medio_Boleto)
            {
                if (!tarjeta.PuedeUsarse())
                {
                    Console.WriteLine("Debe esperar 5 minutos entre viajes para usar el medio boleto.");
                    return tarifa_basica;
                }
                tarjeta.RegistrarUso();
                return tarifa_medio;
            }
            if (tarjeta is Gratuito_Jubilados jubilado)
            {
                if (jubilado.PuedeUsarse())
                {
                    return tarifa_gratuito;
                }
            }
            if (tarjeta is Gratuito_Estudiantes estudiante)
            {
                if (estudiante.PuedeUsarse())
                {
                    estudiante.RegistrarViaje();
                    return tarifa_gratuito;
                }
            }
            return tarifa_basica;
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


        //se encarga el colectivo de cobrar el pasaje
        public bool PagarPasaje(Tarjeta tarjeta)
        {
            if(!tarjeta.PuedeUsarse()){
            	return false;
            }
            
            float tarifa = obtener_tarifa(tarjeta);
            if (tarjeta.GetType() == typeof(Tarjeta))
            {
                tarifa = tarifa - (tarifa * Aplicar_descuentos_x_usos(tarjeta)) 
            }
            if (tarjeta.saldo - tarifa >= tarjeta.obtener_saldo_negativo_maximo())
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

