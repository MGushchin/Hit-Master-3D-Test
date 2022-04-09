using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpot : MonoBehaviour
{
    public List<Vector3> EnemyLocalPositions = new List<Vector3>(); //ѕозиции относительно нулевой позиции
    public UnityEvent<EnemySpot> OnCompleted = new UnityEvent<EnemySpot>(); //Ёвент при прохождении этапа
    [SerializeField]
    private List<Vector3> emptyPositions = new List<Vector3>(); //—вободные позиции дл€ размещени€ врагов
    private Dictionary<Enemy, Vector3> occupiedPlaces = new Dictionary<Enemy, Vector3>(); //ѕозиции зан€тые врагами
    public int EnemyCount => aliveEnemies;
    private int aliveEnemies = 0;
    private bool inCombat = false; // —ражаетс€ ли сейчас игрок в этой области

    private void Awake()
    {
        foreach (Vector3 position in EnemyLocalPositions) //ѕеренос открытых позиций дл€ противников в закрытый список
            emptyPositions.Add(position + transform.position); //ƒобавл€ем поправку на смещение области от центра
    }

    public void SetEnemy(GameObject enemy)
    {
        Vector3 randomPosition = emptyPositions[Random.Range(0, emptyPositions.Count)];
        Enemy currentEnemy = enemy.GetComponent<Enemy>();
        currentEnemy.Agent.Warp(randomPosition);
        currentEnemy.transform.LookAt(transform.position);
        currentEnemy.OnDead.AddListener(EnemyDeath);
        occupiedPlaces.Add(currentEnemy, randomPosition);
        emptyPositions.Remove(randomPosition);
        aliveEnemies++;
    }

    public void EnemyDeath(Enemy deadEnemy)
    {
        emptyPositions.Add(occupiedPlaces[deadEnemy]); //¬озвращаем освобожденную позицию в пул свободный позиций
        occupiedPlaces.Remove(deadEnemy); //ќчищиаем зан€тую позицию
        aliveEnemies--;
        deadEnemy.OnDead.RemoveListener(EnemyDeath);
        if (aliveEnemies == 0 && inCombat) //≈сли игрок в области сражалс€ и противников больше не осталось
            OnCompleted.Invoke(this);
    }

    public void Restart() //ѕерезапуск точки
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
            if (EnemyCount > 0) //≈сли на сцене есть противники помечаем зону как наход€щуюс€ в бою
            {
                inCombat = true;
            }
            else
                OnCompleted.Invoke(this); //≈сли противников на сцене нет, то объ€вл€ем о прохождении локации
        }
    }
}
