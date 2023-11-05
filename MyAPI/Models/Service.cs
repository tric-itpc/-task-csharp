namespace MyAPI.Models
{
    public class Service
    {
        public int Id { get; set; } // Ключ
        public string Name { get; set; } // Название сервиса
        public string Description { get; set; } // Описание сервиса
        public string Status { get; set; } // Текущий статус: работает/ нестабильно работает/ не работает
        public List<History> StatusHistory { get; set; } // История обновления статуса, со временем
        public int? WorkTime { get; set; } // Время работы в секундах
        public int? BadWorkTime { get; set; } // Время нестабильной работы в секундах
        public int? DownTime { get; set; } // Время простоя в секундах
        public DateTime CreatedAt { get; set; }  // Дата создания
        public DateTime? UpdatedAt { get; set; } // Дата обновления
    }
}
