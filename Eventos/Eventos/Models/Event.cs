namespace EventosApi.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public Guid EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
    }
}
