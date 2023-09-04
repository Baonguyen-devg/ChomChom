using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class UIController : AutoMonoBehaviour
{
    private static UIController instance;
    public static UIController Instance => instance;

    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private GameObject gameLosePanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private Text coinNumberText;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.coinNumberText = transform.Find("Game_Panel").Find("Coin").GetComponentInChildren<Text>(),
            () => this.gameLosePanel = transform.Find("Game_Lose_Panel").gameObject,
            () => this.pauseGamePanel = transform.Find("Pause_Game_Panel").gameObject
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

    public virtual void OnGameLosePanel() => this.gameLosePanel.SetActive(true);

    public virtual void OnPauseGamePanel() => this.pauseGamePanel.SetActive(true);

    public virtual void ContinueGame() => Time.timeScale = 1;

    public virtual void BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public virtual void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
