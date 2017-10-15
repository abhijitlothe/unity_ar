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
        if(OpenAnim != null)
        {
            OpenAnim.Play();
            OpenAnim.OnAnimComplete += (obj)=>
            {
                if(OnOpen !=null)
                {
                    OnOpen.Invoke();  
                }
                ScreenState = State.Open;
            };
        }
    }

    public virtual void Close()
	{
		if (CloseAnim != null)
		{
			CloseAnim.Play();
			CloseAnim.OnAnimComplete += (obj) =>
			{
                if(OnClose != null)
                {
                    OnClose.Invoke();  
                }
                ScreenState = State.Closed;
			};
		}
    }

}
