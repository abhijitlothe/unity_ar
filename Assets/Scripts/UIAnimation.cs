using UnityEngine;


public abstract class UIAnimation : MonoBehaviour 
{
    public System.Action<object> OnAnimComplete;

	protected virtual void HandleOnComplete()
	{
		if (OnAnimComplete != null)
		{
			OnAnimComplete(this);
		}
	}

    public abstract void Play();
}
