using System.Collections.Generic;
using UnityEngine;

public partial class PlayerMovement : Movement, IObserver
{
    private const float DEFAULT_JUMP_FORCE = 4f;
    private const float DEFAULT_MAX_JUMP_FORCE = 10f;
    private const float DEFAULT_RADIUS = 0.2f;
    private const float DEFAULT_COUNT_DOWN = 0.7f;

    [Header("[ Jump behaviour ]"), Space(5)]
    [SerializeField] private Vector2 directionJump = Vector2.up;
    [SerializeField] private float jumpForce = DEFAULT_JUMP_FORCE;
    [SerializeField] private float radius = DEFAULT_RADIUS;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJump = false;
    [SerializeField] private float countDown = DEFAULT_COUNT_DOWN;

    private event System.EventHandler JumpEventHandler;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Transform firstPosition;
    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody2D rigid2D;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.rigid2D = transform.parent.GetComponent<Rigidbody2D>(),
            () => this.animator = transform.parent.Find("Model")?.GetComponent<Animator>(),
            () => this.firstPosition = GameObject.Find("Background").transform.Find("First_Position")
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.JumpEventHandler += this.Jump;
    }

    protected override void Start() => this.RegisterEventInput();
}
