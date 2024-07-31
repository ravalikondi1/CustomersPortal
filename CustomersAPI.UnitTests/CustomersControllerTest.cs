using AutoMapper;
using CustomersAPI.Controllers;
using CustomersAPI.DataAccess.Entities;
using CustomersAPI.Models;
using CustomersAPI.Services;
using CustomersAPI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CustomersAPI.UnitTests
{
    public class CustomersControllerTest
    {
        private readonly CustomersController _controller;
        private readonly Mock<ICustomerService> _service;
        private readonly Mock<ILogger<CustomersController>> _logger;
        private readonly Mock<IValidator> _validator;
        private readonly Mock<IMapper> _mapper;

        public CustomersControllerTest()
        {
            _service = new Mock<ICustomerService>();
            _logger = new Mock<ILogger<CustomersController>>();
            _validator = new Mock<IValidator>();
            _mapper = new Mock<IMapper>();
            _controller = new CustomersController(_service.Object, _logger.Object, _validator.Object, _mapper.Object);
        }

        [Fact]
        public void GetAllCustomers()
        {
            //Act
            var result = _controller.GetAllCustomers();
            //Assert
            Assert.IsType<OkObjectResult>(result);

            var res = result as OkObjectResult;

            Assert.NotNull(res);

            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public void AddBookTest()
        {
            //Arrange
            var customer = new AddOrUpdateCustomer()
            {
                FirstName = "UnitTest",
                LastName = "User1",
                DateOfBirth = new DateTime(1990, 01, 01),
                EmailAddress = "unittestuser1@gmail.com",
                PhoneNumber = "1234567890",
                IsActive = true,
            };

            //Act
            _validator.Setup(x => x.Validate(customer)).Returns((true, string.Empty));
            //_service.Verify(x=>x.AddCustomer(customer), Times.Once());
            _service.Setup(x=>x.AddCustomer(customer)).Returns(Task.FromResult(new Customer()
            { 
                FirstName = "UnitTest",
                LastName = "User1",
                DateOfBirth = new DateTime(1990, 01, 01),
                EmailAddress = "unittestuser1@gmail.com",
                PhoneNumber = "1234567890",
                IsActive = true,
            }));
            var response = _controller.Post(customer);

            //Assert
            Assert.IsType<OkObjectResult>(response);

            //value of the result
            var item = response as OkObjectResult;
            Assert.NotNull(item);
            Assert.NotNull(item.Value);
        }
    }
}