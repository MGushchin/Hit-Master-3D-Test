using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public List<EnemySpot> Movepoints = new List<EnemySpot>(); //����� ����������� � ������ ������
    public EnemyFactory Factory;
    public float EnemyLife = 1; //���������� �������� ���� ������ �� ������

    public void SetupEnemies()
    {
        for (int i = 1; i < Movepoints.Count - 1; i++) //�������� �������� ����� ���� �������� ����� ������ � ���������
        {
            int enemyCount = Random.Range(1, 4);
            for (int j = 0; j < enemyCount; j++)
                Movepoints[i].SetEnemy(Factory.GetEnemy(EnemyLife));
        }
    }
}
