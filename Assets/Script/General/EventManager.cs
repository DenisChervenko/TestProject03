using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public delegate void OnCustomUpdate(IUpdate update);
    public OnCustomUpdate onCustomUpdateAdd;
    public OnCustomUpdate onCustomUpdateRemove;

    public delegate void OnChangeAnimation(string animation);
    public OnChangeAnimation onChangeAnimation;

    public delegate void OnCollectItem(string objectType);
    public OnCollectItem onCollectItem;

    public delegate void OnTierChange(bool isUpgrade);
    public OnTierChange onTierChange;

    public delegate void OnBalanceChange(string balance);
    public OnBalanceChange onBalanceChange;

    public delegate int OnTakeStatus();
    public OnTakeStatus onTakeStatus;

    public UnityAction onPlayerUpgrade;

    public UnityAction onStartLevel;
    public UnityAction onUpdatePosition;

    public UnityAction onEndGame;
    public UnityAction onLevelComplete;

    public UnityAction onFlagEntered;
}
