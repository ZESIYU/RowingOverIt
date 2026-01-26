using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    void PlayHoverSound()
    {
        if (UIAudioManager.Instance != null)
            UIAudioManager.Instance.PlayHover();
    }

    void PlayClickSound()
    {
        if (UIAudioManager.Instance != null)
            UIAudioManager.Instance.PlayClick();
    }
}
