using System;

namespace TpSube {
    public class Boleto {
        public string LineaColectivo { get; }
        public float SaldoRestante { get; }
        public float CostoDelPasaje { get; }
        public DateTime FechaDelPasaje { get; }
        public string TipoDeTarjeta { get; }
        
        private static DateTime obtener_fecha_actual() {
            return DateTime.Now;
        }
        
        public Boleto(Colectivo colectivo, Tarjeta tarjeta) {
            LineaColectivo = colectivo.Linea;
            TipoDeTarjeta = tarjeta.obtener_tipo_tarjeta(tarjeta);
            SaldoRestante = tarjeta.saldo;
            CostoDelPasaje = colectivo.obtener_tarifa(tarjeta);
            FechaDelPasaje = obtener_fecha_actual();
        }
        
        // Método para mostrar la información del boleto
        public void MostrarBoleto() {
          Console.WriteLine($"Boleto de la línea: {LineaColectivo}");
          Console.WriteLine($"Costo del pasaje: ${CostoDelPasaje}");
          Console.WriteLine($"Saldo restante en la tarjeta: ${SaldoRestante}");
        }
    }
}
