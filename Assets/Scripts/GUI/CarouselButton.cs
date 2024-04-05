using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselButton : MonoBehaviour
{
    public RectTransform carouselContainer;
    public Image[] images;

    private int currentIndex = 0;

    void Start()
    {
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
  
    }

    public void ShiftLeft()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        images[currentIndex].gameObject.SetActive(true);
    }

    public void ShiftRight()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % images.Length;
        images[currentIndex].gameObject.SetActive(true);
    }
}
