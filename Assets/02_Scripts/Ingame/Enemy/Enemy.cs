using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    [SerializeField] private HPCanvas _hpCanvas;        // HP바 들어있는 캔버스
    public bool IsDead { get; private set; }            // 적이 죽었는지

    public event Action<Enemy> OnEnemySpawn;            // 스폰시 이벤트
    public event Action<Enemy> OnEnemyDie;              // 죽었을때 이벤트

    private ObjectPool<Enemy> _pool;                    // 자신을 반환할 풀 참조
    private EnemyData _enemyData;                       // 데이터

    private int _currentHp;                             // 체력
    private int _maxHp;                                 // 최대체력
    private int _attackPower;                           // 공격력
    private int _attackSpeed;                           // 공격속도
    private int _moveSpeed;                             // 이동속도

    private float _attackCooldown;                      // 공격 쿨타임
    private float _time;                                // 쿨타임 시간 계산용
    private Player _targetPlayer;                       // 공격 타겟 플레이어
    private EnemyState _currentState;                   // 현재 상태
    private bool _isFirstAttack;                        // 첫 1회공격인지

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyData = enemyData;

        // 스탯 셋팅
        _currentHp = (_enemyData.MaxHp * statPercentage) / 100;
        _maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _attackPower = (_enemyData.AttackPower * statPercentage) / 100;
        _attackSpeed = _enemyData.AttackSpeed;
        _moveSpeed = _enemyData.MoveSpeed;
        _attackCooldown = 1f / _attackSpeed;

        // 정보들 초기화
        IsDead = false;
        _time = 0f;
        _targetPlayer = null;
        _currentState = EnemyState.Move;
        _isFirstAttack = true;

        _hpCanvas.UpdateHPBar(_currentHp, _maxHp);

        // 스폰 이벤트 호출
        OnEnemySpawn?.Invoke(this);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Move:
                MoveStateHandler();
                break;
            case EnemyState.Attack:
                AttackStateHandler(); 
                break;
        }
    }

    /// <summary>
    /// 움직임 상태 관련 처리들
    /// </summary>
    private void MoveStateHandler()
    {
        MoveLeft();
    }

    /// <summary>
    /// 왼쪽으로 이동
    /// </summary>
    private void MoveLeft()
    {
        transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 공격상태 관련 처리들
    /// </summary>
    private void AttackStateHandler()
    {
        if (_isFirstAttack)
        {
            AttackPlayer();
            _isFirstAttack = false; 
        }
        else if (IsAttackCoolTime())
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// 공격 쿨타임 계산
    /// </summary>
    private bool IsAttackCoolTime()
    {
        _time += Time.deltaTime;

        if (_time >= _attackCooldown)
        {
            _time %= _attackCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 플레이어 공격
    /// </summary>
    private void AttackPlayer()
    {
        if (_targetPlayer != null)
            _targetPlayer.TakeDamage(_attackPower);
    }

    /// <summary>
    /// 플레이어 감지해서 타겟 설정하고 공격상태로 변경
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _targetPlayer = collision.gameObject.GetComponent<Player>();
            _currentState = EnemyState.Attack;
        }
    }

    /// <summary>
    /// 공격 받음
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;
        _hpCanvas.UpdateHPBar(_currentHp, _maxHp);

        if (_currentHp <= 0)
            Die();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    private void Die()
    {
        // 죽었음을 true로
        IsDead = true;

        // 플레이어의 골드 추가 // Todo : 얼마 얻을지 데이터로 빼기
        PlayerManager.Instance.AddGold(1000);

        // 사망 이벤트 호출
        OnEnemyDie?.Invoke(this);  

        // 풀로 반환
        _pool.ReturnObject(this); 
    }

    /// <summary>
    /// 현재 HP 가져오기
    /// </summary>
    public int GetCurrentHP()
    {
        return _currentHp;
    }

}
