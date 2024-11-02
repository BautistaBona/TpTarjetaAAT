using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpSube;

namespace TpTarjetaTests
{
    internal class ColectivoTests
    {
        private Gratuito_Estudiantes tarjetaGratuito;
        private Colectivo colectivo;
        private Medio_Boleto tarjetaMedioBoleto;
        private Gratuito_Estudiantes tarjetaGratuito2;
        private Medio_Boleto tarjetaMedio2;

        [SetUp]
        public void Setup()
        {
            tarjetaGratuito = new Gratuito_Estudiantes(5000);
            colectivo = new InterUrbano("Línea 1", 1200);
            tarjetaMedioBoleto = new Medio_Boleto(35000);
            tarjetaGratuito2 = new Gratuito_Estudiantes(0);
            tarjetaMedio2 = new Medio_Boleto(0);
        }

        [Test]
        public void TestTarifaNormalDespuesDelSegundoViajeGratuito()
        {
            
            colectivo.PagarPasaje(tarjetaGratuito);
            colectivo.PagarPasaje(tarjetaGratuito);
            colectivo.PagarPasaje(tarjetaGratuito);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(3800));
        }

        // Test para validar que después de realizar un viaje se acredita el saldo pendiente
        [Test]
        public void RecargaConSaldoPendienteTest()
        {
            tarjetaMedioBoleto.CargarSaldo(2000);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            colectivo.PagarPasaje(tarjetaMedioBoleto);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaMedioBoleto.obtener_saldo_pendiente(), Is.EqualTo(400));
        }

        [Test]
        public void PagarConSaldoTest()
        {

            float saldoInicial = tarjetaMedioBoleto.saldo;

            colectivo.PagarPasaje(tarjetaMedioBoleto);

            Assert.That(tarjetaMedioBoleto.saldo, Is.LessThan(saldoInicial));
        }

        // Test para validar el pago de gratuito
        [Test]
        public void PagarSinSaldoGratuitoTest()
        {

            float saldoInicial = tarjetaGratuito2.saldo;
            colectivo.PagarPasaje(tarjetaGratuito2);

            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(saldoInicial));
        }

        // Test para validar que el monto del boleto pagado con Medio Boleto es siempre la mitad del normal
        [Test]
        public void MedioBoletoDescuentoTest()
        {
            colectivo.PagarPasaje(tarjetaMedioBoleto);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(35000 - 600));
        }

        // Test para validar que la tarjeta no pueda quedar con menos saldo que el permitido
        [Test]
        public void NoPermitirSaldoNegativoTest()
        {

            Assert.That(colectivo.PagarPasaje(tarjetaMedio2), Is.False);

        }


        // Test de saldo pendiente: Validar que al recargar la tarjeta se descuente correctamente cualquier saldo adeudado
        [Test]
        public void TestSaldoPendienteDescuento()
        {
            tarjetaMedioBoleto.CargarSaldo(2000);
            colectivo.PagarPasaje(tarjetaMedioBoleto);
            Assert.That(tarjetaMedioBoleto.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaMedioBoleto.obtener_saldo_pendiente(), Is.EqualTo(400));
        }

    }
}
