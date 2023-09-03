using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIController : AutoMonoBehaviour
{
    private static UIController instance;
    public static UIController Instance => instance;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private Text coinNumberText;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.coinNumberText = transform.Find("Game_Panel").Find("Coin").GetComponentInChildren<Text>()
        };
        foreach (var action in this.loadComponentActions)
            action?.Invoke();
    }

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        UIController.instance = this;
    }

    public virtual void ChangeCoinNumberText(string number) =>
        this.coinNumberText.text = number;
}
