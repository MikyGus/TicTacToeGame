using TicTacToe.Abstract;

namespace TicTacToe.Models;
internal class CellEntity
{
    private readonly List<IComponent> _components = new();
    public required Vector2 Position { get; set; }

    public void AddComponent(IComponent component)
    {
        component.Parent = this;
        _components.Add(component);
    }

    public void RemoveComponent(IComponent component)
    {
        if (component.Parent != this)
            throw new ArgumentException("Trying to remove a component belonging to another entity");
        _components.Remove(component);
    }

    public TComponent GetComponent<TComponent>() where TComponent : class, IComponent 
        => _components.OfType<TComponent>().FirstOrDefault();

    public IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : class, IComponent
    {
        foreach (var component in _components.OfType<TComponent>())
            yield return component;
    }
}
