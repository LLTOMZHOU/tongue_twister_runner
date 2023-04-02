using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour
{
    public float BGInitialSpeed;
    private float BGSpeed;
    private Renderer mRenderer;
    private List<Material> backgrounds;
    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        BGSpeed = BGInitialSpeed;
        
    }
    // Update is called once per frame
    void Update()
    {
        mRenderer.material.mainTextureOffset += new Vector2(BGSpeed * Time.deltaTime, 0);
    }

    public void NextBG(int sceneIdx)
    {
        
    }

    IEnumerator ChangeBG(int sceneIdx)
    {
        float timer = 0.0f;
        while (timer < 3.0f)
        {
            mRenderer.material.Lerp(backgrounds[sceneIdx - 1], backgrounds[sceneIdx], timer / 3.0f);
            yield return null;
            timer += Time.deltaTime;
        }
    }


}
