using UnityEngine;

/// <summary>
/// ���׿� ��ų
/// </summary>
public class Skill_Meteor : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _range;                // ��Ÿ�
    private float _skillAttackPower;     // ���� ��ų ���ݷ�
    
    /// <summary>
    /// ������
    /// </summary>
    public Skill_Meteor(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _range = GetSkillStatValue(SkillStatType.Range);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        //Debug.Log($"Skill_Meteor!! ���ݷ� : {_skillAttackPower}");

        Vector3 targetPos = (FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position) as Component).transform.position;
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(ID);

        SkillProjectile_Meteor projectile = GameObject.Instantiate(itemDataSO.Prefab, targetPos, Quaternion.identity).GetComponent<SkillProjectile_Meteor>();

        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        projectile.Init(finalDamage, isCritical);
    }
}
