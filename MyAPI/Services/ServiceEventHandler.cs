
using MyAPI.Services.Service1Folder;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Services.Service2Folder;
using MyAPI.Services.Service3Folder;

namespace MyAPI.Services
{
    /// <summary>
    /// Для сбора информации с сервисов и её изменения
    /// </summary>
    public class ServiceEventHandler : IServiceEventHandler
    {
        private IRepository repository;
        private static Service service1_db_ekz, service2_db_ekz, service3_db_ekz;
        private static Service1 service1;
        private static Service2 service2;
        private static Service3 service3;
        private static int updateCount = 0;

        public ServiceEventHandler(IRepository _repository)
        {
            repository = _repository;
        }

        /// <summary>
        /// Метод обработчик
        /// </summary>
        /// <param name="sender">Сервис, который подписан на событие</param>
        /// <param name="e">Экземпляр класса, который содержит id сервиса и сообщение от него</param>
        private void ServiceHandler(object sender, DescriptionOfEventArgs e)
        {
            if (e.ServiceId == 1)
                ChangeTime(ref service1_db_ekz, e);

            else if (e.ServiceId == 2)
                ChangeTime(ref service2_db_ekz, e);

            else if (e.ServiceId == 3)
                ChangeTime(ref service3_db_ekz, e);
        }

        /// <summary>
        /// Меняет статус и таймеры сервиса
        /// </summary>
        /// <param name="service_db">Экземпляр класса с информацией о сервисе</param>
        private void ChangeTime(ref Service service_db, DescriptionOfEventArgs e)
        {
            updateCount++;

            DateTime lastTime = DateTime.Now;
            var currentTime = DateTime.Now;
            string record = "";
            string previousStatus = "";

            // Если это НЕ первый запуск (если первый, я просто пропускаю, ведь еще не знаю сколько он будет работать/не работать/стоять
            if (service_db.StatusHistory.Count != 0)
            {
                // Последняя запись из истории статусов
                record = service_db.StatusHistory.Last().Record;

                // Время последней записи
                lastTime = DateTime.Parse(record.Substring(record.Length - 8));

                // Наименование последнего статуса
                previousStatus = record[0..^21];

                // Разница между текущим временем и временем последнего обновления статуса
                var timeDifference = currentTime.Subtract(lastTime);

                // Изменяем таймеры в зависимости от предыдущего статуса 
                if (previousStatus == "Не работает")
                    service_db.DownTime += (TimeSpan.FromSeconds(Math.Ceiling(timeDifference.TotalSeconds)).Seconds - 1);

                else if (previousStatus == "Работает")
                    service_db.WorkTime += (TimeSpan.FromSeconds(Math.Ceiling(timeDifference.TotalSeconds)).Seconds - 1);

                else if (previousStatus == "Нестабильно работает")
                    service_db.BadWorkTime += (TimeSpan.FromSeconds(Math.Ceiling(timeDifference.TotalSeconds)).Seconds - 1);
            }
            // Обновляем текущий статус
            service_db.Status = e.Message;

            var history = new History(e.Message + ": " + DateTime.Now.ToString());
            // Добавляем запись в историю статусов
            service_db.StatusHistory.Add(history);

            // Обновляем БД каждые 9 изменений статуса
            if (updateCount % 9 == 0 && updateCount != 0)
            {
                UpdateDB();
                Console.WriteLine("База данных обновлена");
            }
        }

        /// <summary>
        /// Обновляет данные о сервисах
        /// </summary>
        private void UpdateDB()
        {
            repository.UpdateService(service1_db_ekz);
            repository.UpdateService(service2_db_ekz);
            repository.UpdateService(service3_db_ekz);
        }

        /// <summary>
        /// Инициализирует таблицу для сервисов
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateServices()
        {

            var _service1_db_ekz = new Service
            {
                Id = 1,
                Name = "Service1",
                Description = "Service1Description",
                WorkTime = 0,
                DownTime = 0,
                BadWorkTime = 0,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = "DontWork",
                StatusHistory = new List<History>()

            };
            service1_db_ekz = _service1_db_ekz;

            var _service2_db_ekz = new Service
            {
                Id = 2,
                Name = "Service2",
                Description = "Service2Description",
                WorkTime = 0,
                DownTime = 0,
                BadWorkTime = 0,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = "DontWork",
                StatusHistory = new List<History>()
            };
            service2_db_ekz = _service2_db_ekz;

            var _service3_db_ekz = new Service
            {
                Id = 3,
                Name = "Service3",
                Description = "Service3Description",
                WorkTime = 0,
                DownTime = 0,
                BadWorkTime = 0,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = "DontWork",
                StatusHistory = new List<History>()
            };
            service3_db_ekz = _service3_db_ekz;

            repository.AddService(_service1_db_ekz);
            repository.AddService(_service2_db_ekz);
            repository.AddService(_service3_db_ekz);

            if (await repository.SaveChangesAsync())
            {
                Console.WriteLine("Сервисы созданы успешно");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Запускает работу сервисов
        /// </summary>
        public async void StartServices()
        {
            if (await CreateServices())
                Console.WriteLine("Сервисы созданы успешно");

            service1 = new Service1();
            service1.NoticeFromService += ServiceHandler;

            service2 = new Service2();
            service2.NoticeFromService += ServiceHandler;

            service3 = new Service3();
            service3.NoticeFromService += ServiceHandler;

            while (true)
            {
                await service1.Work1();
                await service2.Work2();
                await service3.Work3();
            }
        }
    }
}