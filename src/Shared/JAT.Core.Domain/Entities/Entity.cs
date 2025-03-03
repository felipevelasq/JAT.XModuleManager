namespace JAT.Core.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public bool Deleted { get; set; }

    protected Entity(Guid id)
    {
        Id = id;
    }
}