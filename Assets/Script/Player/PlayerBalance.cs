using TMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerBalance : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectPositiveEffect;
    [SerializeField] private ParticleSystem _collectNegativeEffect;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _moneyForPoor;
    
    [SerializeField] private int _maxBalance;
    [SerializeField] private int _balance;
    private int _previouseBalance;

    [SerializeField] private Image _fillAmount;
    [SerializeField] private TMP_Text _currentBalance;

    [Inject] private EventManager _eventManager;

    private void Start()
    {
         _fillAmount.fillAmount = (float)_balance / _maxBalance;
        _currentBalance.text = _balance.ToString();
    } 
    
    private void ChangeReputation(string typeReputation)
    {
        int balanceModifier = 0;

        if(typeReputation == "Positive")
            balanceModifier += 2;
        else if(typeReputation == "Negative")
            balanceModifier -= 3;
        else if(typeReputation == "PositiveX2")
            balanceModifier += 20;
        else
            balanceModifier -= 10;

        _previouseBalance = _balance;
        _balance += balanceModifier;

        string modifier;
        StringBuilder sb = new StringBuilder();

        if(typeReputation == "Positive" || typeReputation == "PositiveX2")
        {
            sb.Append("+").Append(balanceModifier);
            _collectPositiveEffect.Play();
        }
        else
        {
            _collectNegativeEffect.Play();
            sb.Append("").Append(balanceModifier);
        }

        modifier = sb.ToString();
        _eventManager.onBalanceChange?.Invoke(modifier);

        UpdateValue();
    }

    private void UpdateValue()
    {
        _fillAmount.fillAmount = (float)_balance / _maxBalance;
        _currentBalance.text = _balance.ToString();

        if(_balance <= 0)
            _eventManager.onEndGame?.Invoke();

        int previousTier = _previouseBalance / _upgradeCost;
        int currentTier = _balance / _upgradeCost;
            
        if(currentTier > previousTier)
            _eventManager.onTierChange?.Invoke(true);
        else if(currentTier < previousTier)
            _eventManager.onTierChange?.Invoke(false);
    }

    private void OnEnable() => _eventManager.onCollectItem += ChangeReputation;
    private void OnDisable() => _eventManager.onCollectItem -= ChangeReputation;
}
