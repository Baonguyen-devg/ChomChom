using UnityEngine;

public class EnemySpawner : Spawner<IEnemyProduct>
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    protected override bool CompareType(Transform p, IEnemyProduct type) =>
        p.GetComponent<IEnemyProduct>() == type;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        EnemySpawner.instance = this;
    }
}

