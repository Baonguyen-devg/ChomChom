using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : Impact
{
    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Animator animator;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.animator = transform.Find("Model")?.GetComponent<Animator>()
        };
        foreach (var action in this.loadComponentActions) action?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Land")) return;
        this.animator.SetBool("Jump", false);
    }
}
