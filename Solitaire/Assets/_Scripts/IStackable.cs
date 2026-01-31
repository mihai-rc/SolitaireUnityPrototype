namespace Solitaire.DataStructures
{
    public interface IStackable<TStackable>
    {
        TStackable Previous { get; }

        TStackable Next { get; }

        bool TryAdd(TStackable stackable);

        void DetachSubstack();
    }
}