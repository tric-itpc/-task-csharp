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
            if (((int)serviceView.State) >= 3 || ((int)serviceView.State) < 0)
                return BadRequest("Не верно указано состояние сервиса");

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
            var servicesDomain = _serviceRepository.GetAll();
            var services = new List<ServiceView>();
            foreach (var service in servicesDomain)
            {
                var serviceView = _mapper.Map<ServiceDomain, ServiceView>(service);
                services.Add(serviceView);
            }
            return Ok(services);
        }

        [HttpGet("GetServiceHistory")]
        public IActionResult GetAllServicesHistory(string name)
        {
            var servicesHistoryDomain = _serviceHistoryRepository.GetHistoryByName(name);
            var serviceHistory = new List<ServiceHistoryView>();
            foreach (var serviceHistoryDomain in servicesHistoryDomain)
            {
                var serviceHistoryView = _mapper.Map<ServiceHistoryDomain, ServiceHistoryView>(serviceHistoryDomain);
                serviceHistory.Add(serviceHistoryView);
            }
            return Ok(serviceHistory);
        }

        [HttpGet("GetSLA")]
        public IActionResult GetSLA(DateTime startDate, DateTime endDate, string name)
        {
            var span = endDate - startDate;
            var spanNotWorked = CalculateSpanNotWorked(startDate, endDate, name);
            var sla = ((span.TotalMinutes - spanNotWorked.TotalMinutes) / span.TotalMinutes) * 100;
            sla = Math.Round(sla, 3);
            var result = new
            {
                NotWorkedInMinutes = spanNotWorked.TotalMinutes,
                SLA = sla
            };
            return Ok(result);
        }

        private TimeSpan CalculateSpanNotWorked(DateTime startDate, DateTime endDate, string name)
        {
            var currentDate = startDate;
            var histories = _serviceHistoryRepository.GetHistoryByDate(startDate, endDate, name);
            var timeSpan = new TimeSpan();
            foreach (var history in histories)
            {
                if(history.StateOld == State.NotWorking)
                    timeSpan += history.DateTime - currentDate;
                currentDate = history.DateTime;
            }
            if (histories.Count() != 0 && histories.Last().StateNew == State.NotWorking)
                timeSpan += histories.Last().DateTime - currentDate;
            return timeSpan;
        }
    }
}
