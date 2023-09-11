using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : AutoMonoBehaviour
{
    private const float DEFAULT_DISTANCE_RESET_POSITION = 18f;

    [SerializeField] private float distanceResetPos = DEFAULT_DISTANCE_RESET_POSITION;
    [SerializeField] private Vector3 firstPostion = Vector3.zero;
    [SerializeField] private bool isReset = default;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Transform player;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.player = GameObject.Find("Player").transform
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    protected override void Start() => this.firstPostion = transform.parent.position;
    protected override void OnEnable() => this.isReset = false;

    private void Update()
    {
        if (this.isReset) return;
        if (Vector3.Distance(this.player.position, transform.parent.position) > this.distanceResetPos) return;
        Vector3 resetPos = transform.parent.position;
        resetPos.y = this.firstPostion.y;
        (transform.parent.position, this.isReset) = (resetPos, true);
    }
}
