using ServiceAnalyzer.DTO;
using ServiceAnalyzer.Model;

namespace ServiceAnalyzer.Services
{
    public interface IRepository
    {
        Task<ServiceInfo> AddData(ServiceInfoDTO serviceInfo);
        Task<List<ActualServiceStatusDTO>> GetActual();
        Task<List<ServiceInfoDTO>> GetHistory(string name);
        Task<List<ServiceSLA>> GetSLA(DateTime startDate, DateTime endDate);
    }
}
