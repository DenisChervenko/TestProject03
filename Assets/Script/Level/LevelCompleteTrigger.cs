using System;
using UnityEngine;
using Zenject;
public class LevelCompleteTrigger : MonoBehaviour
{
    [Inject] private EventManager _eventManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            _eventManager.onLevelComplete?.Invoke();
    }
}
