using Microsoft.EntityFrameworkCore;
using MyAPI.Models;

namespace MyAPI.Data
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx) =>
            _ctx = ctx;

        /// <summary>
        /// Добавляет информацию о сервисе в БД
        /// </summary>
        /// <param name="service"></param>
        public void AddService(Service service) =>
            _ctx.Services.Add(service);

        /// <summary>
        /// Метод для API
        /// </summary>
        /// <returns>Этот список нужен, чтобы вернуть только нужные поля из таблицы</returns>
        public List<PartOfService> GetServicesInfo() =>
             _ctx.Services.Select(service => new PartOfService
             {
                 Name = service.Name,
                 Description = service.Description,
                 Status = service.Status,
                 // Для удобства сортирую записи по возрастанию
                 StatusHistory = service.StatusHistory.OrderBy(n => n).ToList(),
                 WorkTime = service.WorkTime,
                 BadWorkTime = service.BadWorkTime,
                 DownTime = service.DownTime
             }).AsNoTracking().ToList();

        /// <summary>
        /// Обновляем в БД информацию о сервисе
        /// </summary>
        /// <param name="service">Содержит информацию о сервисе</param>
        public void UpdateService(Service service)
        {
            service.UpdatedAt = DateTime.Now;
            _ctx.Services.Update(service);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Асинхронно сохраняет данные с подтверждением
        /// </summary>
        public async Task<bool> SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();

            if (await _ctx.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }

    public class PartOfService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<History> StatusHistory { get; set; }
        public int? WorkTime { get; set; }
        public int? BadWorkTime { get; set; }
        public int? DownTime { get; set; }
    }
}
