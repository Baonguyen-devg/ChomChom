using System.Collections.Generic;
using UnityEngine;

public partial class InputController : AutoMonoBehaviour, ISubject
{
    private static InputController instance;
    public static InputController Instance => instance;

    private List<IObserver> observers = new List<IObserver>();

    [SerializeField] private Vector3 keySpace = Vector3.zero;
    public Vector3 KeySpace => this.keySpace;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        InputController.instance = this;
    }

    private void Update()
    {
        if (this.GetkeyEscape()) GameController.Instance.PauseGame();
        this.CheckKeySpace();
        if (keySpace != Vector3.zero) this.Notify();
    }

    private void CheckKeySpace() =>
        keySpace = new Vector3(this.GetButtonJump() ? 1 : 0, this.GetButtonJumpDown() ? 1 : 0,
                               this.GetButtonJumpUp() ? 1 : 0);

    public virtual Vector3 GetMousePosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public virtual bool GetkeyEscape() => Input.GetKey(KeyCode.Escape);

    public virtual bool GetButtonJump() => Input.GetButton("Jump");

    public virtual bool GetButtonJumpDown() => Input.GetButtonDown("Jump");

    public virtual bool GetButtonJumpUp() => Input.GetButtonUp("Jump");

    public void Attach(IObserver observer) => this.observers.Add(observer);

    public void Detach(IObserver observer) => this.observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in this.observers)
            observer.UpdateObserver(this);
    }
}
