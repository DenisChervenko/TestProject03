using UnityEngine;
using Zenject;
public class FlagZone : MonoBehaviour
{
    private Animator _animator;

    [Inject] private EventManager _eventManager;

    private void Start() => _animator = GetComponent<Animator>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _animator.SetTrigger("Active");
            _eventManager.onFlagEntered?.Invoke();
        }
    }
}
