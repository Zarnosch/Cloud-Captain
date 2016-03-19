using UnityEngine;
using System.Collections;

public class BumpManipulator : MonoBehaviour {
    public Material bump;
    Texture bumpTex;
    Texture2D changeTex;
    MeshRenderer meshRenderer;
    public Vector2 offset;
    float wave;
	// Use this for initialization
	void Start () {
        offset = new Vector2(0, 0);
        meshRenderer = GetComponent<MeshRenderer>();
        bumpTex = meshRenderer.material.GetTexture("_BumpMap");
        changeTex = (Texture2D) bumpTex;
        meshRenderer.sharedMaterial.SetTexture("_BumpMap", changeTex);
        wave = 0;
    }

    // Update is called once per frame
    void Update() {
        wave = wave + 0.00000009f;
        
        offset = offset + new Vector2(wave, 0f);
        meshRenderer.sharedMaterial.mainTextureOffset = offset;
    }
}
