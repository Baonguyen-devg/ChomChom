using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory 
{
    public abstract IEnemyProduct CreateEnemy(string name, Vector3 pos, Quaternion rot);
}
