using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingUpdatesHandler : DefaultTrackableEventHandler 
{
    public Vuforia.TrackableBehaviour ImageTargetToTrack;

    protected override void Start()
	{
        mTrackableBehaviour = ImageTargetToTrack;
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
	}

    protected override void OnTrackingFound()
    {
        Debug.Log("OnTracking Started : " + ImageTargetToTrack.GetInstanceID());
        base.OnTrackingFound();
        gameObject.SetActive(true);
    }

	protected override void OnTrackingLost()
	{
        Debug.Log("OnTracking Lost : " + ImageTargetToTrack.GetInstanceID());
        base.OnTrackingLost();
		gameObject.SetActive(false);
	}

}
