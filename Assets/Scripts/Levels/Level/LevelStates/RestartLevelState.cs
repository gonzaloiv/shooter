﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelState : State {

  #region State Behaviour

  [SerializeField] private GameObject restartScreenPrefab;
  private GameObject restartScreen;

  #endregion

  #region Mono Behaviour

  void Awake() {
    restartScreen = Instantiate(restartScreenPrefab, transform);
    restartScreen.SetActive(false);
  }

  #endregion

  #region State Behaviour

  public override void Enter() {
    base.Enter();

    restartScreen.SetActive(true);
  }

  public override void Exit() {
    base.Exit();

    restartScreen.SetActive(false);  
  }

  protected override void AddListeners() {
    EventManager.StartListening<EnterInput>(OnEnterInput);
  }

  protected override void RemoveListeners() {
    EventManager.StopListening<EnterInput>(OnEnterInput);
  }

  #endregion

  #region Event Behaviour

  void OnEnterInput(EnterInput enterInput) {
    EventManager.TriggerEvent(new RestartGameEvent());
  }

  #endregion

}
