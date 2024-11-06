using System;
using ManejoDeTiempos;

namespace TpSube
{
    public class Tarjeta
    {
        private const float saldo_max = 36000;  // actualizo a 36000 
        private const float saldo_negativo_max = -480;
        private float saldo_pendiente;  //atributo para el saldo pendiente
        public float saldo { get; protected set; }
        private DateTime? ultima_vez_usada;
        private int cantidad_usos_mes;

        public Tarjeta(float saldo_inicial)
        {
            saldo_pendiente = 0;
            if (saldo_inicial > saldo_max)
            {
                saldo = saldo_max;
                Console.WriteLine("Saldo inicial excede el saldo máximo, se ha cargado $36000");
            }
            else
            {
                saldo = saldo_inicial;
            }
            ultima_vez_usada = null;
        }

        public float obtener_saldo_pendiente()
        {
            return saldo_pendiente;
        }


        public float obtener_saldo_negativo_maximo()
        {
            return saldo_negativo_max;
        }

        public int Obtener_cant_usos_mes()
        {
            return cantidad_usos_mes;
        }

        public void actualizar_saldo(float monto_a_actualizar)
        {
            // si tenemos saldo pendiente, se acredita antes de actualizar
            if (saldo_pendiente > 0)
            {
                if (monto_a_actualizar >= saldo_pendiente)
                {
                    saldo -= monto_a_actualizar;
                    saldo += saldo_pendiente;
                    saldo_pendiente = 0;
                }
                else
                {
                    saldo_pendiente -= monto_a_actualizar;

                }
            }
            else
            {
                saldo -= monto_a_actualizar;
            }
        }

        public virtual void RegistrarUso(Tiempo tiempo)
        {
            if (ultima_vez_usada == null || ultima_vez_usada.Value.Month != tiempo.Now().Month)
            {
                cantidad_usos_mes = 0;
            }
            ultima_vez_usada = tiempo.Now();
            cantidad_usos_mes++;
        }

        public virtual bool PuedeUsarse(Tiempo tiempo)
        {
            return true;
        }


        public bool CargarSaldo(float monto_carga)
        {
            if (EsCargaValida(monto_carga))
            {
                if (saldo < 0)
                {
                    float deuda = Math.Abs(saldo);
                    if (monto_carga > deuda)
                    {
                        saldo = monto_carga - deuda;
                        Console.WriteLine($"Se descontaron ${deuda} de la deuda. Su nuevo saldo es de ${saldo}");
                    }
                    else
                    {
                        saldo += monto_carga;
                        Console.WriteLine($"Se descontaron ${monto_carga} de la deuda. Su deuda restante es de ${Math.Abs(saldo)}");
                    }
                }
                else
                {
                    saldo += monto_carga;
                }

                // gestionar saldo pendiente si excede el máximo
                if (saldo > saldo_max)
                {
                    saldo_pendiente = saldo - saldo_max;  // asignamos el exceso como saldo pendiente
                    saldo = saldo_max;
                    Console.WriteLine($"Saldo máximo excedido. Se ha cargado hasta llegar a $36000 y el saldo pendiente es de ${saldo_pendiente}");
                }
                else if (saldo_pendiente > 0 && saldo < saldo_max)
                {
                    float espacioDisponible = saldo_max - saldo;
                    if (saldo_pendiente <= espacioDisponible)
                    {
                        saldo += saldo_pendiente;
                        saldo_pendiente = 0;
                    }
                    else
                    {
                        saldo += espacioDisponible;
                        saldo_pendiente -= espacioDisponible;
                    }
                }

                Console.WriteLine($"Su tarjeta ha sido cargada, su saldo es de ${saldo}");
                return true;
            }
            return false;
        }

        private bool EsCargaValida(float monto_carga)
        {
            return monto_carga == 2000 || monto_carga == 3000 || monto_carga == 4000 ||
                   monto_carga == 5000 || monto_carga == 6000 || monto_carga == 7000 ||
                   monto_carga == 8000 || monto_carga == 9000;
        }

        public string obtener_tipo_tarjeta(Tarjeta tarjeta)
        {
            string tipo_tarjeta = tarjeta.GetType().ToString();
            tipo_tarjeta = tipo_tarjeta.Replace("_", " ");
            return tipo_tarjeta;
        }
    }
}
