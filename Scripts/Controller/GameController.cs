using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController : AutoMonoBehaviour, ISubject
{
    private const float DEFAULT_SPEED_GAME = 1f;
    private const float DEFAULT_MAX_SPEED_GAME = 5f;

    private const float DEFAULT_RATE_SPEED_GAME = 0.1f;
    private const float DEFAULT_RATE_TIME_INCREASE = 20f;

    private static GameController instance;
    public static GameController Instance => instance;

    [Header("[ Speed Game ]"), Space(5)]
    [SerializeField] private float speedGame = DEFAULT_SPEED_GAME;
    public float SpeedGame => this.speedGame;

    [SerializeField] private float rateSpeedGame = DEFAULT_RATE_SPEED_GAME;
    [SerializeField] private float countDown = DEFAULT_RATE_TIME_INCREASE;

    [Header("[ Coin ]"), Space(5)]
    [SerializeField] private int numberCoint = default;
    public int NumberCoint => this.numberCoint;

    private List<IObserver> observers = new List<IObserver>();

    private event EventHandler CountDownEventHandler;

    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.CountDownEventHandler += this.IncreaseSpeedGame;
        GameController.instance = this;
    }
}
