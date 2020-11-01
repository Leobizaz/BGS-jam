﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<float> onCollectCargo;
    public event Action<float> onLoseCargo;
    public event Action<string> onCollectTreco;
    public event Action<int> onCollectWrong;

    public void collectWrong(int index)
    {
        if(onCollectWrong != null)
        {
            onCollectWrong(index);
        }
    }

    public void collectTreco(string tipo)
    {
        if(onCollectTreco != null)
        {
            onCollectTreco(tipo);
        }
    }

    public void loseCargo(float mass)
    {
        if(onLoseCargo != null)
        {
            onLoseCargo(mass);
        }
    }
    public void collectCargo(float mass)
    {
        if(onCollectCargo != null)
        {
            onCollectCargo(mass);
        }
    }
}