using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibilityManager : MonoBehaviour
{
    [SerializeField] private ColorSchemScriptableObject normalVisionCS;
    [SerializeField] private ColorSchemScriptableObject deutranopiaCS;
    [SerializeField] private ColorSchemScriptableObject protanopiaCS;
    [SerializeField] private ColorSchemScriptableObject tritanopiaCS;

    public ColorSchemScriptableObject chosenColorScheme;

    private void Start()
    {
        chosenColorScheme = normalVisionCS;
    }

    public void ToDeuteranopia()
    {
        chosenColorScheme = deutranopiaCS;
    }

    public void ToProtanopia()
    {
        chosenColorScheme = protanopiaCS;
    }

    public void ToTritanopia()
    {
        chosenColorScheme = tritanopiaCS;
    }
}
