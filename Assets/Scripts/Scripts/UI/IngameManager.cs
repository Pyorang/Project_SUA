using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : SingletonBehaviour<IngameManager>
{
    public IngameUIController ingameUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;

        ingameUIController = FindAnyObjectByType<IngameUIController>();
        base.Init();
    }
}
