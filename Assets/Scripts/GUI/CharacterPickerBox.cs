using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPickerBox : MonoBehaviour
{
    public TMP_Text textArea;
    public Image imageArea;
    public Sprite p1box;
    public Sprite p2box;

    private bool IsSandman;
    // Start is called before the first frame update
    void Start()
    {
        IsSandman = transform.GetChild(0).gameObject.activeInHierarchy;
        updateSelectBox(IsSandman);
    }

    public void updateSelectBox(bool val) {
        IsSandman = val;
        if (IsSandman) {
            //visible p1
            textArea.text = "Marchand de Sable";
            imageArea.sprite = p1box;
        } else {
            //visible p2
            textArea.text = "Toutou";
            imageArea.sprite = p2box;
        }
    }

}
