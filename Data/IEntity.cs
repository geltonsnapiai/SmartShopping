namespace SmartShopping.Data
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public interface INamedEntity : IEntity
    {
        string SimplifiedName { get; set; }
        string DisplayName { get; set; }
    }
}
