using NUnit.Framework;
using TpSube;
using System;
using System.Threading;

namespace TarjetaTest
{
    public class TarjetaTest
    {
        private Urbano colectivo;
        private Gratuito_Estudiantes tarjetaGratuito;
        private Medio_Boleto tarjetaMedioBoleto;
        private Gratuito_Estudiantes tarjetaGratuito2;
        private Medio_Boleto tarjetaMedio2;
        [SetUp]
        public void Setup()
        {
            colectivo = new Urbano("Línea 2", 1200);
            tarjetaGratuito = new Gratuito_Estudiantes(35000);
            tarjetaMedioBoleto = new Medio_Boleto(35000);
            tarjetaGratuito2 = new Gratuito_Estudiantes(0);
            tarjetaMedio2 = new Medio_Boleto(0);
        }

        // Test para verificar que el saldo excedente se almacena como pendiente
        [Test]
        public void CargarSaldo_ExcedenteSeAlmacenaComoPendienteTest()
        {
            float montoCarga = 2000;
            bool resultado = tarjetaGratuito.CargarSaldo(montoCarga);

            Assert.That(resultado, Is.True);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaGratuito.obtener_saldo_pendiente(), Is.EqualTo(1000));
        }

       

        [Test]
        public void PermitirViajeDespuesDe5MinutosTest()
         {
            
        tarjetaMedioBoleto.RegistrarUso();

        Thread.Sleep(300000);

        //Intentamos registrar otro uso
        bool puedeUsarse = tarjetaMedioBoleto.PuedeUsarse();
        Assert.That(puedeUsarse, Is.True);
        }

        // Test de saldo: Validar que se puedan realizar pagos con los montos de carga específicos
        [Test]
        public void TestMontosDeCargaEspecificos()
        {
            float[] montos = { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };

            foreach (var monto in montos)
            {
                tarjetaGratuito.CargarSaldo(monto);
                Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            }
        }

        
        // Test de límite de 4 viajes por día para medio boleto
        [Test]
        public void TestLimiteCuatroViajesPorDia()
        {
            for (int i = 0; i < 4; i++)
            {
                tarjetaMedioBoleto.RegistrarUso(); // Registrar 4 viajes
            }

            // Intentar registrar un quinto viaje
            bool puedeRegistrar = tarjetaMedioBoleto.PuedeUsarse();
            Assert.That(puedeRegistrar, Is.False); 
        }

        
    }
}