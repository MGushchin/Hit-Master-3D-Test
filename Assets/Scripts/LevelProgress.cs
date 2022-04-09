using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public Player Player;
    public List<EnemySpot> Spots = new List<EnemySpot>(); //Точки перемещение и спауна врагов
    private int currentMovepointIndex = 0; //Текущая область игрока
    private bool gameStarted = false; //Была ли начата игра касанием в начале
    public InputControl Control; //Управление игрока
    public LevelSetup Setup; //Установка противников по зонам
    public LevelRestart Restart; //Перезапуск систем и положений игрока и камеры

    private void Start()
    {
        Control.OnTap.AddListener(OnPlayerTap); //Подписка на обработку нажатий игрока
        currentMovepointIndex = 0;
        for (int i = 1; i < Spots.Count; i++) // Подписка на прохождение областей
        {
            Spots[i].OnCompleted.AddListener(OnPointCompletion);
        }
    }

    private void restart() //Перезапуск сцены
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Restart.Restart();
        currentMovepointIndex = 0;
        Player.Weapon.SetCanFire(false);
        gameStarted = false;
    }

    public void OnPlayerTap(Vector3 tapPosition)
    {
        if (!gameStarted) //Начинаем игру при касании
        {
            Setup.SetupEnemies(); //Расставляем противников
            currentMovepointIndex++;
            Queue<Vector3> movePositions = new Queue<Vector3>(); //Заполнение очереди движения персонажа игрока
            for (int i = 1; i < Spots.Count; i++)
                movePositions.Enqueue(Spots[i].transform.position);
            Player.StartMoving(movePositions); //Запускаем персонажа игрока по собранному маршруту
            Player.Weapon.SetCanFire(true); //Разрешаем стрельбу
            gameStarted = true;
        }
    }

    public void OnPointCompletion(EnemySpot completedPoint) //При завершении области
    {
        currentMovepointIndex++; //Повышаем индекс текущей области
        if (currentMovepointIndex == Spots.Count) //Если была пройдена последняя область, то перезапускаем сцену
            restart();
    }
}
