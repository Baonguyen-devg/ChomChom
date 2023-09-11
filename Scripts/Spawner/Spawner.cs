using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Spawner<T>: AutoMonoBehaviour
{
    [SerializeField] protected List<Transform> poolObjects;

    //Begin predicatedload of components
    [SerializeField] protected List<System.Action> loadComponentActions;

    [SerializeField] protected List<Transform> listPrefab = new List<Transform>();
    [SerializeField] protected Transform holder;
    //End predicatedload of components

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.listPrefab.Count != 0) this.listPrefab.Clear();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.holder = transform.Find("Holder"),
            () => this.listPrefab.AddRange(transform.Find("Prefabs").Cast<Transform>())
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    public virtual Transform Spawn(T typeObject, Vector3 pos, Quaternion rot)
    {
        Transform obj = this.GetObjectByType(typeObject);
        if (obj == null) return null;

        Transform objSpawn = this.GetPoolObject(obj);
        objSpawn.SetPositionAndRotation(pos, rot);

        objSpawn.gameObject.SetActive(true);
        objSpawn.SetParent(this.holder);
        return objSpawn;
    }

    protected abstract bool CompareType(Transform p, T type);
    protected virtual Transform GetObjectByType(T type)
    {
        var prefabs = this.listPrefab.Where(p => this.CompareType(p, type)).ToList();
        return (prefabs.Any()) ? prefabs[0] : null;
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

    public virtual string GetRandomPrefab() =>
        this.listPrefab[Random.Range(0, this.listPrefab.Count)].name;
}