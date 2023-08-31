using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnLand : AutoMonoBehaviour, IObserver
{    
    private const float DEFAULT_SPEED_SPAWN = 1f;
    private const float DEFAULT_MIN_HEIGHT = -5.5f;
    private const float DEFAULT_MAX_HEIGHT = -4.5f;

    [SerializeField] private float speedSpawn = DEFAULT_SPEED_SPAWN;
    [SerializeField] private float countDown = DEFAULT_SPEED_SPAWN;

    private event System.EventHandler CountDownEventHandler;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.CountDownEventHandler += this.SpawnLand;
    }

    private void Update()
    {
        this.countDown = this.countDown - Time.deltaTime;
        if (this.countDown <= 0)
        {
            this.countDown = this.speedSpawn;
            this.CountDownEventHandler?.Invoke(null, null);
        }
    }

    public void UpdateObserver(ISubject subject)
    {
        var gameControllerSubject = (subject as GameController);
        this.speedSpawn = this.speedSpawn * gameControllerSubject.SpeedGame;
    }

    private void SpawnLand(object sender, System.EventArgs e)
    {
        string namePrefabs = LandSpawner.Instance.GetRandomPrefab();
        float height = (Random.Range(0, 2) == 0) ? DEFAULT_MIN_HEIGHT : DEFAULT_MAX_HEIGHT;

        Vector3 pos = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        LandSpawner.Instance.Spawn(namePrefabs, pos, transform.rotation);
    }
}
