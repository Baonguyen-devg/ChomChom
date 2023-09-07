using System.Collections.Generic;
using UnityEngine;

public partial class InputController : AutoMonoBehaviour, ISubject
{
    private static InputController instance;
    public static InputController Instance => instance;
    private List<IObserver> observers = new List<IObserver>();

    [Header("[ Button Space] : x = normal, y = down, z = up"), Space(5)]
    [SerializeField] private Vector3 buttonSpace = Vector3.zero;
    public Vector3 ButtonSpace => this.buttonSpace;

    protected override void LoadComponentInAwakeBefore() => 
        InputController.instance = this;

    private void Update()
    {
        if (this.GetkeyEscape()) GameController.Instance.PauseGame();
        this.CheckButtonSpace();
        if (this.buttonSpace != Vector3.zero) this.Notify();
    }

    /// <summary>
    ///   <para>Check button space, display by a vector (x, y, z)</para>
    ///   <para>x: buttonJump, y: buttonJumpDown, z: buttonJumpUp</para>
    /// </summary>
    private void CheckButtonSpace() =>
        buttonSpace = new Vector3(this.GetButtonJump() ? 1 : 0, this.GetButtonJumpDown() ? 1 : 0,
                               this.GetButtonJumpUp() ? 1 : 0);

    /// <summary>Gets mouse's position.</summary>
    public virtual Vector3 GetMousePosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public virtual bool GetkeyEscape() => Input.GetKey(KeyCode.Escape);

    /// <summary>Gets the button jump.</summary>
    public virtual bool GetButtonJump() => Input.GetButton("Jump");
    public virtual bool GetButtonJumpDown() => Input.GetButtonDown("Jump");
    public virtual bool GetButtonJumpUp() => Input.GetButtonUp("Jump");

    /// <summary>
    ///   <para>Attach function use for register subject of gamecontroller (Add a observer into list)</para>
    ///   <para>Detach function use for cancel register subject of gamecontroller (Remove a observer in list)</para>
    ///   <para>Notify function use for remind all observers when event happen </para>
    /// </summary>
    public void Attach(IObserver observer) => this.observers.Add(observer);
    public void Detach(IObserver observer) => this.observers.Remove(observer);
    public void Notify()
    {
        foreach (var observer in this.observers)
            observer.UpdateObserver(this);
    }
}
