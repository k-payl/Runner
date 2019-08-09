using UnityEngine;
using System.Collections;

public class Switchable : ISwitchPanelBehaviour {

    public ihntPanelBase srcPanel;
    public ihntPanelBase destPanel;

    public Switchable(ihntPanelBase _srcPanel, ihntPanelBase _destPanel)
    {
        srcPanel = _srcPanel;
        destPanel = _destPanel;
    }

    public void Switch()
    {
        if (srcPanel != null)
        {
            srcPanel.Hide();
        }
        else
            Debug.Log("SwitchPanel.Switch(): srcPanel == null");

        if (destPanel != null)
        {
            destPanel.Show();
        }
        else
            Debug.Log("SwitchPanel.Switch(): destPanel == null");
    }
}
