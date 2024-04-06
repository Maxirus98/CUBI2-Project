using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleViewCommands : MonoBehaviour
{
    public RectTransform commandsContainer;
    public Button controllerButton;
    public Button mouseButton;
    //0::controller, 1::mouse
    private int currentSelectedCmds = 0;
    // Start is called before the first frame update
    void Start()
    {
        ToggleCommandView(currentSelectedCmds);
        controllerButton.onClick.AddListener(() => ToggleCommandView(0));
        mouseButton.onClick.AddListener(() => ToggleCommandView(1));
    }

    void ToggleCommandView(int selected) 
    {
        currentSelectedCmds = selected;
        
        controllerButton.interactable = false;
        mouseButton.interactable = false;

        if (selected == 0) 
        {
            mouseButton.interactable = true;
        } 
        else 
        {
            controllerButton.interactable = true;
        }
        
        for (int i = 0; i< commandsContainer.childCount; i++) {
            commandsContainer.GetChild(i).gameObject.SetActive(i == selected);
        }
    }


}
