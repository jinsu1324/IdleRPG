using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    [SerializeField] private GameObject _dimd;


    [SerializeField] private TextMeshProUGUI _stateText;
    private IStoreController _storeController;
    private string _gem500 = "gem500";
    private string _gem1000 = "gem1000";

    private void Start()
    {
        InitIAP();
    }

    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(_gem500, ProductType.Consumable);
        builder.AddProduct(_gem1000, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        CheckNonConsumable(_gem1000);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"�ʱ�ȭ ���� : {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"�ʱ�ȭ ���� : {error} , {message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"���� ����");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log($"���� ����! : {product.definition.id}");

        if (product.definition.id == _gem500)
        {
            _stateText.text = "���� 500�� ���� ����!";
        }
        else if (product.definition.id == _gem1000)
        {
            _stateText.text = "���� 1000�� ���� ����!";
            _dimd.gameObject.SetActive(true);
        }


        return PurchaseProcessingResult.Complete;
    }

    public void Purchase(string productID)
    {
        _storeController.InitiatePurchase(productID);
    }

    private void CheckNonConsumable(string id)
    {
        var product = _storeController.products.WithID(id);

        if (product != null)
        {
            bool isCheck = product.hasReceipt;

            _dimd.gameObject.SetActive(isCheck);
        }
    }
}
