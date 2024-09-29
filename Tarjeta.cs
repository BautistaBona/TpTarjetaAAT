using System;

namespace TpSube {
  public class Tarjeta {
    private const float saldo_max = 9900;
    private const float saldo_negativo_max = -480;
    
    public float saldo { get; protected set; }

    public Tarjeta(float saldo_inicial) {
      if (saldo_inicial > saldo_max) {
        saldo = saldo_max;
        Console.WriteLine("Saldo inicial excede el saldo máximo, se ha cargado $9900"); 
      } else {
        saldo = saldo_inicial;
      }
    }
    
    public float obtener_saldo_negativo_maximo(){
      return saldo_negativo_max;
    }
    
    public void actualizar_saldo(float monto_a_actualizar){
      saldo = saldo - monto_a_actualizar;
    }  

    public bool CargarSaldo(float monto_carga) {
      if (EsCargaValida(monto_carga)) {
        if (saldo < 0) {
          float deuda = Math.Abs(saldo);
          if (monto_carga > deuda) {
            saldo = monto_carga - deuda;  
            Console.WriteLine($"Se descontaron ${deuda} de la deuda. Su nuevo saldo es de ${saldo}");
          } else {
            saldo += monto_carga;  
            Console.WriteLine($"Se descontaron ${monto_carga} de la deuda. Su deuda restante es de ${Math.Abs(saldo)}");
          }
        } else {
          saldo += monto_carga; 
        }
        if (saldo > saldo_max) {
          float monto_sobrante = saldo - saldo_max;
          saldo = saldo_max;
          Console.WriteLine($"Saldo máximo excedido por ${monto_sobrante}");
          return false;
        }
        Console.WriteLine($"Su tarjeta ha sido cargada, su saldo es de ${saldo}");
        return true;
      }
      return false;
    }

    private bool EsCargaValida(float monto_carga) {
      return monto_carga == 2000 || monto_carga == 3000 || monto_carga == 4000 ||
             monto_carga == 5000 || monto_carga == 6000 || monto_carga == 7000 ||
             monto_carga == 8000 || monto_carga == 9000;
    }
  }

  public class MedioBoleto : Tarjeta {
    public MedioBoleto(float saldo_inicial) : base(saldo_inicial) {}
	private DateTime? ultima_vez_usada;  // Registrar el último uso

        public MedioBoleto(float saldo_inicial) : base(saldo_inicial) {
            ultima_vez_usada = null; 
        }

        // Metodo para verificar si han pasado al menos 5 minutos desde el último uso
        public bool PuedeUsarse() {
            if (ultima_vez_usada == null) {
                return true; 
            }

            TimeSpan tiempo_transcurrido = DateTime.Now - ultima_vez_usada.Value; // Calculo el tiempo transcurrido
            return tiempo_transcurrido.TotalMinutes >= 5;
        }

        // Actualiza el registro
        public void RegistrarUso() {
            ultima_vez_usada = DateTime.Now;
        }
  }

  public class GratuitoJubilados : Tarjeta {
    public GratuitoJubilados(float saldo_inicial) : base(saldo_inicial) {}
  }

  public class GratuitoEstudiantes : Tarjeta {
    public GratuitoEstudiantes(float saldo_inicial) : base(saldo_inicial) {}
  }
}
