using NUnit.Framework;
using TpSube;
using System;
using System.Threading;

namespace TarjetaTest
{
    public class TarjetaTest
    {
        private Gratuito_Estudiantes tarjetaGratuito;
        private Medio_Boleto tarjetaMedioBoleto;

        [SetUp]
        public void Setup()
        {
            tarjetaGratuito = new Gratuito_Estudiantes(35000); 
            tarjetaMedioBoleto = new Medio_Boleto(35000); 
        }

        // Test para verificar que el saldo excedente se almacena como pendiente
        [Test]
        public void CargarSaldo_ExcedenteSeAlmacenaComoPendienteTest()
        {
            float montoCarga = 2000; 
            bool resultado = tarjetaGratuito.CargarSaldo(montoCarga);

           
            Assert.That(resultado, Is.True);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaGratuito.saldo_pendiente, Is.EqualTo(1000));
        }

        // Test para validar que después de realizar un viaje se acredita el saldo pendiente
        [Test]
        public void RecargaConSaldoPendienteTest()
        {
            
            tarjetaMedioBoleto.CargarSaldo(2000); 
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000)); 
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(1000)); 
            
            tarjetaMedioBoleto.RegistrarUso(); 


            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000)); 
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(530));
        }

        [Test]
        public void PagarConSaldoTest()
        {
            tarjetaGratuito.CargarSaldo(1000);
            float saldoInicial = tarjetaMedioBoleto.saldo;

            
            tarjetaMedioBoleto.RegistrarUso(); 

            Assert.That(tarjetaMedioBoleto.saldo, Is.LessThan(saldoInicial));

        }
        // Test para validar el pago de gratuito
        [Test]
        public void PagarSinSaldoGratuitoTest()
        {
            tarjetaGratuito.CargarSaldo(0); 
            float saldoInicial = tarjetaGratuito.saldo;

            
            tarjetaGratuito.RegistrarUso(); 

            
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial)); // Verifica que el saldo sigue siendo el mismo
        }

        
        [Test]
        public void PermitirViajeDespuesDe5MinutosTest()
        {
            tarjetaMedioBoleto.RegistrarUso(); 

           
            Thread.Sleep(300000); 

            // Intentamos registrar otro uso
            bool puedeUsarse = tarjetaMedioBoleto.PuedeUsarse();
            Assert.That(puedeUsarse, Is.True); 
        }

        // Test para validar que una tarjeta de FranquiciaCompleta siempre puede pagar un boleto
        [Test]
        public void FranquiciaCompletaPuedePagarBoletoTest()
        {

           
            tarjetaGratuito.RegistrarUso();

            
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(35000)); 
        }

        // Test para validar que el monto del boleto pagado con Medio Boleto es siempre la mitad del normal
        [Test]
        public void MedioBoletoDescuentoTest()
        {


            tarjetaMedioBoleto.RegistrarUso();


            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(35000 - 470));
        }

        // Test para validar que la tarjeta no pueda quedar con menos saldo que el permitido
        [Test]
        public void NoPermitirSaldoNegativoTest()
        {
            tarjetaGratuito.CargarSaldo(0);
            float saldoInicial = tarjetaGratuito.saldo;


            tarjetaGratuito.RegistrarUso();


            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial));
        }
    }
}