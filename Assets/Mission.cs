﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Runtime.CompilerServices;

public class Mission : MonoBehaviour
{
    public static Mission instance;

    private TimeSpan timePlaying;
    [SerializeField]private string timeCounter;
    private float elapsedTime;
    private bool isMissionActive;
    private EnemySpawner eSpawner;

    public CharacterObject mainChar;
    public TextMeshProUGUI missionStartText, menuTimer;
    public string missionText;
    public Vector2 midScreen, offScreen;
    public float missionStartSeconds = 6f;

    private MusicManager musicManager;
    public AudioClip stageTheme;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        eSpawner = GetComponentInChildren<EnemySpawner>();
        musicManager = GetComponentInChildren<MusicManager>();
        timeCounter = "Time: 00:00.00";
        isMissionActive = false;
    }
    private void FixedUpdate()
    {
        
    }
    public void StartMission()
    {
        StartCoroutine(MissionStart());
        eSpawner.SetPlayer();
    }
    public void EndMission()
    {
        EndTimer();
    }
    private void BeginTimer()
    {
        elapsedTime = 0f;
        isMissionActive = true;
        StartCoroutine(UpdateTimer());
    }
    private void EndTimer()
    {
        isMissionActive = false;
    }
    private IEnumerator UpdateTimer()
    {
        while (isMissionActive)
        {
            if (!PauseManager.IsGamePaused)
            {
                elapsedTime += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = /*"Time: " +*/ timePlaying.ToString("mm' : 'ss'.'ff");
                timeCounter = timePlayingStr;
                menuTimer.text = timeCounter;
            }
            yield return null;
        }
    }
    private IEnumerator MissionStart()
    {
        
        yield return new WaitForFixedUpdate();
        mainChar.controlType = CharacterObject.ControlType.OBJECT;
        missionStartText.rectTransform.DOAnchorPos(offScreen, 0);
        mainChar.QuickChangeForm(4);
        mainChar.SetState(37);
        yield return new WaitForSeconds(missionStartSeconds/2);
        missionStartText.DOColor(Color.white, 0.5f);
        missionStartText.rectTransform.DOAnchorPos(midScreen, 2);
        yield return new WaitForSeconds(missionStartSeconds/2);
        missionStartText.transform.DOScale(8, .25f);
        missionStartText.DOColor(Color.clear, 0.25f);
        mainChar.controlType = CharacterObject.ControlType.PLAYER;
        BeginTimer();
        musicManager.ChangeBGM(stageTheme);
    }
}
