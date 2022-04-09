using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpot : MonoBehaviour
{
    public List<Vector3> EnemyLocalPositions = new List<Vector3>(); //������� ������������ ������� �������
    public UnityEvent<EnemySpot> OnCompleted = new UnityEvent<EnemySpot>(); //����� ��� ����������� �����
    private List<Vector3> emptyPositions = new List<Vector3>(); //��������� ������� ��� ���������� ������
    private Dictionary<Enemy, Vector3> occupiedPlaces = new Dictionary<Enemy, Vector3>(); //������� ������� �������
    public int EnemyCount => aliveEnemies;
    private int aliveEnemies = 0;
    private bool inCombat = false; // ��������� �� ������ ����� � ���� �������

    private void Awake()
    {
        foreach (Vector3 position in EnemyLocalPositions) //������� �������� ������� ��� ����������� � �������� ������
            emptyPositions.Add(position + transform.position); //��������� �������� �� �������� ������� �� ������
    }

    public void SetEnemy(GameObject enemy)
    {
        Vector3 randomPosition = emptyPositions[Random.Range(0, emptyPositions.Count)];
        enemy.transform.position = randomPosition;
        enemy.transform.LookAt(transform.position);
        Enemy currentEnemy = enemy.GetComponent<Enemy>();
        currentEnemy.OnDead.AddListener(EnemyDeath);
        occupiedPlaces.Add(currentEnemy, randomPosition);
        emptyPositions.Remove(randomPosition);
        aliveEnemies++;
    }

    public void EnemyDeath(Enemy deadEnemy)
    {
        emptyPositions.Add(occupiedPlaces[deadEnemy]); //���������� ������������� ������� � ��� ��������� �������
        occupiedPlaces.Remove(deadEnemy); //�������� ������� �������
        aliveEnemies--;
        deadEnemy.OnDead.RemoveListener(EnemyDeath);
        if (aliveEnemies == 0 && inCombat) //���� ����� � ������� �������� � ����������� ������ �� ��������
            OnCompleted.Invoke(this);
    }

    public void Restart() //���������� �����
    {
        foreach (Vector3 position in occupiedPlaces.Values)
            emptyPositions.Add(position);
        occupiedPlaces.Clear();
        inCombat = false;
        aliveEnemies = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if (EnemyCount > 0) //���� �� ����� ���� ���������� ������������� ������ � �������� ���� ��� ����������� � ���
            {
                other.gameObject.GetComponent<Player>().Stop();// ����������
                inCombat = true;
            }
            else
                OnCompleted.Invoke(this); //���� ����������� �� ����� ���, �� ��������� � ����������� �������
        }
    }
}
