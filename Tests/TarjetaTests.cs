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
            // Cargar un monto que exceda el saldo máximo permitido
            tarjetaMedioBoleto.CargarSaldo(1470); // Cargamos un monto que excede el saldo máximo de 36,000
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000)); // Verifica que el saldo sea el máximo
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(470)); // Verifica que haya saldo pendiente

            // Simulamos un uso de la tarjeta (debería reducir el saldo)
            tarjetaMedioBoleto.RegistrarUso(); // Registra un viaje, lo que reduce el saldo en 2000

            
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000-470)); // El saldo debería ser 34,000 después de usar
            Assert.That(tarjetaMedioBoleto.saldo_pendiente, Is.EqualTo(0)); // El saldo pendiente debería ser 0
        }

        [Test]
        public void PagarConSaldoTest()
        {
            tarjetaGratuito.CargarSaldo(1000);
            float saldoInicial = tarjetaMedioBoleto.saldo;

            // Realiza un viaje (simula el pago)
            tarjetaMedioBoleto.RegistrarUso(); // Llama al método sin esperar un valor de retorno

            // Validaciones
            Assert.That(tarjetaMedioBoleto.saldo, Is.LessThan(saldoInicial)); // Verifica que el saldo haya disminuido
        }

        // Test para validar el pago sin saldo suficiente
        [Test]
        public void PagarSinSaldoGratuitoTest()
        {
            tarjetaGratuito.CargarSaldo(0); // Deja la tarjeta sin saldo
            float saldoInicial = tarjetaGratuito.saldo;

            // Intenta realizar un viaje (simula el pago)
            tarjetaGratuito.RegistrarUso(); // Llama al método sin esperar un valor de retorno

            // Validaciones
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial)); // Verifica que el saldo sigue siendo el mismo
        }

        // Simular que pasa el tiempo de 5 minutos en Medio Boleto
        [Test]
        public void PermitirViajeDespuesDe5MinutosTest()
        {
            tarjetaMedioBoleto.RegistrarUso(); // Registra el primer uso

            // Simulamos que han pasado 5 minutos
            Thread.Sleep(300000); // Cambié a 300000ms (5 minutos)

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
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(35000)); // Debe haber descontado el costo del boleto
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
