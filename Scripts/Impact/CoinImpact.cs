using UnityEngine;

public class CoinImpact : Impact
{
    private const string DEFAULT_NAME_LAYERMASK = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(DEFAULT_NAME_LAYERMASK)) return;
        GameController.Instance.IncreaseCoin(1);
        gameObject.SetActive(false);
    }
}
