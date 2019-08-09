using System.Linq;
using UnityEngine;
using System.Collections;

public class ihntPanelBase : MonoBehaviour
{

	private dfPanel owner;
	private dfTweenPlayableBase hideTweensOwner;
	private dfTweenPlayableBase showTweensDestination;


	protected virtual void  Start ()
	{
		owner = GetComponent<dfPanel>();
		hideTweensOwner = GetComponents<dfTweenPlayableBase>().FirstOrDefault(i => i.TweenName == "Hide");
		showTweensDestination = GetComponents<dfTweenPlayableBase>().FirstOrDefault(i => i.TweenName == "Show");
	}

	[ContextMenu("Show")]
	public virtual void Show()
	{
		if (showTweensDestination != null)
			showTweensDestination.Play();
		else
		{
			owner.Show();
		}

		dfProgressBar bar = GetComponent<dfProgressBar>();
		if (bar == null) bar = GetComponentInChildren<dfProgressBar>();
		if (bar==null) return;

		bar.Hide();
		bar.Value = 0;
	}
	[ContextMenu("Hide")]
	public virtual void Hide()
	{
		if (hideTweensOwner != null)
			hideTweensOwner.Play();
		else
		{
			owner.Hide();
		}
	}
}
