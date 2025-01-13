using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스킬 인스턴스 생성 (스킬ID와 각 스킬클래스의 이름이 동일해야함)
/// </summary>
public class SkillFactory
{
    // 스킬 ID 별 스킬클래스 생성 딕셔너리
    private static readonly Dictionary<string, Func<SkillItem>> _skillCreatorDict;

    /// <summary>
    /// 정적 생성자 : 딕셔너리 초기화
    /// </summary>
    static SkillFactory()
    {
        // 1. 딕셔너리 초기화
        _skillCreatorDict = new Dictionary<string, Func<SkillItem>>();

        // 2. Reflection을 사용하여 SkillItem의 하위 클래스를 검색
        var skillTypes = typeof(SkillItem).Assembly.GetTypes() // SkillItem 클래스가 정의된 어셈블리에서 모든 타입 정보를 가져옴
            .Where(type => type.IsClass &&                     // 클래스인지 확인
                   !type.IsAbstract &&                         // 추상 클래스는 제외
                   type.IsSubclassOf(typeof(SkillItem)));      // SkillItem의 하위 클래스만 선택

        // 3. 하위 클래스들을 딕셔너리에 추가
        foreach (var skillType in skillTypes)
        {
            // 클래스 이름을 스킬 ID로 사용
            string skillID = skillType.Name;

            // 생성자 함수를 Func<SkillItem>으로 저장 (ex) type이 SkillMeteor라면 new SkillMeteor() 와 같은역할)
            _skillCreatorDict[skillID] = () => (SkillItem)Activator.CreateInstance(skillType); 
        }
    }

    /// <summary>
    /// 스킬ID를 기반으로 스킬 생성
    /// </summary>
    public static SkillItem CreateSkill(string skillId)
    {
        // 4. 딕셔너리에서 스킬 ID로 생성자 함수 호출
        if (_skillCreatorDict.TryGetValue(skillId, out Func<SkillItem> creator))
        {
            return creator();
        }

        // 5. ID가 없는 경우 null
        Debug.Log($"'{skillId}' 에 맞는 스킬클래스를 생성할수 없었습니다.");
        return null;
    }
}
