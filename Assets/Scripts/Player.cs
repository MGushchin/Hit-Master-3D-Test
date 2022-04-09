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

    public void StartMoving(Queue<Vector3> positions) //������ ������ �� ������ �����������
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

    public void AreaCleared(EnemySpot completedPoint) //����� ������������� �� ������� ����������� ������� �� ���������� ������� � ��� ������������ ���������� ����
    {
        completedPoint.OnCompleted.RemoveListener(AreaCleared);
        movingCoroutine = moving();
        StartCoroutine(movingCoroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemySpot>() != null)
        {
            if (other.gameObject.GetComponent<EnemySpot>().EnemyCount > 0) //���� ����������� ������� �� ������ �������, �� ������������� �� ������� �� ����������� � ���������������
            {
                Stop();
                other.gameObject.GetComponent<EnemySpot>().OnCompleted.AddListener(AreaCleared);
            }
        }
    }

    private IEnumerator moving()
    {
        if (movePoints.Count > 0) //��������� ������ ���� ������� ����� ����������� �� �����
        {
            Agent.SetDestination(movePoints.Peek());
            StateChanger.SetState(State.Run);
            while (movePoints.Count > 0)
            {
                if ((Agent.remainingDistance <= Agent.stoppingDistance) && Agent.remainingDistance > 0.1f) //������ ������� ��� ���������� ������ ������������
                {
                    Agent.SetDestination(movePoints.Dequeue());
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
