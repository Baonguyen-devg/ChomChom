using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGameOverImpact : Impact
{
    private const string DEFAULT_NAME_LAYERMASK = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(DEFAULT_NAME_LAYERMASK)) return;
        GameController.Instance.GameLose();
    }
}
