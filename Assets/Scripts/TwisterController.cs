using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwisterController : MonoBehaviour
{
    public float moveTimeLength = 1.0f;
    public float waitTime = 4.0f;
    public float disTime = 1.0f;
    public Vector3 targetDestination;
    Vector3 initPosition;
    TextMeshPro mTextMesh;
    void Start()
    {
        initPosition = transform.position;
        mTextMesh = GetComponent<TextMeshPro>();
        StartCoroutine(Appear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Appear()
    {
        float timer = 0;
        while (timer < moveTimeLength)
        {
            transform.position = Vector3.Lerp(initPosition, targetDestination, timer / moveTimeLength);
            Color newC = mTextMesh.color;
            newC.a = Mathf.Lerp(0, 1, timer / moveTimeLength);
            mTextMesh.color = newC;
            yield return null;
            timer += Time.deltaTime;
        }
        yield return new WaitForSeconds(waitTime);
        timer = 0;
        while (timer < disTime)
        {
            transform.position = Vector3.Lerp(targetDestination, initPosition, timer / moveTimeLength);
            Color newC = mTextMesh.color;
            newC.a = Mathf.Lerp(1, 0, timer / moveTimeLength);
            mTextMesh.color = newC;
            yield return null;
            timer += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
