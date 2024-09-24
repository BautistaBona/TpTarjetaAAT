using System;
using System.Collections.Generic;

namespace TpSube {
  public class Colectivo {
      public string Linea { get; }
      
      private const float tarifa_basica = 940;
      private const float saldo_tarifa_min = (-480) + 940;
      
      // Lista para almacenar las lineas del programa
      private static List<string> lineasRegistradas = new List<string>();

      //Metodo para registrar la linea de colectivo
      public Colectivo(string linea) {
          Linea = linea;
          lineasRegistradas.Add(linea);
      }

    public virtual bool PagarPasaje(Tarjeta tarjeta) {
        if (tarjeta.saldo >= saldo_tarifa_min) {
            float monto_a_actualizar = tarjeta.saldo - tarifa_basica;
            tarjeta.actualizar_saldo(monto_a_actualizar);
          return true;
        }
        return false;
    }
      

      // Método para pagar con la tarjeta
      public Boleto PagarCon(Tarjeta tarjeta) {
          if (PagarPasaje(tarjeta)) {
              return new Boleto(this, tarjeta.saldo);
          }
          return null; // No se pudo pagar, saldo insuficiente
      }
  }
}
