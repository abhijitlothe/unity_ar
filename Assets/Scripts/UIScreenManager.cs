using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;


public class UIScreenManager : MonoBehaviour, ITrackableEventHandler
{
    private UIScreen _prevScreen = null;
    private UIScreen _tempScreen = null;

    /// <summary>
    /// The initial screen.
    /// </summary>
    public UIScreen InitialScreen;

    /// <summary>
    /// The image target to track.
    /// </summary>
    public TrackableBehaviour ImageToTrack;

    public UnityEvent<UIScreen> OnScreenOpen;
    public UnityEvent<UIScreen> OnScreenClose;
    private List<UIScreen> _screens = new List<UIScreen>();

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Awake()
    {
        if (ImageToTrack)
            ImageToTrack.RegisterTrackableEventHandler(this);

    }

    private void OnDestroy()
    {
        if (ImageToTrack != null)
            ImageToTrack.UnregisterTrackableEventHandler(this);
    }

    private void Start()
    {
        if (InitialScreen != null)
        {

#if UNITY_EDITOR
            OpenScreen(InitialScreen);
#else
            _screens.Add(InitialScreen);
#endif
		}
    }

    /// <summary>
    /// Opens the screen.
    /// </summary>
    /// <param name="newScreen">New screen.</param>
    /// <param name="keepCurrent">If set to <c>true</c> keep current.</param>
    public void OpenScreen(UIScreen aScreen)
    {
        if (_screens.Find((screen)=> screen == aScreen) == null)
        {
            _screens.Add(aScreen);
        }

        if (aScreen.ScreenState == UIScreen.State.Closed)
        {
            Open(aScreen);
        }
	}

	/// <summary>
	/// Closes the currently open screen.
	/// </summary>
	/// <param name="newScreen">New screen.</param>
    public void CloseScreen(UIScreen aScreen)
	{
		if (_screens.Find((screen) => screen == aScreen) != null)
		{
            Debug.Log("Close");
			_screens.Remove(aScreen);
            Close(aScreen);
		}
		else
		{
			Debug.LogError("Screen already opened");
		}
	}

    private void Open(UIScreen aScreen)
    {
        _tempScreen = aScreen;
        _tempScreen.Open();
        _tempScreen.OnOpen.AddListener(HandleOpenNewScreen);
    }

	void HandleOpenNewScreen()
	{
        if(OnScreenOpen != null)
        {
            OnScreenOpen.Invoke(_tempScreen);    
        }
        _tempScreen = null;
	}

	private void Close(UIScreen aScreen)
	{
		_tempScreen = aScreen;
		_tempScreen.Close();
		_tempScreen.OnClose.AddListener(HandleCloseScreen);
	}

	void HandleCloseScreen()
	{
        if (OnScreenClose != null)
		{
			OnScreenClose.Invoke(_tempScreen);
		}
		_tempScreen = null;
	}



    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + ImageToTrack.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + ImageToTrack.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    private void OnTrackingLost()
    {
#if !UNITY_EDITOR
        SetVisibleAll(false);
#endif
    }

    private void OnTrackingFound()
    {
#if !UNITY_EDITOR
        SetVisibleAll(true);
#endif
	}

    private void SetVisibleAll(bool visible)
    {
        List<UIScreen> screensToOpen = new List<UIScreen>();
        _screens.ForEach((screen)=>
        {
            if(visible)
            {
				if (screen.ScreenState == UIScreen.State.Closed)
				{
                    screensToOpen.Add(screen);
				}
			}
            else
            {
                screen.gameObject.SetActive(visible);   
            }
        });

        screensToOpen.ForEach((screen)=>
        {
            OpenScreen(screen);
        });

    }
}
