using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public RagdollState Ragdoll;
    public UnityEvent<Enemy> OnDead = new UnityEvent<Enemy>();
    public UnityEvent<float, float> OnHealthChanged = new UnityEvent<float, float>();
    [SerializeField]
    private float maximumLife;
    public float MaximumLife => maximumLife;
    private float life;
    public float Life => life;

    public void SetData(float health) //Установка значений здоровья
    {
        maximumLife = health;
        life = maximumLife;
        OnHealthChanged.Invoke(life, maximumLife);
    }

    public void TakeHit(float damage) //Обработка получения урона
    {
        life = Mathf.Clamp(life - damage, 0, maximumLife);
        OnHealthChanged.Invoke(life, maximumLife);
        if (life == 0)
            death();
    }

    private void death()
    {
        OnDead.Invoke(this);
        Ragdoll.ToggleRagdoll(false);
    }
}
