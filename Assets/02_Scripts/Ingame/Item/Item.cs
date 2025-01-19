using System;

/// <summary>
/// ������ (ǻ�� ������)
/// </summary>
public class Item
{
    public string ID { get; private set; }           // ID
    public ItemType ItemType { get; private set; }   // ������ Ÿ��
    public int Count { get; private set; }           // ����
    public int Level { get; private set; }           // ����

    /// <summary>
    /// ������
    /// </summary>
    public Item(string id, string itemType, int count, int level)
    {
        ID = id;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), itemType);
        Count = count;
        Level = level;
    }

    /// <summary>
    /// ������ ���� �߰�
    /// </summary>
    public void AddCount(int amount)
    {
        if (amount < 0) return;
        Count += amount;
    }

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public void ReduceCount(int amount)
    {
        if (amount < 0 || Count - amount < 0) return;
        Count -= amount;
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public void LevelUp()
    {
        Level++;
    }
}
