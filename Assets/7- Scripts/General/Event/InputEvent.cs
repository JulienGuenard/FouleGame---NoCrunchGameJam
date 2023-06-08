using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public UnityEvent OnLeftClick;

    private void Start()
    {
        if (OnLeftClick == null) OnLeftClick = new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) OnLeftClick.Invoke();
    }
}
