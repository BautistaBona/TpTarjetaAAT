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
            tarjeta = new Tarjeta(5000);
            colectivo = new InterUrbano("Línea 1", 3500);
        }

        // Test para crear un boleto con tarjeta
        [Test]
        public void CrearBoletoConTarjetaTest()
        {
            Boleto boleto = new Boleto(colectivo, tarjeta);

            // Verificar propiedades del boleto
            Assert.That(boleto.FechaDelPasaje, Is.GreaterThan(DateTime.MinValue));
            Assert.That(boleto.LineaColectivo, Is.EqualTo(colectivo.Linea));
            Assert.That(boleto.TipoDeTarjeta, Is.EqualTo(tarjeta.obtener_tipo_tarjeta(tarjeta)));
            Assert.That(boleto.CostoDelPasaje, Is.EqualTo(colectivo.obtener_tarifa(tarjeta)));
            Assert.That(boleto.SaldoRestante, Is.EqualTo(tarjeta.saldo));
        }
    }
}


