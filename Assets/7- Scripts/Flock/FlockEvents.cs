using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockEvents : Flock
{
    public UnityEvent OnUpdate;

    private void Start()
    {
        if (OnUpdate == null) OnUpdate = new UnityEvent();
    }

    private void Update()
    {
        OnUpdate.Invoke();
    }
}
