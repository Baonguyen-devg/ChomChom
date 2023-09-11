using System;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController : AutoMonoBehaviour, ISubject
{
    private const float DEFAULT_SPEED_GAME = 1f;
    private const float DEFAULT_MAX_SPEED_GAME = 1.36f;
    private const float DEFAULT_RATE_SPEED_GAME = 0.03f;
    private const float DEFAULT_RATE_TIME_INCREASE = 20f;

    private static GameController instance;
    public static GameController Instance => instance;

    [Header("[ Speed Game ]"), Space(5)]
    [SerializeField] private float speedGame = DEFAULT_SPEED_GAME;
    public float SpeedGame => this.speedGame;

    [SerializeField] private float rateSpeedGame = DEFAULT_RATE_SPEED_GAME;
    [SerializeField] private float countDown = DEFAULT_RATE_TIME_INCREASE;

    [Header("[ Coin ]"), Space(5)]
    [SerializeField] private int numberCoin = default;
    public int NumberCoin => this.numberCoin;

    private List<IObserver> observers = new List<IObserver>();
    private event EventHandler CountDownEventHandler;

    /// <summary>
    ///   <para>Load game controller singleton</para>
    ///   <para>add functions increase speed game into event handler</para>
    /// </summary>
    /// 
    protected override void LoadComponentInAwakeBefore()
    {
        base.LoadComponentInAwakeBefore();
        this.CountDownEventHandler += this.IncreaseSpeedGame;
        GameController.instance = this;
        Application.targetFrameRate = 60;
    }

    protected override void Start() => SFXSpawner.Instance.PlayBackgroundAudio();
}
