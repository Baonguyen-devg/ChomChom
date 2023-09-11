using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawner
{
    private const float DEFAULT_DISTANCE_DESPAWN = 11f;
    [SerializeField] private float distanceDespawn = DEFAULT_DISTANCE_DESPAWN;
    [SerializeField] private float distance;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Transform pointEndLand;
    [SerializeField] private Transform pointDespawnLand;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        this.loadComponentActions = new List<System.Action>
        {
            () => this.pointEndLand = transform.parent.Find("End_Point_Land"),
            () => this.pointDespawnLand = GameObject.Find("Main_Camera")?.transform.Find("Point_Despawn_Land")
        };

        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    /// <summary>
    ///   <para>This class create for despawn land prefabs by distance </para>
    ///   <para>That mean if distance between point end land and a point despawn &lt; distance request (distanceDespawn) =&gt; despawn</para>
    /// </summary>
    public override void DespawnObject() => LandSpawner.Instance.Despawn(transform.parent);
    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(this.pointEndLand.position, this.pointDespawnLand.position);
        if (this.distance <= this.distanceDespawn) return true;
        else return false;
    }
}
