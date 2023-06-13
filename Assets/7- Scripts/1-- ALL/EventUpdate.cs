using UnityEngine;
using UnityEngine.Events;

public class EventUpdate : MonoBehaviour
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
