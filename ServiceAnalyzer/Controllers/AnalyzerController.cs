using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAnalyzer.DTO;
using ServiceAnalyzer.Model;
using ServiceAnalyzer.Services;

namespace ServiceAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzerController : ControllerBase
    {
        private IRepository repo;

        public AnalyzerController(IRepository repository)
        {
            repo = repository;
        }

        // Выводит список сервисов с актуальным состоянием
        // GET: AnalyzerController/Actual
        [HttpGet("/Actual")]
        public async Task<List<ActualServiceStatusDTO>> GetActual() => await repo.GetActual();

        // По имени сервиса выдает историю изменения состояния и все данные по каждому состоянию
        // GET: AnalyzerController/History
        [HttpGet("/History")]
        public async Task<List<ServiceInfoDTO>> GetHistory(string name) => await repo.GetHistory(name);

        // По указанному интервалу выдается информация о том сколько не работал сервис и считать SLA в процентах до 3-й запятой
        // GET: AnalyzerController/SLA
        [HttpGet("/SLA")]
        public async Task<List<ServiceSLA>> GetSLA(DateTime startDate, DateTime endDate) => await repo.GetSLA(startDate, endDate);


        // Получает и сохраняет данные: имя, состояние, описание
        // POST: AnalyzerController/AddInfo
        [HttpPost("/AddInfo")]
        public async Task<ServiceInfo> AddInfo(ServiceInfoDTO info) => await repo.AddData(info);


    }
}
