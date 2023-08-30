using System.Collections.Generic;
using UnityEngine;

public class Spawner : AutoMonoBehaviour
{
    [SerializeField] protected List<Transform> listPrefab;
    [SerializeField] protected List<Transform> poolObjects;
    [SerializeField] protected Transform holder;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPrefab();
        this.LoadHolder();
    }

    protected virtual void LoadPrefab()
    {
        if (this.listPrefab.Count != 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
            this.listPrefab.Add(prefab);
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": Load Holder", gameObject);
    }

    public virtual Transform Spawn(string nameObject, Vector3 postion, Quaternion rotation)
    {
        Transform obj = this.GetObjectByName(nameObject);
        if (obj == null)
        {
            Debug.LogError(nameObject + ": Can find object.");
            return null;
        }

        Transform objSpawn = this.GetPoolObject(obj);
        objSpawn.SetPositionAndRotation(postion, rotation);

        objSpawn.gameObject.SetActive(true);
        objSpawn.SetParent(this.holder);
        return objSpawn;
    }

    protected virtual Transform GetObjectByName(string nameObject)
    {
        foreach (Transform obj in this.listPrefab)
            if (obj.name == nameObject) return obj;
        return null;
    }

    public virtual void Despawn(Transform obj)
    {
        obj.gameObject.SetActive(false);
        this.poolObjects.Add(obj);
    }

    protected virtual Transform GetPoolObject(Transform obj)
    {
        foreach (Transform prefab in this.poolObjects)
            if (obj.name == prefab.name && !prefab.gameObject.activeSelf)
            {
                this.poolObjects.Remove(prefab);
                return prefab;
            }

        Transform newObject = Instantiate(obj);
        newObject.name = obj.name;
        return newObject;
    }

    public virtual string GetRandomPrefab()
    {
        int keyObject = Random.Range(0, this.listPrefab.Count);
        return this.listPrefab[keyObject].name;
    }
}
