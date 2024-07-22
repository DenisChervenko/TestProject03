using UnityEngine;
using Zenject;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour, IUpdate
{
    [SerializeField] private Transform[] _targetMove;
    private int _indexTarget = 0;

    [SerializeField] private float _speedMovement;
    [SerializeField] private float _timeRotate;
    [Inject] private EventManager _eventManager;

    private void Start() => _eventManager.onCustomUpdateAdd?.Invoke(this);

    public void CustomUpdate()
    {
        if (_indexTarget >= _targetMove.Length)
            return;

        Transform target = _targetMove[_indexTarget];
        float step = _speedMovement * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            transform.position = target.position;

            _indexTarget++;
            if (_indexTarget >= _targetMove.Length)
                return;

            Vector3 targetLook = Quaternion.LookRotation(_targetMove[_indexTarget].position - transform.position).eulerAngles;
            transform.DORotate(targetLook, _timeRotate).OnComplete(() => _eventManager.onUpdatePosition?.Invoke());
        }
    }

    private void OnDisable() => _eventManager.onCustomUpdateRemove?.Invoke(this);
}
