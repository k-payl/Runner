using GamePlay;
using LevelGeneration;
using UnityEngine;
using System.Collections;

public class ihntCoinElement : ihntBonusElementAbstract
{

    public dfLabel countOfCoins;

    public override void HandleBonusCollected(Bonus bonus, BonusCollection collection)
    {
        if (countOfCoins == null) Debug.Log("countOfCoins==null in"+gameObject.name);
        else
            countOfCoins.Text = GetCorrectText(collection.score);
        //Debug.Log("ihntCoinElement.HandleBonusCollected(): handeled");
    }

    public override void HandleBonusMissed(Bonus bonus, BonusCollection collection)
    {
        if (countOfCoins == null) Debug.Log("countOfCoins==null in"+gameObject.name);
        else
            countOfCoins.Text = GetCorrectText(collection.score);
        //Debug.Log("ihntCoinElement.HandleBonusMissed(): handeled");
    }

    public override void JustUpdate(BonusCollection collection)
    {
        if (countOfCoins == null) Debug.Log("countOfCoins==null in"+gameObject.name);
        else 
        countOfCoins.Text = GetCorrectText(collection.score);
        //Debug.Log("ihntCoinElement.JustUpdate(): handeled");
    }

    protected string GetCorrectText(int score)
    {
        string text = score.ToString();
        if (score < 10)
            text = "0000" + text;
        else if (score >= 10 && score < 100)
            text = "000" + text;
        else if (score >= 100 && score < 1000)
            text = "00" + text;
        else if (score >= 1000 && score < 10000)
            text = "0" + text;
        return text; 
    }

}
