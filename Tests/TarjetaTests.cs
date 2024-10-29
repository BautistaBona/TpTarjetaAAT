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
            tarjetaGratuito = new Gratuito_Estudiantes(35000); // Inicializa con saldo suficiente
            tarjetaMedioBoleto = new Medio_Boleto(35000); // Tarjeta Medio Boleto
        }

        // Test para verificar que el saldo excedente se almacena como pendiente
        [Test]
        public void CargarSaldo_ExcedenteSeAlmacenaComoPendienteTest()
        {
            float montoCarga = 2000; // Excedente que deja saldo pendiente
            bool resultado = tarjetaGratuito.CargarSaldo(montoCarga);

            // Validaciones
            Assert.That(resultado, Is.True);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaGratuito.saldo_pendiente, Is.EqualTo(1000));
        }

        // Test para validar que después de realizar un viaje se acredita el saldo pendiente
        [Test]
        public void RecargaConSaldoPendienteTest()
        {
            
            tarjetaMedioBoleto.CargarSaldo(1470); 
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000)); 
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(470)); 

            // Simulamos un uso de la tarjeta (debería reducir el saldo)
            tarjetaMedioBoleto.RegistrarUso(); 

            
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000-470)); 
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(0)); 
        }

        [Test]
        public void PagarConSaldoTest()
        {
            tarjetaGratuito.CargarSaldo(1000);
            float saldoInicial = tarjetaMedioBoleto.saldo;

            
            tarjetaMedioBoleto.RegistrarUso(); 

            
            Assert.That(tarjetaMedioBoleto.saldo, Is.LessThan(saldoInicial)); 
        }

        // Test para validar el pago sin saldo suficiente
        [Test]
        public void PagarSinSaldoGratuitoTest()
        {
            tarjetaGratuito.CargarSaldo(0); 
            float saldoInicial = tarjetaGratuito.saldo;

           
            tarjetaGratuito.RegistrarUso(); 

            
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial)); 
        }

        // Simular que pasa el tiempo de 5 minutos en Medio Boleto
        [Test]
        public void PermitirViajeDespuesDe5MinutosTest()
        {
            tarjetaMedioBoleto.RegistrarUso(); // Registra el primer uso

            
            Thread.Sleep(300000); 

            // Intentamos registrar otro uso
            bool puedeUsarse = tarjetaMedioBoleto.PuedeUsarse();
            Assert.That(puedeUsarse, Is.True); // Ahora debería permitir el uso
        }

        // Test para validar que una tarjeta de FranquiciaCompleta siempre puede pagar un boleto
        [Test]
        public void FranquiciaCompletaPuedePagarBoletoTest()
        {
            
            // Se registra un uso de la tarjeta de Franquicia Completa
            tarjetaGratuito.RegistrarUso();

            // Verificamos que el saldo se ha reducido correctamente
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(35000)); 
        }

        // Test para validar que el monto del boleto pagado con Medio Boleto es siempre la mitad del normal
        [Test]
        public void MedioBoletoDescuentoTest()
        {

           
            tarjetaMedioBoleto.RegistrarUso(); 

            
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(35000-470)); 
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
