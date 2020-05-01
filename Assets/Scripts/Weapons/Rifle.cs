using System.Collections.Generic;
using UnityEngine;

public class Rifle : RangeWeapon
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;
    [SerializeField] private bool isRndDmg;
    [SerializeField] private float speed;

    public override void Shoot(Vector3 direction)
    {
        if (curAmmo <= 0 || direction == Vector3.zero) return;
        bullet.SetParams(damage, isRndDmg, distance, speed, EnemyTags);
        var b = Instantiate(bullet, this.gameObject.transform.position + direction.normalized * 0.55f, Quaternion.Euler(direction));
        b.GetComponent<Bullet>().Shoot(direction);
        base.Shoot(direction);
    }
}
