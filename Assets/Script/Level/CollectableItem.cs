using UnityEngine;
using Zenject;
using DG.Tweening;
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Transform _collectableItem;
    [SerializeField] private float _timeRotate;

    [SerializeField] private string _typeReputation;
    [Inject] private EventManager _eventManager;

    private void Start() => _collectableItem.DORotate(new Vector3(0, 360f, 0), _timeRotate).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _eventManager.onCollectItem?.Invoke(_typeReputation);
            gameObject.SetActive(false);
        }
    }
}
