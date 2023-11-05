namespace MyAPI.Services.Service2Folder
{
    public class Service2 : ServicePapaz
    {
        /// <summary>
        /// Имитирует работу сервиса 2
        /// </summary>
        public async Task Work2()
        {
            Console.WriteLine("Service2 начал работу");
            StatusChange(new DescriptionOfEventArgs("Работает", 2));
            await Task.Delay(4000);

            StatusChange(new DescriptionOfEventArgs("Нестабильно работает", 2));
            await Task.Delay(1000);

            StatusChange(new DescriptionOfEventArgs("Не работает", 2));
            Console.WriteLine("Service2 отработал");
        }
    }
}

