using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerStatsSO", menuName = "playerStatsSO", order = 1)]
public class PlayerStats_SO : ScriptableObject
{
    //Fields
    [SerializeField] private int _mh;
    [SerializeField] private int _ch;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackHeight;
    [SerializeField] private float _nextAttackTime;
    [SerializeField] private bool _canAttack;

    //Properties
    [SerializeField] public int MaxHealth { 
        get {
            return _mh;
        } 
        set {
            _mh = value;
        } 
    }
    [SerializeField] public int CurrHealth { 
        get {
            return _ch;
        } 
        set {
            _ch = value;
        } 
    }
    [SerializeField] public int AttackDamage { 
        get {
            return _attackDamage;
        } 
        set {
            _attackDamage = value;
        } 
    }
    [SerializeField] public float AttackRate { 
        get {
            return _attackRate;
        } 
        set {
            _attackRate = value;
        } 
    }
    [SerializeField] public float AttackRange { 
        get {
            return _attackRange;
        } 
        set {
            _attackRange = value;
        } 
    }
    [SerializeField] public float AttackHeight { 
        get {
            return _attackHeight;
        } 
        set {
            _attackHeight = value;
        } 
    }
    [SerializeField] public float NextAttackTime { 
        get {
            return _nextAttackTime;
        } 
        set {
            _nextAttackTime = value;
        } 
    }
    [SerializeField] public bool CanAttack { 
        get {
            return _canAttack;
        } 
        set {
            _canAttack = value;
        } 
    }
    
}
