using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public List<EnemySpot> Movepoints = new List<EnemySpot>(); //Точки перемещение и спауна врагов
    public EnemyFactory Factory;
    public float EnemyLife = 1; //Количество здоровья всех врагов на уровне

    public void SetupEnemies()
    {
        for (int i = 1; i < Movepoints.Count - 1; i++) //Передача объектов врага всем областям кроме первой и последней
        {
            int enemyCount = Random.Range(1, 4);
            for (int j = 0; j < enemyCount; j++)
                Movepoints[i].SetEnemy(Factory.GetEnemy(EnemyLife));
        }
    }
}
