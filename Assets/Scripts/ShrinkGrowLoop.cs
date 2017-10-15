using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkGrowLoop : MonoBehaviour 
{
    public float AnimTime = 2.5f;
    public float ShrinkByPercent = 10.0f;
    Vector3 originalScale;
    float time = 0.0f;
    int dir = -1;
    Vector3 rate;

    void Start()
    {
        originalScale = transform.localScale;
        ScaleDown();
    }

	// Use this for initialization
	void _Start () 
    {
        originalScale = transform.localScale;
        Debug.LogFormat("original scale {0} {1} {2}", originalScale.x, originalScale.y, originalScale.z);
        AnimTime = Mathf.Max(0.001f, AnimTime);
        rate = (transform.localScale - (transform.localScale * ShrinkByPercent))/ AnimTime;
        rate.z = 0;
	}

    private void Update()
    {
        time += Time.deltaTime;
        if(time > AnimTime)
        {
            dir *= -1;
            time = 0;  
        }
        transform.localScale = (transform.localScale + (rate * Time.deltaTime * dir));
    }

    void ScaleDown()
    {
		float scale = (100 - ShrinkByPercent) / 100.0f;
		Hashtable hash = new Hashtable();
        hash.Add("x", originalScale.x * scale);
        hash.Add("y", originalScale.y * scale);

		hash.Add("time", AnimTime);
		hash.Add("oncomplete", "ScaleUp");
        hash.Add("easetype", iTween.EaseType.easeOutSine);
		iTween.ScaleTo(gameObject, hash);

	}
	
    void ScaleUp()
	{
		Hashtable hash = new Hashtable();
        hash.Add("x", originalScale.x);
        hash.Add("y", originalScale.y);
		hash.Add("time", AnimTime);
		hash.Add("oncomplete", "ScaleDown");
        hash.Add("easetype", iTween.EaseType.easeInSine);
		iTween.ScaleTo(gameObject, hash);
	}
}
