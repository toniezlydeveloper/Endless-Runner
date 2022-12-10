using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{
    public static event Action<UIAction> OnActionRequired;

    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() => OnActionRequired?.Invoke(UIAction.Restart));
    }

    private void Start()
    {
        AddCallbacks();
        Disable();
    }

    private void OnDestroy()
    {
        RemoveCallbacks();
    }

    private void AddCallbacks() => GameController.OnLost += Enable;
    
    private void RemoveCallbacks() => GameController.OnLost -= Enable;

    private void Enable() => gameObject.SetActive(true);
    
    private void Disable() => gameObject.SetActive(false);
}