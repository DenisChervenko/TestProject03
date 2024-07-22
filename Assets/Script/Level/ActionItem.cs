using UnityEngine;
using Zenject;
public class ActionItem : MonoBehaviour
{
    [SerializeField] private GameObject _collectableItem;

    [SerializeField] private string _typeReputation;
    [Inject] private EventManager _eventManager;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _eventManager.onCollectItem?.Invoke(_typeReputation);
            _collectableItem.SetActive(false);
        }
    }
}
