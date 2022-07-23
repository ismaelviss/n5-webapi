using Moq;
using n5.webApi.Repositories.Interfaces;
using n5.webApi.Models;
using n5.webApi.Services.Impl;
using Microsoft.Extensions.Logging;
using n5.webApi.Services.Interface;
using n5.webApi.Models.dto;
using AutoMapper;

namespace n5.webApi.Test
{

    public class PermissionServiceTest
    {
        public readonly Mock<IPermissionRepository> _permissionRepository = new();
        public readonly Mock<IPermissionTypeRepository> _permissionTypeRepository = new();
        public readonly Mock<ILogger<PermissionService>> _logger = new();
        public readonly Mock<IEsClientProvider> _esClientProvider = new();
        public readonly Mock<IProducerKafka> _producerKafka = new();
        public readonly Mock<IMapper> _mapper = new();


        [Fact]
        public void Get_Success()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            _permissionRepository.Setup(x => x.All()).Returns(new List<Permission>() { permission });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>()));

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object,_esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            var result = permissionService.Get();

            var permissionTest = Assert.Single(result);
            Assert.NotNull(permissionTest);
            Assert.Equal(1, permissionTest.Id);
            Assert.Equal("Salvaierra", permissionTest.EmployeeLastName);
            Assert.Equal("Elvis", permissionTest.EmployeeName);
            Assert.Equal(DateTime.Parse("2022-07-19"), permissionTest.PermissionDate);
            Assert.Equal(1, permissionTest.PermissionTypeId);
            _permissionRepository.Verify(x => x.All(), Times.Once);
            _producerKafka.Verify(x => x.Publish(It.Is<EventDto>(y => y.NameOperation.Equals(EventDto.GET))), Times.Once);
        }

        [Fact]
        public void Get_NotFound()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            _permissionRepository.Setup(x => x.All()).Returns(new List<Permission>() { });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>()));

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            var result = permissionService.Get();

            Assert.Empty(result);
            _permissionRepository.Verify(x => x.All(), Times.Once);
            _producerKafka.Verify(x => x.Publish(It.Is<EventDto>(y => y.NameOperation.Equals(EventDto.GET))), Times.Once);
        }

        [Fact]
        public void Save_Success()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            _permissionRepository.Setup(x => x.Add(permission)).Verifiable();
            _permissionRepository.Setup(x => x.UnitOfWork.Commit()).Verifiable();
            _permissionTypeRepository.Setup(x => x.FindById(1)).Returns(new PermissionType() { Id = 1, Description = "Seguridad"});
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>())).Verifiable();
            _esClientProvider.Setup(x => x.SaveEntity(It.IsAny<PermissionDto>())).Verifiable();

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            var result = permissionService.Save(permission);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Salvaierra", result.EmployeeLastName);
            Assert.Equal("Elvis", result.EmployeeName);
            Assert.NotNull(result.PermissionDate);
            Assert.Equal(1, result.PermissionTypeId);
            _permissionRepository.Verify(x => x.Add(permission), Times.Once);
            _permissionTypeRepository.Verify(x => x.FindById(1), Times.Once);
            _permissionRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _producerKafka.Verify(x => x.Publish(It.Is<EventDto>(y => y.NameOperation.Equals(EventDto.REQUEST))), Times.Once);
            _esClientProvider.Verify(x => x.SaveEntity(It.IsAny<PermissionDto>()), Times.Once);
        }

        [Fact]
        public void Save_Error()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 2 };
            _permissionRepository.Setup(x => x.Add(permission)).Verifiable();
            _permissionRepository.Setup(x => x.UnitOfWork.Commit()).Verifiable();
            _permissionTypeRepository.Setup(x => x.FindById(1)).Returns(new PermissionType() { Id = 1, Description = "Seguridad" });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>())).Verifiable();
            _esClientProvider.Setup(x => x.SaveEntity(It.IsAny<PermissionDto>())).Verifiable();

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            Assert.ThrowsAny<Exception>(() => permissionService.Save(permission));

            _permissionTypeRepository.Verify(x => x.FindById(2), Times.Once);
        }

        [Fact]
        public void Update_Success()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            var permission2 = new Permission() { Id = 1, EmployeeLastName = "Salvaierra2", EmployeeName = "Elvis2", PermissionDate = DateTime.Parse("2022-07-20"), PermissionTypeId = 1 };
            _permissionRepository.Setup(x => x.FindById(1)).Returns(permission);
            _permissionRepository.Setup(x => x.UnitOfWork.Commit()).Verifiable();
            _permissionTypeRepository.Setup(x => x.FindById(1)).Returns(new PermissionType() { Id = 1, Description = "Seguridad" });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>())).Verifiable();
            

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            var result = permissionService.Update(1, permission2);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Salvaierra2", result.EmployeeLastName);
            Assert.Equal("Elvis2", result.EmployeeName);
            Assert.NotNull(result.PermissionDate);
            Assert.Equal(1, result.PermissionTypeId);
            _permissionRepository.Verify(x => x.FindById(1), Times.Once);
            _permissionRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _permissionTypeRepository.Verify(x => x.FindById(1), Times.Once);
            _producerKafka.Verify(x => x.Publish(It.Is<EventDto>(y => y.NameOperation.Equals(EventDto.MODIFY))), Times.Once);
        }

        [Fact]
        public void Update_Error_permissionType()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            var permission2 = new Permission() { Id = 1, EmployeeLastName = "Salvaierra2", EmployeeName = "Elvis2", PermissionDate = DateTime.Parse("2022-07-20"), PermissionTypeId = 2 };
            _permissionRepository.Setup(x => x.FindById(1)).Returns(permission);
            _permissionRepository.Setup(x => x.UnitOfWork.Commit()).Verifiable();
            _permissionTypeRepository.Setup(x => x.FindById(1)).Returns(new PermissionType() { Id = 1, Description = "Seguridad" });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>())).Verifiable();

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            Assert.ThrowsAny<Exception>(() => permissionService.Update(1, permission2));

            _permissionTypeRepository.Verify(x => x.FindById(2), Times.Once);
        }

        [Fact]
        public void Update_Error_permission()
        {
            var permission = new Permission() { Id = 1, EmployeeLastName = "Salvaierra", EmployeeName = "Elvis", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            var permission2 = new Permission() { Id = 1, EmployeeLastName = "Salvaierra2", EmployeeName = "Elvis2", PermissionDate = DateTime.Parse("2022-07-20"), PermissionTypeId = 1 };
            _permissionRepository.Setup(x => x.FindById(1)).Returns(permission);
            _permissionRepository.Setup(x => x.UnitOfWork.Commit()).Verifiable();
            _permissionTypeRepository.Setup(x => x.FindById(1)).Returns(new PermissionType() { Id = 1, Description = "Seguridad" });
            _producerKafka.Setup(x => x.Publish(It.IsAny<EventDto>())).Verifiable();

            PermissionService permissionService = new(_permissionRepository.Object, _permissionTypeRepository.Object, _logger.Object, _esClientProvider.Object, _producerKafka.Object, _mapper.Object);

            Assert.ThrowsAny<Exception>(() => permissionService.Update(2, permission2));

            _permissionRepository.Verify(x => x.FindById(2), Times.Once);
        }
    }
}