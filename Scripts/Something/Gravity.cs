using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : AutoMonoBehaviour, IObserver
{
    private const float DEFAULT_RATE_INCREASE_GRAVITY = 0.1f;
    private const float DEFAULT_MAX_GRAVITY = 10f;
    private const float DEFAULT_MIN_GRAVITY = 3f;

    [SerializeField] private float rateIncreaseGravity = DEFAULT_RATE_INCREASE_GRAVITY;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> predicateLoad;

    [SerializeField] private Rigidbody2D rigid2D;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.predicateLoad = new List<System.Action>
        {
            () => this.rigid2D = GetComponent<Rigidbody2D>(),
            () => this.rigid2D.gravityScale = DEFAULT_MIN_GRAVITY
        };
        foreach (var predicate in this.predicateLoad)
            predicate?.Invoke();
    }

    protected override void Start() => this.RegisterEventInput();

    private void RegisterEventInput() =>
        InputController.Instance.Attach(this);

    public void UpdateObserver(ISubject subject) =>
        this.rigid2D.gravityScale = Mathf.Min(this.rigid2D.gravityScale + 
                                                this.rateIncreaseGravity, DEFAULT_MAX_GRAVITY);
}
