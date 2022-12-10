using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public static event Action<UIAction> OnActionRequired;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(() => OnActionRequired?.Invoke(UIAction.Quit));
        playButton.onClick.AddListener(() => OnActionRequired?.Invoke(UIAction.Play));
        playButton.onClick.AddListener(DestroySelf);
    }

    private void DestroySelf() => Destroy(gameObject);
}