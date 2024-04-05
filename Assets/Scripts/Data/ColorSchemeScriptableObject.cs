using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "ScriptableObjects/ColorScheme", order = 1)]
public class ColorSchemScriptableObject : ScriptableObject
{
    public GameObject waterMonsterModel;
    public GameObject sandMonsterModel;
    public GameObject sandmanModel;
    public GameObject petModel;

    public Material waterMonsterMat;
    public Material sandMonsterMat;
    public Material sandmanMat;
    public Material petModelMat;
}
