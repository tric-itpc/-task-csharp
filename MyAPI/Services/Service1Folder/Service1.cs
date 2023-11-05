namespace MyAPI.Services.Service1Folder
{
    public class Service1 : ServicePapaz
    {
        /// <summary>
        /// Имитирует работу сервиса 1
        /// </summary>
        public async Task Work1()
        {
            Console.WriteLine("Service1 начал работу");
            StatusChange(new DescriptionOfEventArgs("Работает", 1));
            await Task.Delay(5000);

            StatusChange(new DescriptionOfEventArgs("Нестабильно работает", 1));
            await Task.Delay(2000);

            StatusChange(new DescriptionOfEventArgs("Не работает", 1));
            Console.WriteLine("Service1 отработал");
        }
    }
}