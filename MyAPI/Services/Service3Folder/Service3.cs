namespace MyAPI.Services.Service3Folder
{
    public class Service3 : ServicePapaz
    {
        /// <summary>
        /// Имитирует работу сервиса 3
        /// </summary>
        public async Task Work3()
        {
            Console.WriteLine("Service3 начал работу");
            StatusChange(new DescriptionOfEventArgs("Работает", 3));
            await Task.Delay(3000);

            StatusChange(new DescriptionOfEventArgs("Нестабильно работает", 3));
            await Task.Delay(2000);

            StatusChange(new DescriptionOfEventArgs("Не работает", 3));
            Console.WriteLine("Service3 отработал");
        }
    }
}
