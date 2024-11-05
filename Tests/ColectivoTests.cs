using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpSube;
using ManejoDeTiempos;

namespace TpTarjetaTests
{
    internal class ColectivoTests
    {
        private Gratuito_Estudiantes tarjetaGratuito;
        private Colectivo colectivo;
        private Medio_Boleto tarjetaMedioBoleto;
        private Gratuito_Estudiantes tarjetaGratuito2;
        private Medio_Boleto tarjetaMedio2;
        private TiempoFalso tiempoFalso;
        private Tarjeta tarjetaNormal;
        private Tarjeta tarjetaNormal2;

        [SetUp]
        public void Setup()
        {
            tarjetaGratuito = new Gratuito_Estudiantes(5000);
            colectivo = new InterUrbano("Línea 1", 1200);
            tarjetaMedioBoleto = new Medio_Boleto(35000);
            tarjetaGratuito2 = new Gratuito_Estudiantes(0);
            tarjetaMedio2 = new Medio_Boleto(0);
            tiempoFalso = new TiempoFalso();
            tarjetaNormal = new Tarjeta(0);
            tarjetaNormal2 = new Tarjeta(1000);
        }


        //Test Iteracion 2
        //Chequea que se pueda pagar cuando tenes saldo
        [Test]
        public void PagarConSaldo()
        {

            float saldoInicial = tarjetaNormal2.saldo;

            colectivo.PagarPasaje(tarjetaNormal2, tiempoFalso);

            Assert.That(tarjetaNormal2.saldo, Is.LessThan(saldoInicial));
        }

        // Test para chequear que no se pueda pagar con saldo insuficiente, que tambien chequea que no puedas pagar si vas a quedar con menos de -480.
        [Test]
        public void PagarSinSaldoSuficiente()
        {

            
            Assert.That(colectivo.PagarPasaje(tarjetaNormal, tiempoFalso), Is.False);

            
        }

        [Test]
        public void PagarConSaldoNegativo()
        {
            colectivo.PagarPasaje(tarjetaNormal2, tiempoFalso);
            tarjetaNormal2.CargarSaldo(2000);
          
            Assert.That(tarjetaNormal2.saldo, Is.EqualTo(1800));

        }

        // Test para validar el pago de gratuito aunque no tenga saldo
        [Test]
        public void PagarSinSaldoGratuitoTest()
        {

            float saldoInicial = tarjetaGratuito2.saldo;
            colectivo.PagarPasaje(tarjetaGratuito2, tiempoFalso);

            Assert.That(tarjetaGratuito2.saldo, Is.EqualTo(saldoInicial));
        }


        //Test que chequea que aunque tengas saldo te descuente el pasaje gratuito
        [Test]
        public void PagarGratuitoConSaldo()
        {

            float saldoInicial = tarjetaGratuito.saldo;
            colectivo.PagarPasaje(tarjetaGratuito, tiempoFalso);

            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial));
        }

        // Test para validar que el monto del boleto pagado con Medio Boleto es siempre la mitad del normal
        [Test]
        public void MedioBoletoDescuento()
        {
            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(35000 - 600));
        }

        //Test Iteracion3
        //Verifica que no se puedan pagar 3 gratuitos y este ultimo lo cobra tarifa completa
        [Test]
        public void TestTarifaNormalDespuesDelSegundoViajeGratuito()
        {

            colectivo.PagarPasaje(tarjetaGratuito, tiempoFalso);
            colectivo.PagarPasaje(tarjetaGratuito, tiempoFalso);
            colectivo.PagarPasaje(tarjetaGratuito, tiempoFalso);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(3800));
        }


        // Test para validar que después de realizar un viaje se acredita el saldo pendiente
        [Test]
        public void RecargaConSaldoPendienteTest()
        {
            tarjetaMedioBoleto.CargarSaldo(2000);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaMedioBoleto.obtener_saldo_pendiente(), Is.EqualTo(400));
        }





        [Test]
        public void PagarConSaldoMedio()
        {

            float saldoInicial = tarjetaMedioBoleto.saldo;

            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);

            Assert.That(tarjetaMedioBoleto.saldo, Is.LessThan(saldoInicial));
        }

        

        // Test para validar que el monto del boleto pagado con Medio Boleto es siempre la mitad del normal
        [Test]
        public void MedioBoletoDescuentoTest()
        {
            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(35000 - 600));
        }

        


        // Test de saldo pendiente: Validar que al recargar la tarjeta se descuente correctamente cualquier saldo adeudado
        [Test]
        public void TestSaldoPendienteDescuento()
        {
            tarjetaMedioBoleto.CargarSaldo(2000);
            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaMedioBoleto.obtener_saldo_pendiente(), Is.EqualTo(400));
        }


        //Iteracion 4
        [Test]
        public void UsoFrecuente()
        {

           tarjetaNormal2.CargarSaldo(2000);

           for (int i = 0; i < 30;i++) {

                tarjetaNormal2.RegistrarUso(tiempoFalso);
            
           }

           colectivo.PagarPasaje(tarjetaNormal2, tiempoFalso);
           Assert.That(tarjetaNormal2.saldo, Is.EqualTo(2040));

        }

        [Test]
        public void UsoFrecuente80Viajes()
        {

            tarjetaNormal2.CargarSaldo(2000);

            for (int i = 0; i < 80; i++)
            {

                tarjetaNormal2.RegistrarUso(tiempoFalso);

            }

            colectivo.PagarPasaje(tarjetaNormal2, tiempoFalso);
            Assert.That(tarjetaNormal2.saldo, Is.EqualTo(2100));

        }

        [Test]
        public void UsarFueraHorarioGratuito()
        {

            tiempoFalso.cambiarTiempo();
            colectivo.PagarPasaje(tarjetaGratuito, tiempoFalso);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(3800));
        }

        [Test]
        public void UsarFueraHorarioMedio()
        {

            tiempoFalso.cambiarTiempo();
            colectivo.PagarPasaje(tarjetaMedioBoleto, tiempoFalso);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(33800));
        }
    }
}
