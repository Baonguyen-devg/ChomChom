using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandOnEnable : AutoMonoBehaviour
{
    [SerializeField] private List<GameObject> objectOnLands = new List<GameObject>();

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.objectOnLands.Count != 0) this.objectOnLands.Clear();
        foreach (Transform obj in transform)
            this.objectOnLands.Add(obj.gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (GameObject obj in this.objectOnLands)
            foreach (Transform obj2 in obj.transform)
                obj2.gameObject.SetActive(true);
    }
}
