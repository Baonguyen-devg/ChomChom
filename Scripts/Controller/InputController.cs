using UnityEngine;

public partial class InputController : AutoMonoBehaviour
{
    private static InputController instance;
    public static InputController Instance => instance;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        InputController.instance = this;
    }

    public virtual Vector3 GetMousePosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public virtual bool GetkeyEscape() =>
        Input.GetKey(KeyCode.Escape);
}
