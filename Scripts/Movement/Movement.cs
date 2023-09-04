using UnityEngine;

public abstract class Movement : AutoMonoBehaviour
{
    protected const float DEFAULT_SPEED = 1f;
    protected const float DEFAULT_MAXIMUM_SPEED = 10;
    protected const float DEFAULT_MINIMUM_SPEED = 0.01f;

    [Header("[ Move behaviour ]"), Space(5)]
    [SerializeField] protected float speed = DEFAULT_SPEED;
    [SerializeField] protected float maximumSpeed = DEFAULT_MAXIMUM_SPEED;
    [SerializeField] protected float minimumSpeed = DEFAULT_MINIMUM_SPEED;

    [SerializeField] protected bool isMovingFast = false;

    protected virtual void Update()
    {
        if (Time.timeScale == 1) this.Move();
    }

    public virtual void IncreaseSpeed(float speed) =>
        this.speed = Mathf.Min(a: this.maximumSpeed, b: this.speed + speed);

    public virtual void DecreaseSpeed(float speed) =>
        this.speed = Mathf.Max(a: this.minimumSpeed, b: this.speed - speed);

    protected abstract void Move();
}
