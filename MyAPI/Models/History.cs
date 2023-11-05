using Microsoft.EntityFrameworkCore;

namespace MyAPI.Models
{
    [Owned] // Принадлежит таблице Service
    public class History
    {
        public History(string record) => Record = record;

        public string Record { get; set; } // Запись формата Статус: время изменения
    }
}
