using UnityEngine;
using Zenject;
public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject[] _textUpgrade;
    [SerializeField] private GameObject[] _modelUpgrade;

    private int _currentIndexUpgrade = 1;

    [Inject] private EventManager _eventManager;

    private void Upgrade(bool isUpgrade)
    {
        if (_currentIndexUpgrade >= 0 && _currentIndexUpgrade < _modelUpgrade.Length)
            _modelUpgrade[_currentIndexUpgrade].SetActive(false);
            _textUpgrade[_currentIndexUpgrade >= 3 ? 3 : _currentIndexUpgrade].SetActive(false);

        if (isUpgrade)
            _currentIndexUpgrade++;
        else
            _currentIndexUpgrade--;

        if (_currentIndexUpgrade < 0)
            _currentIndexUpgrade = 0;
        else if (_currentIndexUpgrade >= _modelUpgrade.Length)
            _currentIndexUpgrade = _modelUpgrade.Length - 1;

        if (_currentIndexUpgrade >= 0 && _currentIndexUpgrade < _modelUpgrade.Length)
        {
            _modelUpgrade[_currentIndexUpgrade].SetActive(true);
            _textUpgrade[_currentIndexUpgrade >= 3 ? 3 : _currentIndexUpgrade].SetActive(true);
        }
        _eventManager.onPlayerUpgrade?.Invoke();
    }

    private int GiveStatus() => _currentIndexUpgrade;


    private void OnEnable()
    {
        _eventManager.onTierChange += Upgrade;
        _eventManager.onTakeStatus += GiveStatus;
    } 
    private void OnDisable()
    {
        _eventManager.onTierChange -= Upgrade;
        _eventManager.onTakeStatus -= GiveStatus;
    } 
}
