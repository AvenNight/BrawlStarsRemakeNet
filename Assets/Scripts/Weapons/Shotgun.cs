using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangeWeapon
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;
    [SerializeField] private bool isRndDmg;
    [SerializeField] private int bulletCount;
    [SerializeField] private float speed;

    public override void Shoot(Vector3 direction)
    {
        if (curAmmo <= 0 || direction == Vector3.zero) return;
        bullet.SetParams(damage, isRndDmg, distance, speed, EnemyTags);
        for (int i = 0; i < bulletCount; i++)
        {
            var rndQuaternion = Quaternion.AngleAxis(Random.Range(-angle / 2, angle / 2), Vector3.up);
            var b = Instantiate(bullet, this.gameObject.transform.position + direction.normalized * 0.55f, Quaternion.Euler(direction));
            b.GetComponent<Bullet>().Shoot(rndQuaternion * direction);
        }
        base.Shoot(direction);
    }
}