using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class WobblyText : MonoBehaviour
{
    [SerializeField] private List<TextEffector> _textComponents = new List<TextEffector>();

    private void Update()
    {
        foreach (var text in _textComponents)
        {
            switch (text.TextEffect)
            {
                case ETextEffect.Wobbly:
                    Wobbly(text.TextComponent, text.WobblySetting, text.Multiply);
                    break;
                case ETextEffect.WobblyV2:
                    WobblyV2(text.TextComponent);
                    break;
                case ETextEffect.Shaking:
                    Shaking(text.TextComponent, text.WobblySetting, text.Multiply);
                    break;
                case ETextEffect.VertexWobbly:
                    VertexWobbly(text.TextComponent, text.WobblySetting);
                    break;
            }
        }
    }

    private void Wobbly(TMP_Text text, Vector2 waveSpeed, float multiply)
    {
        text.ForceMeshUpdate();
        var textInfo = text.textInfo;

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
                    origin + new Vector3(0, Mathf.Sin(Time.time * waveSpeed.y + origin.x * waveSpeed.x) * multiply, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            text.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    private void Shaking(TMP_Text text, Vector2 waveSpeed, float multiply)
    {
        text.ForceMeshUpdate();
        var textInfo = text.textInfo;

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
                int offset = 0;
                while (offset == 0)
                {
                    offset = Random.Range(-1, 2);
                }
                
                var origin = verts[characterInfo.vertexIndex + j];
                verts[characterInfo.vertexIndex + j] =
                    origin + new Vector3(Mathf.Sin(Time.time * waveSpeed.y * offset + origin.y * waveSpeed.x) * multiply,
                        Mathf.Sin(Time.time * waveSpeed.y * -offset + origin.y * waveSpeed.x) * multiply, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            text.UpdateGeometry(meshInfo.mesh, i);
        }
    }
    
    private void WobblyV2(TMP_Text text)
    {
        Mesh mesh;

        Vector3[] vertices;
        
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = text.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }

    private void VertexWobbly(TMP_Text text, Vector2 waveSpeed)
    {
        Mesh mesh;

        Vector3[] vertices;
        
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i + waveSpeed.y);

            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*3.3f), Mathf.Cos(time*2.5f));
    }
}

[Serializable]
public struct TextEffector
{
    public TMP_Text TextComponent;
    public ETextEffect TextEffect;
    public Vector2 WobblySetting;
    public float Multiply;
}

public enum ETextEffect
{
    Wobbly,
    WobblyV2,
    VertexWobbly,
    Shaking
}