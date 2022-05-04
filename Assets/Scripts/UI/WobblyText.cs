using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    [SerializeField] private ETextEffect _textEffect = ETextEffect.Wobbly;
    
    [SerializeField] private TMP_Text _textComponent = null;

    [Header("WobblySettings")] [Space(15f)] [SerializeField]
    private Vector2 _waveSpeed = new Vector2(0.01f, 2f);

    [SerializeField] private float _multiply = 10f;

    private void Update()
    {
        switch (_textEffect)
        {
            case ETextEffect.Wobbly:
                Wobbly();
                break;
        }
    }

    private void Wobbly()
    {
        _textComponent.ForceMeshUpdate();
        var textInfo = _textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var characterInfo = textInfo.characterInfo[i];

            if (!characterInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var origin = verts[characterInfo.vertexIndex + j];
                verts[characterInfo.vertexIndex + j] =
                    origin + new Vector3(0, Mathf.Sin(Time.time * _waveSpeed.y + origin.x * _waveSpeed.x) * _multiply, 0);
            }
            
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            
            _textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

public enum ETextEffect
{
    Wobbly
}
