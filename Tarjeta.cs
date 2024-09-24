using System;

public class Tarjeta{
  //Constantes propias del sistema de la tarjeta
    private const float saldo_max = 9900;
    private const float saldo_negativo_max = -480;

  public float saldo{ get; private set; }

  //Metodo para crear una tarjeta con su saldo
  public Tarjeta (float saldo_inicial){
    if(saldo_inicial > saldo_max){
      saldo = saldo_max;
      Console.WriteLine("Saldo inicial excede el saldo maximo, se ha cargado 9400"); 
    }
    else{
      saldo = saldo_inicial;
    }
  }
  
  public void actualizar_saldo(float monto_a_actualizar){
      saldo = saldo - monto_a_actualizar;
    }  

  //Metodo para cargar saldo a la tarjeta, devolviendo true o false dependiendo si la operacion fue existosa o no
  public bool CargarSaldo(float monto_carga) {
    if(EsCargaValida(monto_carga)){        //Se verifica si el monto de carga es valido
      float nuevo_saldo = saldo + monto_carga;
      if(nuevo_saldo > saldo_max){         //Se verifica si el nuevo saldo es valido
        float monto_sobrante = nuevo_saldo - saldo_max;
        Console.WriteLine($"Saldo maximo excedido por {monto_sobrante}$");
        return false;
      }
      else{
        saldo = nuevo_saldo;
        Console.WriteLine($"Su tarjeta ha sido cargada, su saldo es de {saldo}$"); 
        return true;
      }
    }
    else{
      return false;
    }
  }

  //Verificar si el monto de carga es un monto válido
  private bool EsCargaValida(float monto_carga){
    return monto_carga == 2000 || monto_carga == 3000 || monto_carga == 4000 ||
     monto_carga == 5000 || monto_carga == 6000 || monto_carga == 7000 ||
     monto_carga == 8000 || monto_carga == 9000;
  }
  
  
  
  
}
