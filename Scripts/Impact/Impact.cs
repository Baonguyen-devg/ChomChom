using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Impact : AutoMonoBehaviour
{
    [Header("[ Components For Impact ]"), Space(5)]
    [SerializeField] protected Rigidbody2D rigid2D;
    protected virtual void LoadRigidbody2D() =>
        this.rigid2D = GetComponent<Rigidbody2D>();

    [SerializeField] protected Collider2D colli2D;
    protected virtual void LoadCollider2D() =>
        this.colli2D = GetComponent<Collider2D>();

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider2D();
        this.LoadRigidbody2D();
    }
}
