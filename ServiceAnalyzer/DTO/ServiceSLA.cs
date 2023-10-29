namespace ServiceAnalyzer.DTO
{
    public class ServiceSLA
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ServiceSLA() { }

        public ServiceSLA(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
