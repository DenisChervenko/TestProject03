using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Tutorial : MonoBehaviour, IDragHandler
{
    [SerializeField] private Transform _handOnTutorialScreen;

    [SerializeField] private CanvasGroup _startCanvas;
    [SerializeField] private CanvasGroup _controllerCanvas;
    [SerializeField] private CanvasGroup _levelCanvas;

    [SerializeField] private float _fadeDuration;

    private bool _alreadyStart = false;

    [Inject] private EventManager _eventManager;

    private void Start() => _handOnTutorialScreen.DOMoveX(
        _handOnTutorialScreen.position.x / 2, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    public void StartLevel()
    {
        if(_alreadyStart)
            return;

        _alreadyStart = true;
        _eventManager.onStartLevel?.Invoke();
        _eventManager.onChangeAnimation?.Invoke("Move");

        _startCanvas.DOFade(0, _fadeDuration);
        _startCanvas.interactable = false;
        _startCanvas.blocksRaycasts = false;
    }    
    public void OnDrag(PointerEventData eventData) => StartLevel();
}
