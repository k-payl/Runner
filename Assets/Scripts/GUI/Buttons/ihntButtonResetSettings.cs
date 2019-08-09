using UnityEngine;
using System.Collections;
using GamePlay;

public class ihntButtonResetSettings : ihntButtonBase {

    protected override void Start()
    {
        switchPanelBehaviour = new NoSwitchable();
    }

    public virtual void OnClick(dfControl control, dfMouseEventArgs args)
    {
        GameManager.Instance.info.Reset(); 
    }
}
