using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AutoMonoBehaviour, IObserver
{
    private const float DEFAULT_JUMP_FORCE = 2f;
    private const float DEFAULT_RADIUS = 3f;

    [SerializeField] private Vector2 directionJump = Vector2.up;
    [SerializeField] private float jumpForce = DEFAULT_JUMP_FORCE;
    [SerializeField] private float radius = DEFAULT_RADIUS;

    [SerializeField] private bool isGrounded = false;

    private event System.EventHandler JumpEventHandler;
   
    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> predicateLoad;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody2D rigid2D;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.predicateLoad = new List<System.Action>
        {
            () => this.rigid2D = transform.parent.GetComponent<Rigidbody2D>(),
        };
        foreach (var predicate in this.predicateLoad)
            predicate?.Invoke();
    }

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.JumpEventHandler += this.Jump;
    }

    protected override void Start() => this.RegisterEventInput();

    private void RegisterEventInput() => 
        InputController.Instance.Attach(this);

    public void UpdateObserver(ISubject subject)
    {
        this.isGrounded = Physics2D.OverlapCircle(transform.position,
                                     this.radius, this.layerMask);

        if (this.isGrounded) this.JumpEventHandler?.Invoke(null, null);
    }

    private void Jump(object sender, System.EventArgs e) =>
        this.rigid2D.velocity = this.directionJump * this.jumpForce;

}   
