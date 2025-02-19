namespace JAT.Modules.Domain;

public class Module
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string ModuleType { get; private set; }

    public Module(string name, string? description, string moduleType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        ModuleType = moduleType;
    }
}
