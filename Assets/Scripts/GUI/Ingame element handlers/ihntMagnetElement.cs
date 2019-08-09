using GamePlay;
using LevelGeneration;
using UnityEngine;
using System.Collections;

public class ihntMagnetElement : ihntBonusElementAbstract {
    protected override void Awake()
    {
        base.Awake();
        dfControl magnetGUI = GetComponent<dfControl>();
        magnetGUI.Hide();
    }

    public override void HandleBonusCollected(Bonus bonus, BonusCollection collection)
    {
        dfControl magnetGUI = GetComponent<dfControl>();
        magnetGUI.Show();
       // Debug.Log("ihntMagnetElement.HandleBonusCollected(): handeled");
    }

    public override void HandleBonusMissed(Bonus bonus, BonusCollection collection)
    {
        dfControl magnetGUI = GetComponent<dfControl>();
        magnetGUI.Hide();
        //Debug.Log("ihntMagnetElement.HandleBonusMissed(): handeled");
    }

    public override void JustUpdate(BonusCollection collection)
    {
        //Debug.Log("ihntMagnetElement.JustUpdate(): handeled");
    }
}
