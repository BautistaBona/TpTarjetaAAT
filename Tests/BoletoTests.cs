using NUnit.Framework;
using TpSube;
using ManejoDeTiempos;

namespace BoletoTest
{
    public class BoletoTest
    {

        private Tarjeta tarjeta;
        private Colectivo colectivo;
        private Medio_Boleto tarjetaMedio;
        private Gratuito_Estudiantes tarjetaGratuito;


        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(5000);
            colectivo = new InterUrbano("Línea 1", 3500);
            tarjetaMedio = new Medio_Boleto(5000);
            tarjetaGratuito = new Gratuito_Estudiantes(0);
        }

        // Test para crear boleto con tarjeta normal y chequear propiedades
        [Test]
        public void CrearBoletoConTarjeta()
        {
            Boleto boleto = new Boleto(colectivo, tarjeta);

            // Verificar propiedades del boleto
            Assert.That(boleto.FechaDelPasaje, Is.GreaterThan(DateTime.MinValue));
            Assert.That(boleto.LineaColectivo, Is.EqualTo(colectivo.Linea));
            Assert.That(boleto.TipoDeTarjeta, Is.EqualTo(tarjeta.obtener_tipo_tarjeta(tarjeta)));
            Assert.That(boleto.CostoDelPasaje, Is.EqualTo(colectivo.obtener_tarifa(tarjeta)));
            Assert.That(boleto.SaldoRestante, Is.EqualTo(tarjeta.saldo));
        }

        // Test para crear boleto con medio y chequear propiedades
        [Test]
        public void CrearBoletoConTarjetaMedio()
        {
            Boleto boleto = new Boleto(colectivo, tarjetaMedio);

            // Verificar propiedades del boleto
            Assert.That(boleto.FechaDelPasaje, Is.GreaterThan(DateTime.MinValue));
            Assert.That(boleto.LineaColectivo, Is.EqualTo(colectivo.Linea));
            Assert.That(boleto.TipoDeTarjeta, Is.EqualTo(tarjetaMedio.obtener_tipo_tarjeta(tarjetaMedio)));
            Assert.That(boleto.CostoDelPasaje, Is.EqualTo(colectivo.obtener_tarifa(tarjetaMedio)));
            Assert.That(boleto.SaldoRestante, Is.EqualTo(tarjetaMedio.saldo));
        }


        // Test para crear boleto con tarjeta gratuita y chequear propiedades
        [Test]
        public void CrearBoletoConTarjetaGratuito()
        {
            Boleto boleto = new Boleto(colectivo, tarjetaGratuito);

            // Verificar propiedades del boleto
            Assert.That(boleto.FechaDelPasaje, Is.GreaterThan(DateTime.MinValue));
            Assert.That(boleto.LineaColectivo, Is.EqualTo(colectivo.Linea));
            Assert.That(boleto.TipoDeTarjeta, Is.EqualTo(tarjetaGratuito.obtener_tipo_tarjeta(tarjetaGratuito)));
            Assert.That(boleto.CostoDelPasaje, Is.EqualTo(colectivo.obtener_tarifa(tarjetaGratuito)));
            Assert.That(boleto.SaldoRestante, Is.EqualTo(tarjetaGratuito.saldo));
        }
    }
}