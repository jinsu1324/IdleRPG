using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int CurrentHp { get; }
    int MaxtHp { get; }
    bool IsDead { get; }
    void TakeDamage(int atk);
    void Die();
}
