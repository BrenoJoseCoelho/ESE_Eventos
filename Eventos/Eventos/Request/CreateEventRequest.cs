namespace EventosApi.Request
{
    public class CreateEventRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Guid EnterpriseId { get; set; }
    }
}
