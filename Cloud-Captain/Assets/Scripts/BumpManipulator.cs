using UnityEngine;
using System.Collections;
using System.IO;


public class BumpManipulator : MonoBehaviour {
    public Material Mat1, Mat2;
    public float BumpScale1, BumpScale2;
    Texture bumpTex;
    MeshRenderer meshRenderer;
    public Vector2 offset1, offset2;
    public float LerpTime;
    public bool Infade;
    public float LerpSpeed = 0.0001f;
    public float BumpScaleSpeed = 0.001f;
    public float MaxBumpScale = 0.8f;
    public float MinBumpScale = 0.1f;
    public float FastOff = 0.00005f;
    public float SlowOff = 0.00005f;
    // Use this for initialization
    void Start () {
        LerpTime = 0;
        offset1 = new Vector2(0, 0);
        meshRenderer = GetComponent<MeshRenderer>();
        Mat1 = meshRenderer.material;
        Mat2 = Instantiate(Mat1);
        Infade = true;
        offset2 = new Vector2(0.0002f, 0f);
        //bumpTex = meshRenderer.material.GetTexture("_BumpMap");
    }

    // Update is called once per frame
    void Update()
    {

        if(BumpScale1 >= MaxBumpScale && Infade)
        {
            Infade = false;
        }
        else if(BumpScale1 <= MinBumpScale)
        {
            Infade = true;
        }

        if (Infade)
        {
            BumpScale1 += BumpScaleSpeed;
            offset1 = offset1 + new Vector2(SlowOff, 0f);
            offset2 = offset2 + new Vector2(FastOff, 0f);
            
        }
        else
        {
            BumpScale1 -= BumpScaleSpeed;
            offset1 = offset1 + new Vector2(SlowOff, 0f);
            offset2 = offset2 + new Vector2(FastOff, 0f);
        }
        //offset1 = offset1 + new Vector2(0.00005f, 0f);
        //offset2 = offset2 + new Vector2(0.00007f, 0f);
        //offset2 = new Vector2(0.0001f, 0f);
        Mat1.mainTextureOffset = offset1;
        Mat2.mainTextureOffset = offset2;


        if (Infade)
        {
            meshRenderer.material.Lerp(Mat1, Mat2, BumpScale1);
        }
        else
        {
            meshRenderer.material.Lerp(Mat2, Mat1, BumpScale1);
        }

        meshRenderer.material = Mat1;
        Mat1.SetFloat("_BumpScale", BumpScale1);
        Mat2.SetFloat("_BumpScale", MaxBumpScale-BumpScale1);
    }

    /*
    void calculateAllBumpTextures(Texture2D inputTex, int count)
    {
        changeTex[0] = inputTex;
        MaxRed = 0;
        for (int i = 0; i < inputTex.width; i++)
        {
            for (int j = 0; j < inputTex.height; j++)
            {
                if(changeTex[0].GetPixel(i,j).r > MaxRed)
                {
                    MaxRed = changeTex[0].GetPixel(i, j).r;
                }
            }
        }
        for (int c = 1; c < count; c++)
        {
            Texture2D tempTex = new Texture2D(inputTex.width, inputTex.height);
            tempTex.LoadRawTextureData(changeTex[c-1].GetRawTextureData());
            // Normal movement
            for (int i = 0; i < inputTex.width; i++)
            {
                for (int j = 0; j < inputTex.height; j++)
                {
                    if(changeTex[c - 1].GetPixel(i, j).r > 0.5)
                    {
                        if (i == 0)
                        {
                            Color tempColor = changeTex[c - 1].GetPixel(i, j);
                            tempTex.SetPixel(inputTex.width, j, tempColor);
                            tempTex.SetPixel(i, j, changeTex[c - 1].GetPixel(i + 1, j));
                        }
                        else if (i == inputTex.width)
                        {
                            Color tempColor = changeTex[c - 1].GetPixel(i, j);
                            tempTex.SetPixel(i - 1, j, tempColor);
                            tempTex.SetPixel(i, j, changeTex[c - 1].GetPixel(0, j));
                        }
                        else
                        {
                            Color tempColor = changeTex[c - 1].GetPixel(i, j);
                            tempTex.SetPixel(i - 1, j, tempColor);
                            tempTex.SetPixel(i, j, changeTex[c - 1].GetPixel(i + 1, j));
                        }
                    }              
                }
            }
            tempTex.Apply();
            changeTex[c] = tempTex;
            byte[] bytes = tempTex.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/Test/" + c +".png", bytes);
        }
        //Debug.Log(Application.dataPath);       
    }
    */
}
