using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    [SerializeField] private CanvasGroup _endGameCanvas;
    [SerializeField] private CanvasGroup _levelCompleteCanvas;

    [Inject] private EventManager _eventManager;

    public void OnRestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void EndGame()
    {
        _endGameCanvas.interactable = true;
        _endGameCanvas.blocksRaycasts = true;
        _endGameCanvas.DOFade(1, 0.2f).OnComplete(() => Time.timeScale = 0);
    }

    private void LevelComplete()
    {
        _levelCompleteCanvas.interactable = true;
        _levelCompleteCanvas.blocksRaycasts = true;
        _levelCompleteCanvas.DOFade(1, 0.2f);

        _eventManager.onChangeAnimation?.Invoke("Victory");
    }

    private void OnEnable()
    {
        _eventManager.onEndGame += EndGame;
        _eventManager.onLevelComplete += LevelComplete;
    } 
    private void OnDisable()
    {
        _eventManager.onEndGame -= EndGame;
        _eventManager.onLevelComplete -= LevelComplete;
    } 
}
