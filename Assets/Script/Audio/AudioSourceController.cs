using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Collections;

public class AudioSourceController : MonoBehaviour
{
    [SerializeField] private AudioSource _victorySound;
    [SerializeField] private AudioSource _flagEntered;

    [SerializeField] private AudioSource _addedBalance;
    [SerializeField] private AudioSource _removedBalance;
    [SerializeField] private AudioSource[] _steps;

    [SerializeField] private AudioSource _upgradeSound;

    [Inject] private EventManager _eventManager;

    private Queue<AudioSource> _audioPool = new Queue<AudioSource>();
    [SerializeField] private int _poolMoneySize; 

    private void Awake()
    {
        for (int i = 0; i < _poolMoneySize; i++)
        {
            AudioSource audioSource = Instantiate(_addedBalance);
            audioSource.gameObject.SetActive(false);
            _audioPool.Enqueue(audioSource);
        }
    }

    private void StartStep() => _steps[1].enabled = true;

    private void StopStep()
    {
        foreach (var step in _steps)
            step.enabled = false;
    }

    private void UpgradePlayer() => _upgradeSound.Play();
    private void FlagEntered() => _flagEntered.Play();
    private void LevelComplete() => _victorySound.Play();

    private void BalanceChange(string modifier)
    {
        AudioSource audio = GetAudioFromPool();
        if (modifier.StartsWith("+"))
            audio.clip = _addedBalance.clip;
        else
            audio.clip = _removedBalance.clip;

        audio.gameObject.SetActive(true);
        audio.Play();
        StartCoroutine(ReturnAudioToPool(audio, audio.clip.length));
    }

    private AudioSource GetAudioFromPool()
    {
        if (_audioPool.Count > 0)
            return _audioPool.Dequeue();

        AudioSource newAudio = Instantiate(_addedBalance);
        newAudio.gameObject.SetActive(false);
        return newAudio;
    }

    private IEnumerator ReturnAudioToPool(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);

        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
        _audioPool.Enqueue(audioSource);
    }

    private void OnEnable()
    {
        _eventManager.onStartLevel += StartStep;
        _eventManager.onLevelComplete += StopStep;
        _eventManager.onLevelComplete += LevelComplete;
        _eventManager.onEndGame += StopStep;
        _eventManager.onBalanceChange += BalanceChange;
        _eventManager.onFlagEntered += FlagEntered;
        _eventManager.onPlayerUpgrade += UpgradePlayer;
    }

    private void OnDisable()
    {
        _eventManager.onStartLevel -= StartStep;
        _eventManager.onLevelComplete -= StopStep;
        _eventManager.onEndGame -= StopStep;
        _eventManager.onBalanceChange -= BalanceChange;
        _eventManager.onFlagEntered -= FlagEntered;
        _eventManager.onPlayerUpgrade -= UpgradePlayer;
    }
}
