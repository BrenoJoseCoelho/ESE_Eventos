namespace EventosApi.Request
{
    public class UpdateEventRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Guid EnterpriseId { get; set; }
    }
}
