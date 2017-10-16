using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour
{
    public UIAnimation OpenAnim;
    public UIAnimation CloseAnim;

    public UnityEvent OnOpen = new UnityEvent();
    public UnityEvent OnClose = new UnityEvent();

    public enum State
    {
        Closed,
		Open
	}

    public State ScreenState{ get; private set; }

    protected virtual void Awake()
    {
        ScreenState = State.Closed;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        if(OpenAnim != null)
        {
            OpenAnim.Play();
            OpenAnim.OnAnimComplete = (obj) =>
            {
                if (OnOpen != null)
                {
                    OnOpen.Invoke();
                }
                ScreenState = State.Open;
                Debug.LogFormat("Screen {0} state is open", gameObject.name);
            };
        }
    }

    public virtual void Close()
	{
		if (CloseAnim != null)
		{
			CloseAnim.Play();
			CloseAnim.OnAnimComplete = (obj) =>
			{
                gameObject.SetActive(false);
                ScreenState = State.Closed;
                if(OnClose != null)
                {
                    OnClose.Invoke();  
                }
                Debug.LogFormat("Screen {0} state is closed", gameObject.name);
			};
		}
    }

}
