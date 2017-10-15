using UnityEngine;

public class UIAnimationDestroy : UIAnimation 
{
	public float Duration = 3.0f;

    private Vector3 _targetScale = Vector3.zero;
	private Vector3 _startingScale;

	public override void Play()
	{
        gameObject.SetActive(true);
        _startingScale = gameObject.transform.localScale;
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", _targetScale,
            "time", Duration,
            "oncompletetarget", gameObject,
            "oncomplete", "UIAnimationDestroyComplete"
        ));
	}

    private void UIAnimationDestroyComplete()
    {
        gameObject.SetActive(false);
        gameObject.transform.localScale = _startingScale;
        HandleOnComplete();
    }
}
