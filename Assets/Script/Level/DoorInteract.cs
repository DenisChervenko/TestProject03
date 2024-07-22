using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private AudioSource _openDoor;
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Open");
            _openDoor.Play();
        }
    }
}
