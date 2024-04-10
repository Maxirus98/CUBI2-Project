using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarouselButton : MonoBehaviour
{
    public RectTransform carouselContainer;
    public TMP_Text counter;
    public Image[] images;
    public Button leftButton;
    public Button rightButton;

    private int currentIndex = 0;

    void Start()
    {
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        updateCounter();
        leftButton.onClick.AddListener(ShiftLeft);
        rightButton.onClick.AddListener(ShiftRight);
    }

    public void ShiftLeft()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        images[currentIndex].gameObject.SetActive(true);
        updateCounter();
    }

    public void ShiftRight()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % images.Length;
        images[currentIndex].gameObject.SetActive(true);
        updateCounter();
    }

    private void updateCounter()
    {
        counter.text = (currentIndex + 1).ToString() + "/" + (images.Length).ToString();
    }
}
