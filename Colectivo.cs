using System;
using System.Collections.Generic;

namespace TpSube {
  public class Colectivo {
      public string Linea { get; }
      private const float tarifa_medio = 470;
      private const float tarifa_gratuito = 0;
      private const float tarifa_basica = 940;
      private const float saldo_tarifa_min = (-480) + tarifa_basica;
      
      // Lista para almacenar las lineas del programa
      private static List<string> lineasRegistradas = new List<string>();

      //Metodo para registrar la linea de colectivo
      public Colectivo(string linea) {
          Linea = linea;
          lineasRegistradas.Add(linea);
      }

      public float obtener_tarifa(Tarjeta tarjeta){
        if(tarjeta is MedioBoleto){
		if (!medioBoleto.PuedeUsarse()) {
			Console.WriteLine("Debe esperar 5 minutos entre viajes para usar el medio boleto.");
                    	return tarifa_basica;
                }
    		medioBoleto.RegistrarUso();
            	return tarifa_medio;
        }
        if(tarjeta is GratuitoJubilados || tarjeta is GratuitoEstudiantes){
            return tarifa_gratuito;
        }
        else{
            return tarifa_basica;
        }
      }

    //se encarga el colectivo de cobrar el pasaje
    public bool PagarPasaje(Tarjeta tarjeta) {
        float tarifa = obtener_tarifa(tarjeta);
        if(tarjeta.saldo - tarifa >= tarjeta.obtener_saldo_negativo_maximo()){
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
