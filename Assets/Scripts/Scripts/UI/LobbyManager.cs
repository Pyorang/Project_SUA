using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
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
}
