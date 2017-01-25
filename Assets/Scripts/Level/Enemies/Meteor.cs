﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Meteor : BaseEnemy {

  #region Mono Behaviour

  private Animator animator;
  private Vector3 rotation;

  #endregion

  #region Mono Behaviour

  void Awake() {
    animator = GetComponent<Animator>();
    rotation = new Vector3(0, 0, Random.Range(0, 2));
  }

  void Update() {
    transform.Rotate(rotation);
  }

  void OnCollisionEnter2D(Collision2D collision2D) {
    if(collision2D.gameObject.name.Contains("Player"))
      animator.Play("Die");
  }

  #endregion

}