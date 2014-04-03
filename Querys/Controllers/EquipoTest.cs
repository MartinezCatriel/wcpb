using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prediccion;

namespace Game.Tests.Controllers
{
    [TestClass]
    public class EquipoTest
    {
        [TestMethod]
        public void AlCrearUnEquipoElNombreDebeSerValido()
        {
            var unEquipo = Equipo.Create(1, "Argentina");

            Assert.AreEqual("Argentina", unEquipo.Nombre);
        }

        [TestMethod]
        public void AlCrearUnEquipoElIdDebeSerValido()
        {
            var unEquipo = Equipo.Create(1, "Argentina");

            Assert.AreEqual(1, unEquipo.Id);
        }

        [TestMethod]
        public void AlCrearUnEquipoNoDebeEstarVacio()
        {
            var unEquipo = Equipo.Create(1, "Argentina");

            Assert.IsFalse(string.IsNullOrWhiteSpace(unEquipo.Nombre));
        }


    }
}
