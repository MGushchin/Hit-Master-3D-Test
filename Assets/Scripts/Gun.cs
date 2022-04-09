using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public int Damage = 1;
    private Queue<Projectile> bullets = new Queue<Projectile>();
    private bool canFire = false;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullets.Enqueue(bullet.GetComponent<Projectile>());
            bullet.GetComponent<Projectile>().SetBulletDamage(Damage);
            bullet.SetActive(false);
        }
    }

    public void SetCanFire(bool value) //ћожет ли игрок в данный момент стрел€ть
    {
        canFire = value;
    }

    public void Fire(Vector3 position)
    {
        if (canFire)
        {
            transform.LookAt(position);
            Projectile nextProjectile = bullets.Dequeue();
            nextProjectile.Fire(transform.position, transform.rotation);
            bullets.Enqueue(nextProjectile);
        }
    }
}
