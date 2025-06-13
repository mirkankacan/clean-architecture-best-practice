using CleanArchitecture.Domain.Abstracts;

namespace CleanArchitecture.Domain.Entities
{
    public sealed class Car : BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public int EnginePower { get; set; }
    }
}