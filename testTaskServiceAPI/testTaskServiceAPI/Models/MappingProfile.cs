using AutoMapper;
using testTaskServiceAPI.Models.Domain;
using testTaskServiceAPI.Models.View;

namespace testTaskServiceAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<ServiceDomain, ServiceView>();
            this.CreateMap<ServiceView, ServiceDomain>();
            this.CreateMap<ServiceHistoryDomain, ServiceHistoryView>();
            this.CreateMap<ServiceHistoryView, ServiceHistoryDomain>();
        }
    }
}
