public abstract class Despawner : AutoMonoBehaviour
{
    protected virtual void Update()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }

    public virtual void DespawnObject() { /* For override */ }

    protected abstract bool CanDespawn();
}
