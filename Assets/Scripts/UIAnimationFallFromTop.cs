using UnityEngine;

public class UIAnimationFallFromTop : UIAnimation 
{
	public float Duration = 3.0f;

	private RectTransform _transform;
	private Vector3 _targetPosition;
    private Vector3 _offscreen;

    public override void Play()
    {
        _transform = GetComponent<RectTransform>();
		_targetPosition = _transform.anchoredPosition3D;
        _offscreen = new Vector3(_targetPosition.x, _targetPosition.y,  40.0f);
		iTween.ValueTo(_transform.gameObject, iTween.Hash(
            "from", _offscreen,
    	    "to", _targetPosition,
            "time", Duration,
    	    "onupdatetarget", this.gameObject,
			"onupdate", "MoveGuiElement",
            "oncomplete", "HandleOnComplete",
			"easetype", iTween.EaseType.easeInOutBack));
    }

	public void MoveGuiElement(Vector3 position)
	{
        _transform.anchoredPosition3D = position;
	}
}
