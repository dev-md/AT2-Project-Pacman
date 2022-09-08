//Dylan Mount
//08/09/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFleeSad : MonoBehaviour
{
    [SerializeField] private GameObject objRobot;
    private SkinnedMeshRenderer targetMesh;
    private Material targetMaterial;
    private MeshRenderer textMesh;
    private TextMesh textCom;
    private Color textColor;
    private Color defTextColor;

    private void Awake()
    {
        if(objRobot.TryGetComponent(out SkinnedMeshRenderer skinned))
        {
            targetMesh = skinned;
            targetMaterial = targetMesh.materials[0];
        }
        if(TryGetComponent(out MeshRenderer _textMesh))
        {
            textMesh = _textMesh;
        }
        if(TryGetComponent(out TextMesh _textCom))
        {
            textCom = _textCom;
            textColor = _textCom.color;
            defTextColor = _textCom.color;
        }
    }

    private void Update()
    {
        if(targetMesh.materials[0] != targetMaterial)
        {
            textMesh.enabled = true;
            float pertMeshTrans = GameManager.Instance.PowerUpTimer / 10f;
            float pertRevMeshTrans = (pertMeshTrans * -1f) + 1f;
            textColor.r = pertMeshTrans;
            textColor.g = pertRevMeshTrans;
            textColor.b = pertRevMeshTrans;
            textCom.color = textColor;
        }
        else if (targetMesh.materials[0] == targetMaterial)
        {
            textMesh.enabled = false;
            if(textCom.color != defTextColor)
            {
                textCom.color = defTextColor;
            }
        }
    }
}
