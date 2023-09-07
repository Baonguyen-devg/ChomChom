using UnityEngine;

public partial class PlayerMovement : Movement, IObserver
{
    protected override void Move()
    {
        this.isGrounded = Physics2D.OverlapCircle(transform.position, this.radius, this.layerMask);

        if (!this.isGrounded) return;
        if (Vector3.Distance(transform.parent.position, this.firstPosition.position) <= 1) return;
        transform.parent.position = Vector3.Lerp(transform.parent.position,
                                                this.firstPosition.position, this.speed);
    }

    private void RegisterEventInput() =>
        InputController.Instance.Attach(this);

    public void UpdateObserver(ISubject subject)
    {
        Vector3 dataKeySpace = (subject as InputController).ButtonSpace;

        if (this.isGrounded && dataKeySpace.y == 1)
        {
            this.animator.SetBool("Jump", true);
            SFXSpawner.Instance.PlaySound("Jump_Audio");

            (this.countDown, this.isJump) = (DEFAULT_COUNT_DOWN, true);
            this.JumpEventHandler?.Invoke(null, null);
        }

        if (dataKeySpace.x == 1 && this.isJump)
        {
            this.jumpForce = Mathf.Min(DEFAULT_MAX_JUMP_FORCE, this.jumpForce + 0.01f);
            this.countDown = this.countDown - Time.deltaTime;
            if (this.countDown > 0) this.JumpEventHandler?.Invoke(null, null);
        }

        if (dataKeySpace.z == 1)
            (this.isJump, this.jumpForce) = (false, DEFAULT_JUMP_FORCE);
    }

    private void Jump(object sender, System.EventArgs e) =>
        this.rigid2D.velocity = this.directionJump * this.jumpForce;
}
