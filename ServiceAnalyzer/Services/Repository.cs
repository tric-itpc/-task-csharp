using Microsoft.EntityFrameworkCore;
using ServiceAnalyzer.DTO;
using ServiceAnalyzer.Enums;
using ServiceAnalyzer.Model;

namespace ServiceAnalyzer.Services
{
    public class Repository : IRepository
    {
        private ApplicationContext db;
        public Repository(ApplicationContext context)
        {
            db = context;
        }

        // Получает и сохраняет данные: имя, состояние, описание
        public async Task<ServiceInfo> AddData(ServiceInfoDTO serviceInfo)
        {
            db.Infos.Add(new ServiceInfo(serviceInfo));
            await db.SaveChangesAsync();

            return db.Infos.Last();
        }

        // Выводит список сервисов с актуальным состоянием
        public async Task<List<ActualServiceStatusDTO>> GetActual()
        {
            List<ActualServiceStatusDTO> list = new List<ActualServiceStatusDTO>();

            var listInfos = await db.Infos.ToListAsync();

            var distinctNames = listInfos.DistinctBy(i => i.Name).ToList();

            foreach (var item in distinctNames)
                list.Add(listInfos.Where(i => i.Name == item.Name)
                                    .MaxBy(m => m.DateTime)
                                    !.ToActualServiceStatusDTO());

            return list;
        }

        // По имени сервиса выдает историю изменения состояния и все данные по каждому состоянию
        public async Task<List<ServiceInfoDTO>> GetHistory(string name)
        {
            List<ServiceInfoDTO> list = new List<ServiceInfoDTO>();

            list = await db.Infos.Where(i => i.Name == name)
                                    .OrderByDescending(i => i.DateTime)
                                    .Select(s => s.ToServiceInfoDTO())
                                    .ToListAsync();

            return list;
        }

        // По указанному интервалу выдается информация о том сколько не работал сервис и считать SLA в процентах до 3-й запятой (время в остоянии "NotStability" принимается равным 50% доступности)
        public async Task<List<ServiceSLA>> GetSLA(DateTime startDate, DateTime endDate)
        {
            List<ServiceSLA> listSLA = new List<ServiceSLA>();

            var allData = await db.Infos.ToListAsync();

            // данные в текущем диапазоне
            var list = allData.Where(i => i.DateTime >= startDate && i.DateTime <= endDate)
                                        .OrderByDescending(w => w.DateTime)
                                        .Select(o => o.ToServiceInfoDTO())
                                        .ToList();

            // список названий сервисов
            var distinctServicesLists = list.DistinctBy(l => l.Name)
                                                .Select(d => d.Name)
                                                .ToList();

            foreach (var name in distinctServicesLists)
            {
                // данные по конкретному сервису
                var statusList = list.Where(l => l.Name == name)
                                        .ToArray();

                //для подсчёта
                TimeSpan workTime = TimeSpan.Zero, notWorkTime = TimeSpan.Zero, notStabilityTime = TimeSpan.Zero;

                // состояние сервиса до момента отсчёта
                StatusService preStatus;
                var lastInfo = allData.Where(i => i.DateTime < startDate && i.Name == name).MaxBy(w => w.DateTime);

                preStatus = lastInfo is null ? StatusService.NotActive : lastInfo.Status;

                // т.к. счёт идёт от максимального времени, выбираем максимальное время, которое будет перебиваться с каждой итерацией статуса
                DateTime maxDate = endDate;

                foreach (var item in statusList)
                {
                    switch (item.Status)
                    {
                        case StatusService.Active:
                            workTime += maxDate - item.DateTime;
                            break;

                        case StatusService.NotStability:
                            notStabilityTime += maxDate - item.DateTime;
                            break;

                        case StatusService.NotActive:
                            notWorkTime += maxDate - item.DateTime;
                            break;

                        default: break;
                    }

                    maxDate = item.DateTime;
                }

                switch (preStatus)
                {
                    case StatusService.Active:
                        workTime += maxDate - startDate;
                        break;

                    case StatusService.NotStability:
                        notStabilityTime += maxDate - startDate;
                        break;

                    case StatusService.NotActive:
                        notWorkTime += maxDate - startDate;
                        break;

                    default: break;
                }

                var SLA = (workTime + (notStabilityTime / 2)) / (endDate - startDate) * 100;
                SLA = Math.Round(SLA, 3);

                listSLA.Add(new ServiceSLA(name, $"{SLA}%"));
            }

            return listSLA;
        }
    }
}
