using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : AutoMonoBehaviour
{
    internal class DataEventHandler: EventArgs
    {
        [SerializeField] private Vector3 pos = default;
        public Vector3 Pos => this.pos;
        public DataEventHandler(Vector3 newPos) => this.pos = newPos;
    }

    private event EventHandler MoveEventHandler;

    /*Begin predicatedload of components*/
    [SerializeField] private List<Action> loadComponentActions;

    [SerializeField] private Transform cam;
    [SerializeField] private float len = default;
    [SerializeField] private float posCamera = default;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.MoveEventHandler += this.MoveToPos;
        this.loadComponentActions = new List<Action>
        {
            () => this.cam = GameObject.Find("Main_Camera")?.transform,
            () => this.len = GetComponent<SpriteRenderer>().bounds.size.x,
            () => this.posCamera = this.cam.localPosition.x
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    private void Update()
    {
        var posX = transform.localPosition.x;
        if (posX > this.posCamera + this.len) posX = this.posCamera - this.len;
        else if (posX < this.posCamera - this.len) posX = this.posCamera + this.len;
        else return;

        Vector3 newPos = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
        this.MoveEventHandler?.Invoke(null, new DataEventHandler(newPos));
    }

    private void MoveToPos(object sender, EventArgs e) =>
        transform.position = ((DataEventHandler)e).Pos;
}
