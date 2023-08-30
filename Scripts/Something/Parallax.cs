using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : AutoMonoBehaviour
{
    private const float DEFAULT_PARALLAX_NUMBER = 1f;
    [SerializeField] private float parallaxNumber = DEFAULT_PARALLAX_NUMBER;

    /*Begin predicatedload of components*/
    [SerializeField] private List<Action> predicateLoad;

    [SerializeField] private Transform cam;
    [SerializeField] private float len = default;
    [SerializeField] private float pos = default;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.predicateLoad = new List<Action>
        {
            () => this.cam = GameObject.Find("Main_Camera")?.transform,
            () => this.len = GetComponent<SpriteRenderer>().bounds.size.x,
            () => this.pos = transform.position.x
        };
        foreach (var predicate in this.predicateLoad)
            predicate?.Invoke();
    }

    private void Update()
    {
        float Res = this.cam.position.x * (1 - this.parallaxNumber);
        float Dis = this.cam.position.x * this.parallaxNumber;
        transform.position = new Vector3(this.pos + Dis, transform.position.y, transform.position.z);
        if (Res > this.pos + this.len) this.pos += this.len;
        else if (Res < this.pos - this.len) this.pos -= this.len;
    }
}
