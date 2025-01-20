using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// �����͵� �ʱ�ȭ Ŭ����
/// </summary>
public class DataInitializer : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("������ �ʱ�ȭ ����...");
        
        // ��� ������ �ε� ���
        await LoadAllDataAsync();

        Debug.Log("��� ������ �ε� �Ϸ�! ������ �����մϴ�!");
        StartGame();
    }

    /// <summary>
    /// ��� ������ �ε�
    /// </summary>
    private async Task LoadAllDataAsync()
    {
        // ��� ������ �Ŵ��� �ε� �۾� �߰�
        var tasks = new List<Task>
        {
            ItemManager.LoadItemDataAsync(),
            // ���⿡ ������ �ε� �߰�
        };

        // ��� �񵿱� �۾� ���
        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StartGame()
    {
        Debug.Log("���� ����!");

        // ���� ���� ����...
    }
}
