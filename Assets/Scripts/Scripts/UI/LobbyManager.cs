using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    [SerializeField] private GameObject sedanCar;
    public LobbyUIController LobbyUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        LobbyUIController = FindAnyObjectByType<LobbyUIController>();

        if(LobbyUIController != null)
        {
            AudioManager.Instance.Play(AudioType.BGM, "Lobby");
            return;
        }

        LobbyUIController.Init();
    }

    private void Update()
    {
        sedanCar.transform.Rotate(Vector3.up, 10f * Time.deltaTime);
    }
}
