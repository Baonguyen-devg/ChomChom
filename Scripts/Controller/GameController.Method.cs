using System;
using System.Collections;
using System.Collections.Generic;
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
        this.numberCoint = this.numberCoint + number;
    }

    private void IncreaseSpeedGame(object sender, EventArgs e)
    {
        this.speedGame = Mathf.Min(DEFAULT_MAX_SPEED_GAME, this.speedGame + this.rateSpeedGame);
        this.Notify();
    }

    public void Attach(IObserver observer) => this.observers.Add(observer);

    public void Detach(IObserver observer) => this.observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in this.observers)
            observer.UpdateObserver(this);
    }
}
