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
        if (reserveEnemiesPool.Count == 0) //���� ��� ��� ��������� ���������� �������� �����
            enemy = Instantiate(EnemyPrefab);
        else
        {
            enemy = reserveEnemiesPool.Dequeue();
            enemy.SetActive(true);
        }
        enemy.GetComponent<Enemy>().SetData(enemyLife); //������ �������������� ����������
        activeEnemiesPool.Add(enemy); //���������� ���������� � ��� �������� �����������
        return enemy;
    }

    public void Restart() //���������� ������
    {
        foreach(GameObject enemy in activeEnemiesPool) //���������� ������� ��������� ���������� � ���������� � ��� ����������
        {
            ReturnEnemy(enemy.GetComponent<Enemy>());
        }
        activeEnemiesPool.Clear();
    }

    public void ReturnEnemy(Enemy deadEnemy)
    {
        deadEnemy.Ragdoll.ToggleRagdoll(true); //����������� ��������
        reserveEnemiesPool.Enqueue(deadEnemy.gameObject);
        deadEnemy.gameObject.SetActive(false);
    }
}
