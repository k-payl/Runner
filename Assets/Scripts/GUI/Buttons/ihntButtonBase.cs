using System.Linq;
using GamePlay;
using UnityEngine;
using System.Collections;



public class ihntButtonBase : MonoBehaviour
{
	public dfProgressBar loadingBar;
	public ihntPanelBase ToHidePanel;
	public ihntPanelBase ToShowPanel;
	protected ISwitchPanelBehaviour switchPanelBehaviour;

	protected virtual void Start()
	{
		if (loadingBar != null)
			loadingBar.Hide();
		switchPanelBehaviour = new Switchable(ToHidePanel, ToShowPanel);
	} 

	public virtual void OnClick(dfControl control, dfMouseEventArgs args)
	{
		switchPanelBehaviour.Switch();
	}
}
