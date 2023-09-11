using UnityEngine;

public class LandSpawner : Spawner<string>
{
    private static LandSpawner instance;
    public static LandSpawner Instance => instance;

    protected override bool CompareType(Transform p, string type) =>
        p.name.Equals(type);

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        LandSpawner.instance = this;
    }
}
