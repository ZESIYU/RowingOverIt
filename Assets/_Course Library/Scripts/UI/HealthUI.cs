using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    [Header("Heart Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Heart Images")]
    public List<Image> hearts;

    public void Init(int maxHP, int currentHP)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].gameObject.SetActive(i < maxHP);
            hearts[i].sprite = i < currentHP ? fullHeart : emptyHeart;
        }
    }

    public void UpdateHP(int currentHP)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < currentHP ? fullHeart : emptyHeart;
        }
    }
}
