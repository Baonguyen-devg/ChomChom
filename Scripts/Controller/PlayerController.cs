using System;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : AutoMonoBehaviour
{
    /*Begin predicatedload of components*/
    [SerializeField] private List<Action> predicateLoad;

    [SerializeField] private Transform model;
    public Transform Model => this.model;

    [SerializeField] private PlayerMovement movement;
    public PlayerMovement Movement => this.movement;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        predicateLoad = new List<Action>
        {
            () => this.model = transform.Find("Model"),
            () => this.movement = transform.Find("Movement")?.GetComponent<PlayerMovement>()
        };

        foreach (var predicate in predicateLoad)
            predicate?.Invoke();
    }
}
