using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpdateManager : MonoBehaviour
{
    private LinkedList<IUpdate> _update;
    private bool _isLevelStart = false;

    [Inject] private EventManager _eventManager;

    private void Awake() => _update = new LinkedList<IUpdate>();
    private void Update()
    {
        if(_update.First == null || !_isLevelStart)
            return;

        foreach(var update in _update)
            update.CustomUpdate();
    }

    public void AddUpdate(IUpdate update) => _update.AddLast(update);
    public void RemoveUpdate(IUpdate update) => _update.Remove(update);

    private void IsLevelStart() => _isLevelStart = true;

    private void OnEnable()
    {
        _eventManager.onCustomUpdateAdd += AddUpdate;
        _eventManager.onCustomUpdateRemove += RemoveUpdate;
        _eventManager.onStartLevel += IsLevelStart;
    }

    private void OnDisable()
    {
        _eventManager.onCustomUpdateAdd -= AddUpdate;
        _eventManager.onCustomUpdateRemove -= RemoveUpdate;
        _eventManager.onStartLevel -= IsLevelStart;
    }
}
