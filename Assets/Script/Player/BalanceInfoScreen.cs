using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class BalanceInfoScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _balanceInfoScreen;
    [SerializeField] private TMP_Text _textInfoPrefab;
    [SerializeField] private Transform _textContainer;
    [SerializeField] private Color _positiveColor = Color.green; 
    [SerializeField] private Color _negativeColor = Color.red;

    private Queue<TMP_Text> _textPool = new Queue<TMP_Text>();

    [Inject] private EventManager _eventManager;

    private void ShowInfo(string countMoney)
    {
        _balanceInfoScreen.DOFade(1, 0.1f);

        CancelInvoke(nameof(HideBalanceInfoScreen));
        Invoke(nameof(HideBalanceInfoScreen), 0.5f);

        TMP_Text textInfo = GetTextFromPool();
        textInfo.text = countMoney;
        textInfo.alpha = 1;
        textInfo.gameObject.SetActive(true);

        if (countMoney.StartsWith("+"))
            textInfo.color = _positiveColor;
        else
            textInfo.color = _negativeColor;

        TextAnimation(textInfo);
    }

    private void HideBalanceInfoScreen()
    {
        _balanceInfoScreen.DOFade(0, 0.1f);
    }

    private void TextAnimation(TMP_Text textInfo)
    {
        Vector3 startPosition = new Vector3(0, -1, 0);
        textInfo.rectTransform.localPosition = startPosition;

        Vector3 randomPosition = new Vector3(Random.Range(-0.6f, 0.6f), 0.5f, 0);
        textInfo.rectTransform.DOLocalMove(randomPosition, 0.3f).SetEase(Ease.OutCubic);
        textInfo.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => ReturnTextToPool(textInfo));
    }

    private TMP_Text GetTextFromPool()
    {
        if (_textPool.Count > 0)
        {
            return _textPool.Dequeue();
        }

        return Instantiate(_textInfoPrefab, _textContainer);
    }

    private void ReturnTextToPool(TMP_Text textInfo)
    {
        textInfo.alpha = 0;
        textInfo.gameObject.SetActive(false);
        _textPool.Enqueue(textInfo);
    }

    private void OnEnable() => _eventManager.onBalanceChange += ShowInfo;
    private void OnDisable() => _eventManager.onBalanceChange -= ShowInfo;
}
