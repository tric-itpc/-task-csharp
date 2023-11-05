namespace MyAPI.Services
{
    /// <summary>
    /// Для описания сложного события (с передачей данных)
    /// </summary>
    public class ServicePapaz
    {
        public event EventHandler<DescriptionOfEventArgs> NoticeFromService;
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;

        // Метод для уведомления подписчиков
        public void StatusChange(DescriptionOfEventArgs e)
        {
            // Сохраняем ссылку на делегат во временной переменной, для обеспечения безопасности потоков
            EventHandler<DescriptionOfEventArgs> temp = Volatile.Read(ref NoticeFromService);

            // Если есть объекты, зарегистрированные для получения уведомления о событии,
            // уведомляем их, чтобы избежать NullReferenceException
            if (temp != null)
                temp(this, e);
        }
    }

    // Класс для описания данных, которые передаются подписчикам
    public class DescriptionOfEventArgs : EventArgs
    {
        public int ServiceId { get; private set; }
        public string Message { get; private set; }
        public DescriptionOfEventArgs(string message, int serviceId)
        {
            Message = message;
            ServiceId = serviceId;
        }
    }
}
