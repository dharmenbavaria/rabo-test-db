namespace RaboBankTest.Domain.Data
{
    public class CustomDataModel
    {
        public Guid RecordId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string DataValue { get; set; }

        public bool NotificationFlag { get; set; }
    }
}
