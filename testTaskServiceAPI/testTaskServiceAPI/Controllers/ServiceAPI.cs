using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testTaskServiceAPI.Models;
using testTaskServiceAPI.Models.Domain;
using testTaskServiceAPI.Models.View;
using testTaskServiceAPI.Repository;

namespace testTaskServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceAPI : ControllerBase
    {
        private readonly ILogger<ServiceAPI> _logger;
        private readonly IMapper _mapper;
        private ServiceRepository _serviceRepository;
        private ServiceHistoryRepository _serviceHistoryRepository;

        public ServiceAPI(ILogger<ServiceAPI> logger, IMapper mapper)
        {
            var db = new Context();
            _logger = logger;
            _mapper = mapper;
            _serviceRepository = new ServiceRepository(db);
            _serviceHistoryRepository = new ServiceHistoryRepository(db);
        }

        [HttpPost("AddServiceState")]
        public IActionResult AddServiceState(ServiceView serviceView)
        {
            var service = _mapper.Map<ServiceView, ServiceDomain>(serviceView);
            if (!_serviceRepository.Contains(service))
            {
                _serviceRepository.Add(service);
                _serviceHistoryRepository.Add(new ServiceHistoryDomain(service));
            }
            else
            {
                var oldService = _serviceRepository.GetByName(service.Name);
                _serviceHistoryRepository.Add(new ServiceHistoryDomain(oldService, service));
                oldService.Update(service);
                _serviceRepository.Update(oldService);
            }

            return Ok();
        }

        [HttpGet("GetAllServices")]
        public IActionResult GetAllServices()
        {
            var services = _serviceRepository.GetAll();
            return Ok(services);
        }

        [HttpGet("GetAllServicesHistory")]
        public IActionResult GetAllServicesHistory()
        {
            var services = _serviceHistoryRepository.GetAll();
            return Ok(services);
        }
    }
}
