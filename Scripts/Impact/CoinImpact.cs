using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinImpact : Impact
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        GameController.Instance.IncreaseCoin(1);
        gameObject.SetActive(false);
    }
}
