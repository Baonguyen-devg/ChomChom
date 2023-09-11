using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : AutoMonoBehaviour
{
    private const float DEFAULT_DISTANCE_SPAWN = 50f;
    private const int DEFAULT_NUMBER_FIRST_LAND = 1;

    [SerializeField] private float distanceSpawn = DEFAULT_DISTANCE_SPAWN;
    [SerializeField] private int numberFirstLand = DEFAULT_NUMBER_FIRST_LAND;
    [SerializeField] private Transform endPointLand;

    private event System.EventHandler CountDownEventHandler;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;
    [SerializeField] private Transform player;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.player = GameObject.Find("Player")?.transform,
            () => this.endPointLand = GameObject.Find("Land_First").transform.Find("End_Point_Land")
        };
        foreach (var action in this.loadComponentActions) action?.Invoke();
    }

    protected override void LoadComponentInAwakeBefore() => this.CountDownEventHandler += this.SpawnLand;
    protected override void Start()
    {
        for (int i = 0; i < this.numberFirstLand; i++)
            this.CountDownEventHandler?.Invoke(null, null);
    }

    private void Update()
    {
        if (Vector3.Distance(this.player.position, this.endPointLand.position) > this.distanceSpawn) return;
        this.CountDownEventHandler?.Invoke(null, null);
    }

    private void SpawnLand(object sender, System.EventArgs e)
    {
        string namePrefabs = LandSpawner.Instance.GetRandomPrefab();
        Transform land = LandSpawner.Instance.Spawn(namePrefabs, this.endPointLand.position, transform.rotation);
        this.endPointLand = land.Find("End_Point_Land").transform;
    }
}
