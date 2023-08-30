using UnityEngine;

public class SFXSpawner : Spawner
{
    private static SFXSpawner instance;
    public static SFXSpawner Instance => instance;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        SFXSpawner.instance = this;
    }

    public virtual void PlaySound(string soundName)
    {
        Transform obj = this.GetObjectByName(soundName);
        if (obj == null) return;

        Transform objSpawn = this.GetPoolObject(obj);
        objSpawn.gameObject.SetActive(true);
        objSpawn.SetParent(this.holder);
        objSpawn.GetComponent<AudioSource>().Play();
    }
}
