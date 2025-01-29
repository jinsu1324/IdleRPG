using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lazer : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _projectileSpeed;      // ����ü �ӵ�
    private float _skillAttackPower;    // ��ų ���ݷ�

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Lazer(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _projectileSpeed = GetSkillStatValue(SkillStatType.ProjectileSpeed);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(ID);

        SkillProjectile_Lazer projectile = 
            GameObject.Instantiate(
                itemDataSO.Prefab, 
                PlayerManager.PlayerInstance.transform.position + new Vector3(0, 0.5f, 0), 
                Quaternion.identity).
                GetComponent<SkillProjectile_Lazer>();

        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        projectile.Init(finalDamage, isCritical);
    }
}
