using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = false;

        base.Init();
    }
}
