using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSpawner : Spawner
{
    private static LandSpawner instance;
    public static LandSpawner Instance => instance;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        LandSpawner.instance = this;
    }
}
