using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerMovement : AutoMonoBehaviour, IObserver
{
    private void RegisterEventInput() =>
        InputController.Instance.Attach(this);

    public void UpdateObserver(ISubject subject)
    {
        Vector3 dataKeySpace = (subject as InputController).KeySpace;
        this.isGrounded = Physics2D.OverlapCircle(transform.position, this.radius, this.layerMask);

        if (this.isGrounded && dataKeySpace.y == 1)
        {
            this.animator.SetBool("Jump", true);
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
