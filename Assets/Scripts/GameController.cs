using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static event Action OnLost;
    
    [SerializeField] private Transform player;

    private static readonly Vector3 HalfCollisionRange = new(0.5f, 0.0625f, 0.5f);
    
    private void Start()
    {
        PauseTime();
    }

    private void OnEnable()
    {
        RestartPanel.OnActionRequired += PerformAction;
        MenuPanel.OnActionRequired += PerformAction;
    }

    private void OnDisable()
    {
        RestartPanel.OnActionRequired -= PerformAction;
        MenuPanel.OnActionRequired -= PerformAction;
    }

    private void Update()
    {
        if (!HitObstacle())
        {
            return;
        }
        
        NotifyAboutLosing();
        PauseTime();
    }

    private void PerformAction(UIAction action)
    {
        switch (action)
        {
            case UIAction.Restart:
                Restart();
                break;
            case UIAction.Play:
                ResumeTime();
                break;
            case UIAction.Quit:
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    // ReSharper disable once Unity.PreferNonAllocApi
    private bool HitObstacle() => Physics.OverlapBox(player.position, HalfCollisionRange).Length > 1;

    private void NotifyAboutLosing() => OnLost?.Invoke();
    
    private void Restart()
    {
        ReloadScene();
        ResumeTime();
    }

    private void PauseTime() => Time.timeScale = 0f;

    private void ResumeTime() => Time.timeScale = 1f;

    private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}