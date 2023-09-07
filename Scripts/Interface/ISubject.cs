public interface ISubject
{
    public abstract void Attach(IObserver observer);
    public abstract void Detach(IObserver observer);
    public abstract void Notify();
}