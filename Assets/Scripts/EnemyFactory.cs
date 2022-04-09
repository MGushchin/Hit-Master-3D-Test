using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private List<GameObject> activeEnemiesPool = new List<GameObject>();
    private Queue<GameObject> reserveEnemiesPool = new Queue<GameObject>();
    public GameObject GetEnemy(float enemyLife)
    {
        GameObject enemy;
        if (reserveEnemiesPool.Count == 0) //Если нет уже созданных неактивных объектов врага
            enemy = Instantiate(EnemyPrefab);
        else
        {
            enemy = reserveEnemiesPool.Dequeue();
            enemy.SetActive(true);
        }
        enemy.GetComponent<Enemy>().SetData(enemyLife); //Задаем характеристики противника
        activeEnemiesPool.Add(enemy); //Добавление противника в пул активных противников
        return enemy;
    }

    public void Restart() //Перезапуск уровня
    {
        foreach(GameObject enemy in activeEnemiesPool) //Перезапуск каждого активного противника и добавление в пул неактивных
        {
            ReturnEnemy(enemy.GetComponent<Enemy>());
        }
        activeEnemiesPool.Clear();
    }

    public void ReturnEnemy(Enemy deadEnemy)
    {
        deadEnemy.Ragdoll.ToggleRagdoll(true); //Сбрасывание рэгдолла
        reserveEnemiesPool.Enqueue(deadEnemy.gameObject);
        deadEnemy.gameObject.SetActive(false);
    }
}
