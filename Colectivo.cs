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
                if (jubilado.PuedeViajar())
                {
                    jubilado.RegistrarViaje();
                    return tarifa_gratuito;
                }
            }
            if (tarjeta is Gratuito_Estudiantes estudiante)
            {
                if (estudiante.PuedeViajar())
                {
                    estudiante.RegistrarViaje();
                    return tarifa_gratuito;
                }
            }
            return tarifa_basica;
        }

        //se encarga el colectivo de cobrar el pasaje
        public bool PagarPasaje(Tarjeta tarjeta)
        {
            if (!tarjeta.PuedeUsarse())
            {
                return false; // No se puede cobrar si no se puede usar
            }

            float tarifa = obtener_tarifa(tarjeta);
            if (tarjeta.saldo - tarifa >= tarjeta.obtener_saldo_negativo_maximo())
            {
                float monto_a_actualizar = tarjeta.saldo - tarifa;
                tarjeta.actualizar_saldo(monto_a_actualizar);
                tarjeta.RegistrarUso(); // Registrar el uso solo si se pudo cobrar
                return true;
            }
            return false;
        }

    }
}
