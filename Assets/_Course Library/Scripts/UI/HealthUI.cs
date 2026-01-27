using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    [Header("Heart Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Hearts")]
    public List<Image> hearts; // 手动拖入所有爱心 Image

    // ======================
    // 初始化显示
    // ======================
    public void Init(int maxHP, int currentHP)
    {
        UpdateHP(currentHP);
    }

    public void UpdateHP(int currentHP)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHP)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void Show()
    {
        foreach (var heart in hearts)
            heart.gameObject.SetActive(true);
    }

    public void Hide()
    {
        foreach (var heart in hearts)
            heart.gameObject.SetActive(false);
    }
}
