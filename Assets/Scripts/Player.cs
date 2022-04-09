using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public NavMeshAgent Agent;
    public UnitState StateChanger;
    public Gun Weapon;
    private Queue<Vector3> movePoints = new Queue<Vector3>();
    private IEnumerator movingCoroutine;

    public void StartMoving(Queue<Vector3> positions) //«апуск игрока по точкам перемещени€
    {
        movePoints = positions;
        movingCoroutine = moving();
        StartCoroutine(movingCoroutine);
    }

    public void Stop()
    {
        Agent.ResetPath();
        StopCoroutine(movingCoroutine);
        StateChanger.SetState(State.Idle);
    }

    public void AreaCleared(EnemySpot completedPoint) //ћетод подписываетс€ на событие прохождение текущей не пройденной локации и при срабатывании продолжает путь
    {
        completedPoint.OnCompleted.RemoveListener(AreaCleared);
        movingCoroutine = moving();
        StartCoroutine(movingCoroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemySpot>() != null)
        {
            if (other.gameObject.GetComponent<EnemySpot>().EnemyCount > 0) //≈сли срабатывает триггер не пустой области, то подписываемс€ на событие ее прохождени€ и останавливаемс€
            {
                Stop();
                other.gameObject.GetComponent<EnemySpot>().OnCompleted.AddListener(AreaCleared);
            }
        }
    }

    private IEnumerator moving()
    {
        if (movePoints.Count > 0) //ƒвигаемс€ только если очередь точек прохождени€ не пуста
        {
            Agent.SetDestination(movePoints.Peek());
            StateChanger.SetState(State.Run);
            while (movePoints.Count > 0)
            {
                if ((Agent.remainingDistance <= Agent.stoppingDistance) && Agent.remainingDistance > 0.1f) //второе условие дл€ исключени€ ложных срабатываний
                {
                    Agent.SetDestination(movePoints.Dequeue());
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
