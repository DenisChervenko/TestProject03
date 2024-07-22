using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveLimit;

    [SerializeField] private Transform _player;
    [SerializeField] private Slider _sliderController;

    private float _startPositionX;

    private CharacterController _characterController;
    private Animator _animator;

    [Inject] private EventManager _eventManager;

    private void Awake()
    {
        _characterController = _player.GetComponent<CharacterController>();
        _startPositionX = _player.position.x;

        _animator = _player.GetComponent<Animator>();
    }

    public void ChangeMovement()
    {
        float newPosition = _startPositionX + _sliderController.value * _moveLimit;

        Vector3 localDirection = new Vector3(newPosition - _player.localPosition.x, 0, 0);
        Vector3 globalDirection = _player.parent.TransformDirection(localDirection);

        _characterController.Move(globalDirection);
        _player.localPosition = new Vector3(_player.localPosition.x, 0, 0);
    }

    private void ChangeAnimation(string animation) => _animator.SetTrigger(animation);

    private void OnEnable() => _eventManager.onChangeAnimation += ChangeAnimation;
    private void OnDisable() => _eventManager.onChangeAnimation -= ChangeAnimation;
}
