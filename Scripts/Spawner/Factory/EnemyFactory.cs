using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   <para>This class was created for practice factoy method design pattern</para>
///   <para>Don't use in project</para>
/// </summary>
public class EnemyFactory : AutoMonoBehaviour, IEnemyFactory
{
    private static EnemyFactory instance;
    public static EnemyFactory Instance => instance;

    [SerializeField] private Dictionary<string, IEnemyProduct> enemyProducts;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        EnemyFactory.instance = this;
        this.enemyProducts = new Dictionary<string, IEnemyProduct>()
        {
            {"Red_Chom", new RedChomEnemyController() },
            {"Green_Chom", new GreenChomEnemyController() },
        };
    }

    private IEnemyProduct GetValue(string key) => this.enemyProducts[key];
    public IEnemyProduct CreateEnemy(string name, Vector3 pos, Quaternion rot)
    {
        IEnemyProduct product = this.GetValue(name);
        if (product == null) return null;

        IEnemyProduct enemyProduct = EnemySpawner.Instance.Spawn(product, pos, rot).
                                        GetComponent<IEnemyProduct>();
        enemyProduct.Initialize();
        return enemyProduct;
    }
}
