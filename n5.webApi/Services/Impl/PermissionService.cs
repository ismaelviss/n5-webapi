using AutoMapper;
using Microsoft.Extensions.Logging;
using n5.webApi.Models;
using n5.webApi.Models.dto;
using n5.webApi.Repositories.Interfaces;
using n5.webApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n5.webApi.Services.Impl
{
    public class PermissionService : IPermissionService
    {
        IPermissionRepository _permissionRepository;
        IPermissionTypeRepository _permissionTypeRepository;
        IMapper _mapper;

        IEsClientProvider _esClientProvider;

        IProducerKafka _producerKafka;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository, ILogger<PermissionService> logger, IEsClientProvider esClientProvider, IProducerKafka producerKafka, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _logger = logger;
            _esClientProvider = esClientProvider;
            _producerKafka = producerKafka;
            _mapper = mapper;
        }

        public IEnumerable<Permission> Get()
        {
            var result = _permissionRepository.All();
            _logger.LogDebug("se consulto de la base de datos");

            _producerKafka.Publish(new EventDto(){ Id = Guid.NewGuid(), NameOperation = EventDto.GET});
            _logger.LogDebug("se publico el evento en kafka");

            return result;
        }

        public Permission GetById(int id)
        {
            var result = _permissionRepository.FindById(id);
            _logger.LogDebug("se consulto de la base de datos");

            _producerKafka.Publish(new EventDto(){ Id = Guid.NewGuid(), NameOperation = EventDto.GET});
            _logger.LogDebug("se publico el evento en kafka");

            return result;
        }

        public Permission Save(Permission permission)
        {
            
            var permissionType = _permissionTypeRepository.FindById(permission.PermissionTypeId);
            if (permissionType == null || permissionType.Description == null)
                throw new ExceptionService("No existe el ID del tipo de permiso enviado");
            
            permission.PermissionDate = DateTime.Now;
            _permissionRepository.Add(permission);
            _logger.LogDebug("se inserta con exito el permiso en DB");
            
            _permissionRepository.UnitOfWork.Commit();
            var mappedPermission = _mapper.Map<PermissionDto>(permission);
            _esClientProvider.SaveEntity(mappedPermission);
            _logger.LogDebug("se inserta con exito el permiso en ES");

            _producerKafka.Publish(new EventDto(){ Id = Guid.NewGuid(), NameOperation = EventDto.REQUEST});
            _logger.LogDebug("se publico el evento en kafka");

            _logger.LogDebug("se creo con exito el permiso");

            return permission;

        }

        public Permission Update(int id, Permission permission)
        {
            var permissionExists = _permissionRepository.FindById(id);
            if (permissionExists != null)
            {
                if (_permissionTypeRepository.FindById(permission.PermissionTypeId) == null)
                    throw new ExceptionService("No existe el ID del tipo de permiso enviado");

                permissionExists.EmployeeLastName = permission.EmployeeLastName;
                permissionExists.EmployeeName = permission.EmployeeName;
                permissionExists.PermissionDate = DateTime.Now;
                permissionExists.PermissionTypeId = permission.PermissionTypeId;

                _permissionRepository.UnitOfWork.Commit();
                _logger.LogDebug("se actulizo en la base de datos");

                _producerKafka.Publish(new EventDto(){ Id = Guid.NewGuid(), NameOperation = EventDto.MODIFY});
                _logger.LogDebug("se publico el evento en kafka");
            }
            else
            {
                throw new ExceptionService("No existe el ID del permiso enviado");
            }

            return permissionExists;

        }

        public IEnumerable<PermissionType> GetPermissionTypes()
        {
            var result = _permissionTypeRepository.All();
            _logger.LogDebug("se consulto de la base de datos");
            return result;
        }
    }
}