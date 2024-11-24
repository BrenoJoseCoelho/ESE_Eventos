namespace EventosApi.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public EnterpriseDto Enterprise { get; set; }
    }
}
