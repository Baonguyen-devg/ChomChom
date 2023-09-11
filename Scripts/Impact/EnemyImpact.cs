using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : Impact
{
    private const float DEFAULT_RADIUS = 0.5f;

    [Header("[ Impact Follow LayerMask ]"), Space(5)]
    [SerializeField] private bool? isImpact = null;
    [SerializeField] private float radius = DEFAULT_RADIUS;
    [SerializeField] private LayerMask layerMask;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.animator = transform.Find("Model")?.GetComponent<Animator>(),
            () => this.playerController = GameObject.Find("Player").GetComponent<PlayerController>()
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    private void Update()
    {
        if (this.isImpact == true) return;
        this.isImpact = Physics2D.OverlapCircle(transform.position, this.radius, this.layerMask);
        if (this.isImpact == true) this.Affect();
    }

    protected void Affect()
    {
        this.animator.SetTrigger("Bomb");
        SFXSpawner.Instance.PlaySound("Smoke_Bomb_Audio");
        this.playerController.Die();
        StartCoroutine(this.DespawnEnemy());
    }

    private IEnumerator DespawnEnemy()
    {
        yield return new WaitForSeconds(1f);
        EnemySpawner.Instance.Despawn(transform);
    }
}
