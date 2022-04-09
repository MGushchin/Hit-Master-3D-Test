using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    public Player Player;
    public Transform PlayerCamera;
    public List<EnemySpot> Spots = new List<EnemySpot>();
    public EnemyFactory Factory;
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    private Vector3 cameraPosition;
    private Quaternion CameraStartRotation;

    private void Start() //Сохранение стартовых позиций камеры и игрока
    {
        playerStartPosition = Player.transform.position;
        cameraPosition = PlayerCamera.transform.position;
        playerStartRotation = Player.transform.rotation;
        CameraStartRotation = PlayerCamera.transform.rotation;
    }

    public void Restart() //Перезапуск систем и возвращение игрока и камеры в стартовое положение
    {
        foreach (EnemySpot spot in Spots)
            spot.Restart();
        Player.Stop();
        Factory.Restart();
        Player.Agent.Warp(playerStartPosition);
        PlayerCamera.transform.position = cameraPosition;
        Player.transform.rotation = playerStartRotation;
        PlayerCamera.transform.rotation = CameraStartRotation;
    }
}
