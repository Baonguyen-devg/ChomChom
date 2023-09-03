using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : Impact
{
    private const float DEFAULT_RADIUS = 0.5f;
    private const string NAME_LAYER_PLAYER = "Player";

    [SerializeField] private bool? isImpact = null;
    [SerializeField] private float radius = DEFAULT_RADIUS;
    [SerializeField] private LayerMask layerMask;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.player = GameObject.Find("Player")?.transform,
            () => this.animator = transform.Find("Model")?.GetComponent<Animator>()
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
        StartCoroutine(this.DespawnEnemy());
    }

    private IEnumerator DespawnEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        EnemySpawner.Instance.Despawn(transform);
    }
}
