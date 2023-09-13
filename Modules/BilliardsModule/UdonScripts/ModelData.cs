﻿
using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
public class ModelData : UdonSharpBehaviour
{
    [Header("Table Visuals")]
    [System.NonSerialized] public Animator tableAnimator;
    [Header("Animator trigger names")]
    public string ballPocketedTriggerName = "BallPocketed";
    public string indicateSuccessTriggerName = "IndicateSuccess";
    public string flashTableColorTriggerName = "FlashTableColor";
    public string flashTableLightTriggerName = "FlashTableLight";
    public string flashTableErrorTriggerName = "FlashTableError";
    public string onGameStartedTriggerName = "OnGameStarted";
    public string onGameEndedTriggerName = "OnGameEnded";
    public string setGameModeTriggerName = "SetGameMode";
    public string setTableTriggerName = "SetTable";
    public string gameModeIntName = "GameMode";

    [Header("Table Properties")]
    [SerializeField] public float tableWidth = 1.06f;
    [SerializeField] public float tableHeight = 0.603f;
    [SerializeField] public float pocketRadius = 0.1f;
    [SerializeField] public float cushionRadius = 0.043f;
    [SerializeField] public float innerRadius = 0.072f;
    [SerializeField] public float facingAngleCorner = 133f; // corner pocket facing angle
    [SerializeField] public float facingAngleSide = 104.93142f; // corner pocket facing angle
    [SerializeField] public float ballRadius = 0.03f;
    [SerializeField] public float ballMass = 0.16f;
    [SerializeField] public float rackTrianglePosition = 0.5334f;

    [SerializeField] public Vector3 cornerPocket = new Vector3(11.087f, 0, 10.63f); // k_vE
    [SerializeField] public Vector3 sidePocket = new Vector3(0, 0, 10.662f); // k_vF

    [SerializeField] public GameObject[] pockets;
    [Header("Snooker:")]
    [SerializeField] public float baulkLine = 0.7367f;
    [SerializeField] public float blackSpotFromR = 0.324f;
    [SerializeField] public float semiCircleRadius = 0.2921f;

    public void _Init()
    {
        tableAnimator = GetComponent<Animator>();
    }
    public void onBallPocketed()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(ballPocketedTriggerName); }
    }
    public void _indicateSuccess()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(indicateSuccessTriggerName); }
    }
    public void _flashTableColor(Color color)
    {
        if (tableAnimator) { tableAnimator.SetTrigger(flashTableColorTriggerName); }
    }
    public void _flashTableLight()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(flashTableLightTriggerName); }
    }
    public void _flashTableError()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(flashTableErrorTriggerName); }
    }
    public void _OnGameStarted()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(onGameStartedTriggerName); }
    }
    public void _OnGameEnded()
    {
        if (tableAnimator) { tableAnimator.SetTrigger(onGameEndedTriggerName); }
    }
    public void _setGameMode(uint mode)
    {
        if (tableAnimator) { tableAnimator.SetTrigger(setGameModeTriggerName); }
        if (tableAnimator) { tableAnimator.SetInteger(gameModeIntName, (int)mode); }
    }
    //everything needs to be re-set in _setTable because disabling the table resets the animator.
    public void _setTable(uint mode)
    {
        if (tableAnimator) { tableAnimator.SetTrigger(setTableTriggerName); }
        if (tableAnimator) { tableAnimator.SetInteger(gameModeIntName, (int)mode); }
    }
}
