namespace CleanArchitecture.Domain.Abstracts
{
    public abstract class BaseEntity
    {
        public string Id { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}