using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : AutoMonoBehaviour
{
    private event System.EventHandler DieEventHandler;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Animator animtor;
    [SerializeField] private PlayerMovement movement;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.animtor = transform.Find("Model")?.GetComponent<Animator>(),
            () => this.movement = transform.Find("Movement")?.GetComponent<PlayerMovement>()
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.DieEventHandler += (s, e) =>
        {
            this.animtor.SetTrigger("Die");
            this.movement.gameObject.SetActive(false);
            GameController.Instance.GameLose();
        };
    }

    public virtual void Die() => this.DieEventHandler?.Invoke(null, System.EventArgs.Empty);
}
