using System.Collections.Generic;
using UnityEngine;

public partial class InputController : AutoMonoBehaviour, ISubject
{
    private static InputController instance;
    public static InputController Instance => instance;

    private List<IObserver> observers = new List<IObserver>();

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        InputController.instance = this;
    }

    private void Update()
    {
        this.GetKeySpace();
    }

    public virtual Vector3 GetMousePosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public virtual bool GetkeyEscape() => Input.GetKey(KeyCode.Escape);

    public virtual void GetKeySpace()
    {
        if (Input.GetKey(KeyCode.Space)) this.Notify();
    }

    public void Attach(IObserver observer) => this.observers.Add(observer);

    public void Detach(IObserver observer) => this.observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in this.observers)
            observer.UpdateObserver(this);
    }
}
