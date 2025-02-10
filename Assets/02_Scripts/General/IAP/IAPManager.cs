using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;




public class IAPManager : MonoBehaviour, IStoreListener
{
    public static event Action<OnRewardedArgs> OnIAPCompleted; // ���� �Ϸ� �� �̺�Ʈ

    //[SerializeField] private Button _gem1000Button; // gem1000 ���Ź�ư

    private IStoreController _storeController;  // �ξ۰��� ��Ʈ�ѷ�
    
    // ��ǰ ID
    private string _gem500 = "gem500";          // 500 ���� (�Ҹ�)
    private string _gem1000 = "gem1000";        // 1000 ���� (��Ҹ�)

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        InitIAP();
    }

    /// <summary>
    /// �ξ� ���� �ý��� �ʱ�ȭ
    /// </summary>
    private void InitIAP()
    {
        // ���� ���� ���� ����
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // ��ǰ �߰�
        builder.AddProduct(_gem500, ProductType.Consumable);
        builder.AddProduct(_gem1000, ProductType.Consumable);

        // ���� �ý��� �ʱ�ȭ
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// IAP �ʱ�ȭ ���� �� ȣ���
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // ���� ��Ʈ�ѷ� ����
        _storeController = controller; 

        //// ��Ҹ� ��ǰ ���� ���� Ȯ��
        //CheckNonConsumable(_gem1000);
    }

    /// <summary>
    /// IAP �ʱ�ȭ ���� �� ȣ���
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"�ʱ�ȭ ���� : {error}");
    }

    /// <summary>
    /// IAP �ʱ�ȭ ���� �� ȣ��� (�߰� �޽��� ����)
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"�ʱ�ȭ ���� : {error} , {message}");
    }

    /// <summary>
    /// ���� ���� �� ȣ���
    /// </summary>
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"���� ����");
    }

    /// <summary>
    /// ���� ���� �� ȣ���
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        // ������ ��ǰ ���� ��������
        var product = purchaseEvent.purchasedProduct;

        Debug.Log($"���� ����! : {product.definition.id}");

        if (product.definition.id == _gem500)
        {
            Debug.Log("���� 500�� ���� ����!");

            // ���� 500�� �߰�
            GemManager.AddGem(500); 

            // ���� �Ϸ� �̺�Ʈ �߻�
            OnRewardedArgs args = new OnRewardedArgs()
            {
                Count = 500,
                CanBuyItem = GemManager.HasEnoughGem(ItemDropMachine.DropCost)
            };
            OnIAPCompleted?.Invoke(args);

            // ���� ���
            SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);
        }
        else if (product.definition.id == _gem1000)
        {
            Debug.Log("���� 1000�� ���� ����!");

            // ���� 1000�� �߰�
            GemManager.AddGem(1000);

            // ���� �Ϸ� �̺�Ʈ �߻�
            OnRewardedArgs args = new OnRewardedArgs()
            {
                Count = 1000,
                CanBuyItem = GemManager.HasEnoughGem(ItemDropMachine.DropCost)
            };
            OnIAPCompleted?.Invoke(args);

            // ���� ���
            SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);

            //// 1000 ���� ��ư ��Ȱ��ȭ (��Ҹ� ��ǰ�̹Ƿ�)
            //_gem1000Button.gameObject.SetActive(false);
        }

        // ���� ó�� �Ϸ�
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// ��ǰ ���� ��û
    /// </summary>
    public void Purchase(string productID)
    {
        _storeController.InitiatePurchase(productID);
    }

    ///// <summary>
    ///// ��Ҹ� ������ ���� ���� Ȯ��
    ///// </summary>
    //private void CheckNonConsumable(string id)
    //{
    //    // id�� �´� �ش� ��ǰ ��������
    //    var product = _storeController.products.WithID(id);

    //    if (product != null)
    //    {
    //        // �����ߴ��� ���� Ȯ��
    //        bool isBuy = product.hasReceipt;

    //        // �̹� ������ ��� ��ư ��Ȱ��ȭ
    //        _gem1000Button.gameObject.SetActive(!isBuy);
    //    }
    //}
}
