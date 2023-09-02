using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    private const float DEFAULT_DISTANCE_TO_ATTACK = 1f;

    [SerializeField] private float distanceToAttack = DEFAULT_DISTANCE_TO_ATTACK;
    [SerializeField] private bool isExplorate = false;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> predicateLoad;

    [SerializeField] private Transform player;
    /*End predicatedload of components*/

    protected virtual void Awake()
    {
        this.predicateLoad = new List<System.Action>
        {
            () => this.player = GameObject.Find("Player")?.transform,
        };
        foreach (var predicate in this.predicateLoad)
            predicate?.Invoke();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (this.isExplorate) return;
        if (Vector3.Distance(this.player.position, animator.transform.parent.position) > this.distanceToAttack) return;
        animator.SetTrigger("Bomb");
        this.isExplorate = true;
        VFXSpawner.Instance.Spawn("Smoke_Bomb", animator.transform.parent.position, animator.transform.parent.rotation);
    }
}
