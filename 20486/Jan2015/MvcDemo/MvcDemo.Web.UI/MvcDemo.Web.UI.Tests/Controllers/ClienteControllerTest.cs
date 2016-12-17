using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcDemo.Model.Entities;
using MvcDemo.Services.ClientManagement;
using MvcDemo.Web.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcDemo.Web.UI.Tests.Controllers
{
    [TestClass]
    public class ClienteControllerTest
    {
        private IEnumerable<Cliente> FindAllFake()
        {
            return new List<Cliente>() 
            {
                new Cliente(){ Id = 10, Nome = "Kemps", 
                    Email = "kemps.vieira@gmail.com", 
                    CreatedDate = DateTime.Now, Idade = 33}
            };
        }

        [TestMethod]
        public void Index_Partial()
        {
            // Arrange
            int exptectedCounter = 1;
            int exptectedId = 10;
            string expectedName = "Kemps";

            var service = new Mock<IClientManagementService>();

            var findAll = service.As<IClientManagementService>();
            findAll.Setup(v => v.FindAll()).Returns(FindAllFake());

            ClienteController controller = new ClienteController(service.Object);

            // Act
            PartialViewResult result = controller.IndexPartial() as PartialViewResult;
            List<Cliente> clientes = result.Model as List<Cliente>;

            // Assert
            Assert.AreEqual(exptectedCounter, clientes.Count);
            Assert.AreEqual(exptectedId, clientes[0].Id);
            Assert.AreEqual(expectedName, clientes[0].Nome);
        }

        [TestMethod]
        public void Create() 
        {
            //Arrange
            var _service = new Mock<IClientManagementService>();

            var createFake = _service.As<IClientManagementService>();
            createFake.Setup(f => f.Add(null)).Verifiable();

            Cliente newCliente = new Cliente(){ 
                        Nome = "Cliente2",
                        Endereco = "Endereco2", 
                        CreatedDate = DateTime.Now, 
                        Idade = 20};

            //Act
            ClienteController controller = new ClienteController(_service.Object);
            var result = controller.Create(newCliente);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
