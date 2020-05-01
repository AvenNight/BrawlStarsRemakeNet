using System.Collections;
using UnityEngine;

public abstract class RangeWeapon : MonoBehaviour
{
    [SerializeField] protected float distance;
    public float Distance => distance;
    [SerializeField] protected float angle;
    public float Angle => angle;

    [SerializeField] private BarScript barAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float cooldownSec;
    protected bool isRecharging;
    protected int curAmmo;

    [HideInInspector] public string[] EnemyTags;

    protected virtual void Start() => curAmmo = maxAmmo;

    protected virtual void FixedUpdate()
    {
        if (curAmmo < maxAmmo && !isRecharging)
            StartCoroutine("Recharge");
    }

    protected virtual IEnumerator Recharge()
    {
        isRecharging = true;
        yield return new WaitForSeconds(cooldownSec);
        curAmmo++;
        BarChange();
        barAmmo.BarChange((float)curAmmo / maxAmmo);
        isRecharging = false;
    }

    protected virtual void BarChange() => barAmmo.BarChange((float)curAmmo / maxAmmo);

    public virtual void Shoot(Vector3 direction)
    {
        if (curAmmo <= 0) return;
        curAmmo--;
        BarChange();
    }
}