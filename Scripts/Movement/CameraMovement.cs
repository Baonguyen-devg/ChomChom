using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : Movement
{
    /*Begin predicatedload of components*/
    [SerializeField] private List<Action> predicateLoad;

    [SerializeField] private Transform player;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.predicateLoad = new List<Action>
        {
            () => this.player = GameObject.Find("Player")?.transform,
        };
        foreach (var predicate in this.predicateLoad)
            predicate?.Invoke();
    }

    protected override void Move()
    {
        Vector3 pos = new Vector3(this.player.localPosition.x, transform.localPosition.y, -10);
        transform.parent.position = Vector3.MoveTowards(transform.position, pos, this.speed);
    }
}