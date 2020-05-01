using UnityEngine;
using UnityEngine.AI;

public class EnemyWithAI : Creature
{
    public float UpdateTimeAttack;
    public float UpdateTimeMove;
    public float AggroDistance;
    private float curAggroDistance;
    public float AttackRange;
    public float WanderRadius;

    [SerializeField] private RangeWeapon curWeapon;

    private ObjectsFinder playersFinder;
    private Vector3 direction;

    void Start()
    {
        curWeapon.EnemyTags = enemyTags;
        curAggroDistance = AggroDistance;
        InvokeRepeating("MovenentAI", 0f, UpdateTimeMove);
        InvokeRepeating("AttackAI", 0f, UpdateTimeAttack);
    }

    private void MovenentAI()
    {
        if (TryGetDirection(out direction) && playersFinder.DistanceToNearest < curAggroDistance)
            agent.SetDestination(playersFinder.NearestObject.transform.position);
        else
        {
            var newDestination = RandomNavSphere(this.transform.position, WanderRadius, -1);
            agent.SetDestination(newDestination);
        }
    }

    private bool TryGetDirection(out Vector3 direction)
    {
        playersFinder = new CreatureFinder(this.gameObject, enemyTags);
        if (playersFinder.Objects.Count == 0)
        {
            direction = Vector3.zero;
            return false;
        }
        if (playersFinder.NearestObject.TryGetComponent(out IInvisible invisibility))
        {
            curAggroDistance = invisibility.Invisible ? AggroDistance / 3 : AggroDistance;
            direction = invisibility.Invisible ? this.direction : playersFinder.Direction;
        }
        else
            direction = playersFinder.Direction;
        return true;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float wanderRadius, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * wanderRadius;
        NavMesh.SamplePosition(randDirection + origin, out NavMeshHit navHit, wanderRadius, layermask);
        return navHit.position;
    }

    private void AttackAI()
    {
        if (!TryGetDirection(out direction)) return;
        var ray = new Ray(this.transform.position, direction);
        bool hitted = Physics.Raycast(ray, out RaycastHit hit, AttackRange);
        if (hitted && (hit.collider.tag == "Player"))
            curWeapon.Shoot(direction);
    }
}