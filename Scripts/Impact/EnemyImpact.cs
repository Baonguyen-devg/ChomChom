using UnityEngine;

public abstract class EnemyImpact : Impact
{
    private const string NAME_LAYER_PLAYER = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(NAME_LAYER_PLAYER)) return;
        this.Affect(collision.transform);
    }

    protected abstract void Affect(Transform objectAffect);
}
