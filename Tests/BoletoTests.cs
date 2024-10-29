using NUnit.Framework;
using TpSube;

namespace BoletoTest
{
    public class BoletoTest
    {
        private Tarjeta tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(5000); // Inicializa con saldo suficiente
            colectivo = new Colectivo("Línea 1");
        }

        // Test para crear un boleto con tarjeta
        [Test]
        public void CrearBoletoConTarjetaTest()
        {
            Boleto boleto = new Boleto(colectivo, tarjeta);

            // Verificar propiedades del boleto
            Assert.That(boleto.FechaDelPasaje, Is.GreaterThan(DateTime.MinValue)); // Verifica que la fecha sea mayor que la fecha mínima
            Assert.That(boleto.LineaColectivo, Is.EqualTo(colectivo.Linea)); // Verifica línea de colectivo
            Assert.That(boleto.TipoDeTarjeta, Is.EqualTo(tarjeta.obtener_tipo_tarjeta(tarjeta))); // Verifica tipo de tarjeta
            Assert.That(boleto.CostoDelPasaje, Is.EqualTo(colectivo.obtener_tarifa(tarjeta))); // Verifica costo del pasaje
            Assert.That(boleto.SaldoRestante, Is.EqualTo(tarjeta.saldo)); // Verifica saldo restante
        }
    }
}

