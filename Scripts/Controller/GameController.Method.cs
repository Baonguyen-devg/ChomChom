using System;
using UnityEngine;

public partial class GameController : AutoMonoBehaviour, ISubject
{
    private void Update()
    {
        this.countDown = this.countDown - Time.deltaTime;
        if (this.countDown <= 0)
        {
            this.countDown = DEFAULT_RATE_TIME_INCREASE;
            this.CountDownEventHandler?.Invoke(null, null);
        }
    }

    public virtual void IncreaseCoin(int number)
    {
        this.numberCoin = this.numberCoin + number;
        UIController.Instance.ChangeCoinNumberText(this.numberCoin.ToString());
        SFXSpawner.Instance.PlaySound("Coin_Audio");
    }

    private void IncreaseSpeedGame(object sender, EventArgs e)
    {
        this.speedGame = Mathf.Min(DEFAULT_MAX_SPEED_GAME, this.speedGame + this.rateSpeedGame);
        if (this.speedGame == DEFAULT_MAX_SPEED_GAME) return;
        this.Notify();
    }

    public virtual void GameLose()
    {
        Time.timeScale = 0;
        UIController.Instance.OnGameLosePanel();
        SFXSpawner.Instance.PlaySound("Game_Lose_Audio");
        SFXSpawner.Instance.StopBackGroundAudio();
    }

    public virtual void PauseGame()
    {
        Time.timeScale = 0;
        UIController.Instance.OnPauseGamePanel();
    }

    /// <summary>
    ///   <para>Attach function use for register subject of gamecontroller (Add a observer into list)</para>
    ///   <para>Detach function use for cancel register subject of gamecontroller (Remove a observer in list)</para>
    ///   <para>Notify function use for remind all observers when event happen </para>
    /// </summary>
    /// <param name="observer">The observer.</param>
    /// 
    public void Attach(IObserver observer) => this.observers.Add(observer);
    public void Detach(IObserver observer) => this.observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in this.observers)
            observer.UpdateObserver(this);
    }
}
