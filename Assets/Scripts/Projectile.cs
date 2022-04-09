using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10;
    public float BulletLifeTime = 2;
    private Transform ownTransform;
    private float damage = 1;
    private IEnumerator lifeTimer; //Корутина отключающая пулю если она летит больше времени ее жизни

    private void Awake()
    {
        ownTransform = gameObject.GetComponent<Transform>(); //Кешируем собственный трансформ
    }

    private void FixedUpdate()
    {
        ownTransform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime); //Постоянное движение пули вперед
    }

    public void SetBulletDamage(float damage) //Изменение урона пули
    {
        this.damage = damage;
    }

    public void Fire(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        lifeTimer = lifeTimeDelay(BulletLifeTime);
        StartCoroutine(lifeTimer);
        ownTransform.position = position;
        ownTransform.rotation = rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null) //Если пуля сталкивается с врагом, то наносим ему урон
        {
            other.gameObject.GetComponent<Enemy>().TakeHit(damage);
            other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.position - gameObject.transform.position);
        }
        StopCoroutine(lifeTimer);
        gameObject.SetActive(false);
    }

    private IEnumerator lifeTimeDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
