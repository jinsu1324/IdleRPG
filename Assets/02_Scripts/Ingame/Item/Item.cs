using System;

/// <summary>
/// 아이템 (퓨어 데이터)
/// </summary>
public class Item
{
    public string ID { get; private set; }           // ID
    public ItemType ItemType { get; private set; }   // 아이템 타입
    public int Count { get; private set; }           // 갯수
    public int Level { get; private set; }           // 레벨

    /// <summary>
    /// 생성자
    /// </summary>
    public Item(string id, string itemType, int count, int level)
    {
        ID = id;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), itemType);
        Count = count;
        Level = level;
    }

    /// <summary>
    /// 아이템 갯수 추가
    /// </summary>
    public void AddCount(int amount)
    {
        if (amount < 0) return;
        Count += amount;
    }

    /// <summary>
    /// 아이템 갯수 감소
    /// </summary>
    public void ReduceCount(int amount)
    {
        if (amount < 0 || Count - amount < 0) return;
        Count -= amount;
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public void LevelUp()
    {
        Level++;
    }
}
