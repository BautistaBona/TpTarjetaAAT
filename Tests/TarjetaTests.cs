using NUnit.Framework;
using TpSube;
using System;
using System.Threading;
using ManejoDeTiempos;

namespace TarjetaTest
{
    public class TarjetaTest
    {
        private Urbano colectivo;
        private Gratuito_Estudiantes tarjetaGratuito;
        private Medio_Boleto tarjetaMedioBoleto;
        private Gratuito_Estudiantes tarjetaGratuito2;
        private Medio_Boleto tarjetaMedio2;
        private TiempoFalso tiempoFalso;
        private Tarjeta tarjetaNormal;

        [SetUp]
        public void Setup()
        {
            colectivo = new Urbano("Línea 2", 1200);
            tarjetaGratuito = new Gratuito_Estudiantes(35000);
            tarjetaMedioBoleto = new Medio_Boleto(35000);
            tarjetaGratuito2 = new Gratuito_Estudiantes(0);
            tarjetaMedio2 = new Medio_Boleto(0);
            tiempoFalso = new TiempoFalso();
            tarjetaNormal = new Tarjeta(35000);
        }
        


        // Test Iteracion 1
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




        //Iteracion 3

        [Test]
        public void PermitirViajeDespuesDe5Minutos()
        {

            tarjetaMedioBoleto.RegistrarUso(tiempoFalso);

            tiempoFalso.AgregarMinutos(5);

            bool puedeUsarse = tarjetaMedioBoleto.PuedeUsarse(tiempoFalso);
            Assert.That(puedeUsarse, Is.True);
        }

        [Test]
        public void NoPermitirViajeAntesDe5Minutos()
        {

            tarjetaMedioBoleto.RegistrarUso(tiempoFalso);
            tiempoFalso.AgregarMinutos(3);
            Assert.That(tarjetaMedioBoleto.PuedeUsarse(tiempoFalso), Is.False);
        }



        // Test de límite de 4 viajes por día para medio boleto
        [Test]
        public void LimiteCuatroViajesPorDia()
        {
            for (int i = 0; i < 4; i++)
            {
                tarjetaMedioBoleto.RegistrarUso(tiempoFalso); // Registrar 4 viajes
                tiempoFalso.AgregarMinutos(5);
            }

        
            Assert.That(tarjetaMedioBoleto.PuedeUsarse(tiempoFalso), Is.False);
        }


        // Test para verificar que el saldo excedente se almacena como pendiente
        [Test]
        public void ExcedenteSeAlmacenaComoPendiente()
        {

            Assert.That(tarjetaGratuito.CargarSaldo(2000), Is.True);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            Assert.That(tarjetaGratuito.obtener_saldo_pendiente(), Is.EqualTo(1000));
        }


        [Test]
        public void AcreditarSaldoPendiente()
        {
            Assert.That(tarjetaGratuito.CargarSaldo(2000), Is.True);
            Assert.That(tarjetaGratuito.saldo, Is.EqualTo(36000));
            colectivo.PagarPasaje(tarjetaNormal, tiempoFalso);
            Assert.That(tarjetaGratuito.obtener_saldo_pendiente(), Is.EqualTo(1000));
        }


    }

}
