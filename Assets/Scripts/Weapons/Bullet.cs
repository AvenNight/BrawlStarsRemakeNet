using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public bool IsRndDmg;
    private int damage => IsRndDmg ? Random.Range(1, Damage * 2 - 1) : Damage;
    public float Distance;
    public float Speed;
    public string[] EnemyTags;

    private Vector3 direction;

    public void SetParams(int damage, bool isRndDmg, float distance, float speed, params string[] enemyTags)
    {
        Damage = damage;
        IsRndDmg = isRndDmg;
        Distance = distance;
        Speed = speed;
        EnemyTags = enemyTags;
    }

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Destroy(this.gameObject, Distance / Speed);
    }

    private void FixedUpdate() =>
        transform.Translate(direction.normalized * Speed * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Grass") return;
        if (EnemyTags.Contains(tag))
        {
            var enemy = other.GetComponent<IDamaged>();
            enemy.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}