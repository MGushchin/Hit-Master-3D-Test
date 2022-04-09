using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public Player Player;
    public List<EnemySpot> Spots = new List<EnemySpot>(); //����� ����������� � ������ ������
    private int currentMovepointIndex = 0; //������� ������� ������
    private bool gameStarted = false; //���� �� ������ ���� �������� � ������
    public InputControl Control; //���������� ������
    public LevelSetup Setup; //��������� ����������� �� �����
    public LevelRestart Restart; //���������� ������ � ��������� ������ � ������

    private void Start()
    {
        Control.OnTap.AddListener(OnPlayerTap); //�������� �� ��������� ������� ������
        currentMovepointIndex = 0;
        for (int i = 1; i < Spots.Count; i++) // �������� �� ����������� ��������
        {
            Spots[i].OnCompleted.AddListener(OnPointCompletion);
        }
    }

    private void restart() //���������� �����
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Restart.Restart();
        currentMovepointIndex = 0;
        Player.Weapon.SetCanFire(false);
        gameStarted = false;
    }

    public void OnPlayerTap(Vector3 tapPosition)
    {
        if (!gameStarted) //�������� ���� ��� �������
        {
            Setup.SetupEnemies(); //����������� �����������
            currentMovepointIndex++;
            Queue<Vector3> movePositions = new Queue<Vector3>(); //���������� ������� �������� ��������� ������
            for (int i = 1; i < Spots.Count; i++)
                movePositions.Enqueue(Spots[i].transform.position);
            Player.StartMoving(movePositions); //��������� ��������� ������ �� ���������� ��������
            Player.Weapon.SetCanFire(true); //��������� ��������
            gameStarted = true;
        }
    }

    public void OnPointCompletion(EnemySpot completedPoint) //��� ���������� �������
    {
        currentMovepointIndex++; //�������� ������ ������� �������
        if (currentMovepointIndex == Spots.Count) //���� ���� �������� ��������� �������, �� ������������� �����
            restart();
    }
}
