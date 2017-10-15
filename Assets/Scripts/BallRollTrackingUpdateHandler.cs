using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BallRollTrackingUpdateHandler : DefaultTrackableEventHandler
{
    public TrackableBehaviour ImageTargetToTrack;
	protected override void Start()
	{
		mTrackableBehaviour = ImageTargetToTrack.GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
	}

	protected override void OnTrackingFound()
    {
        gameObject.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        gameObject.SetActive(false);
    }
}
