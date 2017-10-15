using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationScaleOpen : UIAnimation 
{
    public float Duration = 3.0f;

    private Vector3 _targetScale;

    public override void Play()
    {
        _targetScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(0, _targetScale.y, _targetScale.z);
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", _targetScale,
            "time", Duration,
            "oncompletetarget", gameObject,
            "eastype", iTween.EaseType.easeInCirc,
            "oncomplete", "UIAnimationScaleOpenComplete"
        ));
    }

    private void UIAnimationScaleOpenComplete()
    {
        gameObject.transform.localScale = _targetScale;
        HandleOnComplete();
    }
}
