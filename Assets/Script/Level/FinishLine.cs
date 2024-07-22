using UnityEngine;
using Zenject;
public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject[] _gate;
    private int _status;

    [Inject] private EventManager _eventManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _status = _eventManager.onTakeStatus.Invoke();
            _eventManager.onFlagEntered?.Invoke();

            if(_status >= _gate.Length)
                _status = _gate.Length;

            Debug.Log(_status);

            for(int i = 0; i < _status; i++)
                _gate[i].SetActive(true);
        }
    }
}
