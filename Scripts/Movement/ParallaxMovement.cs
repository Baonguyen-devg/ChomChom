using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : Movement, IObserver
{
    [SerializeField] private Vector3 direction = Vector3.left;

    protected override void Start() =>
        this.RegisterSubject(GameController.Instance);

    public void UpdateObserver(ISubject subject)
    {
        var gameControllerSubject = (subject as GameController);
        this.speed = this.speed * gameControllerSubject.SpeedGame;
    }

    private void RegisterSubject(ISubject subject) =>
        (subject as GameController).Attach(this);

    protected override void Move() =>
        transform.parent.position = Vector3.Lerp(transform.parent.position,
                                    transform.parent.position + this.direction, this.speed);
}
