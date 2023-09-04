using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : AutoMonoBehaviour
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

    public virtual Transform Spawn(string nameObject, Vector3 postion, Quaternion rotation)
    {
        Transform obj = this.GetObjectByName(nameObject);
        if (obj == null) return null;

        Transform objSpawn = this.GetPoolObject(obj);
        objSpawn.SetPositionAndRotation(postion, rotation);

        objSpawn.gameObject.SetActive(true);
        objSpawn.SetParent(this.holder);
        return objSpawn;
    }

    protected virtual Transform GetObjectByName(string nameObject)
    {
        var prefabs = this.listPrefab.Where(p => nameObject.Equals(p.name)).ToList();
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
