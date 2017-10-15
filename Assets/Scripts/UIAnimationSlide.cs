using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationSlide : UIAnimation 
{
    public float Duration = 1.0f;
    public RectTransform Target;
    public bool SlideOut = true;

	private RectTransform _transform;
	private Vector3 _finalPosition;
	private Vector3 _offscreen;

	public override void Play()
	{
		_transform = GetComponent<RectTransform>();
        _transform.gameObject.SetActive(true);
        _finalPosition = _transform.anchoredPosition3D;
        if(SlideOut)
        {
            //start  behind the target
            _offscreen = Target.anchoredPosition3D;
            _offscreen.z += 0.05f;
        }
        else
        {
            //start from current position
            _offscreen = _transform.anchoredPosition3D;
            //go to target position
            _finalPosition = Target.anchoredPosition3D;
            //go behind the target
            _finalPosition.z += 0.5f;
        }

		iTween.ValueTo(_transform.gameObject, iTween.Hash(
			"from", _offscreen,
			"to", _finalPosition,
			"time", Duration,
			"onupdatetarget", this.gameObject,
			"onupdate", "MoveGuiElement",
			"oncomplete", "HandleOnComplete"));
	}

	public void MoveGuiElement(Vector3 position)
	{
		_transform.anchoredPosition3D = position;
	}

}
