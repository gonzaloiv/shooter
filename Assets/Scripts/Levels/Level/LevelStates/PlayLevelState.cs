﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelState : State {

  #region Fields

  [SerializeField] private GameObject pauseScreenPrefab;
  private GameObject pauseScreen;
  [SerializeField] private GameObject hudPrefab;
  private GameObject hud;

  private Level level;
  private GameData.Level levelData;
 
  private IEnumerator spawningRoutine;  

  #endregion

  #region Mono Behaviour

  void Awake() {
    pauseScreen = Instantiate(pauseScreenPrefab, transform);
    pauseScreen.SetActive(false);
 
    hud = Instantiate(hudPrefab, transform);
    hud.SetActive(false);

    level = GetComponent<Level>();
  }
 
  #endregion
 
  #region State Behaviour

  public override void Enter() {
    base.Enter();

    // For testing purposes:
    // levelData = level.GameData.levels[level.CurrentLevel];
    levelData = level.GameData.levels[0];
   
    if(!hud.activeInHierarchy) 
      hud.SetActive(true);

    spawningRoutine = SpawningRoutine(levelData);
    StartCoroutine(spawningRoutine);
  } 

  public override void Exit() {
    base.Exit();
   
    StopCoroutine(spawningRoutine);
  }

  protected override void AddListeners() {
    EventManager.StartListening<PauseLevelInput>(OnPauseLevelInput);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  protected override void RemoveListeners() {
    EventManager.StopListening<PauseLevelInput>(OnPauseLevelInput);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Events Behaviour
  
  void OnPauseLevelInput(PauseLevelInput pauseLevelInput) {
    Time.timeScale = Time.timeScale == 0 ? Config.TimeScale : 0;
    if (pauseScreen.activeInHierarchy) {
      pauseScreen.SetActive(false);
    } else {
      pauseScreen.SetActive(true);
    }
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    hud.SetActive(false);
  }
  
  #endregion

  #region Private Behaviour

  private IEnumerator SpawningRoutine(GameData.Level level) {
    for (int i = 0; i < level.waves.Length; i++) {
      yield return new WaitForSeconds(1);
      LevelSpawner.SpawnWave(level.waves[i]);
    }
    // TODO: el evento se debería lanzar en el momento en que desaparece el último enemigo
    yield return new WaitForSeconds(3);
    EventManager.TriggerEvent(new EndLevelEvent());
  }
 

  #endregion

}
