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
    public static event Action<OnRewardedArgs> OnIAPCompleted; // 결제 완료 시 이벤트

    //[SerializeField] private Button _gem1000Button; // gem1000 구매버튼

    private IStoreController _storeController;  // 인앱결제 컨트롤러
    
    // 상품 ID
    private string _gem500 = "gem500";          // 500 보석 (소모성)
    private string _gem1000 = "gem1000";        // 1000 보석 (비소모성)

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        InitIAP();
    }

    /// <summary>
    /// 인앱 결제 시스템 초기화
    /// </summary>
    private void InitIAP()
    {
        // 결제 설정 빌더 생성
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // 상품 추가
        builder.AddProduct(_gem500, ProductType.Consumable);
        builder.AddProduct(_gem1000, ProductType.Consumable);

        // 결제 시스템 초기화
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// IAP 초기화 성공 시 호출됨
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // 결제 컨트롤러 저장
        _storeController = controller; 

        //// 비소모성 상품 구매 여부 확인
        //CheckNonConsumable(_gem1000);
    }

    /// <summary>
    /// IAP 초기화 실패 시 호출됨
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"초기화 실패 : {error}");
    }

    /// <summary>
    /// IAP 초기화 실패 시 호출됨 (추가 메시지 포함)
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"초기화 실패 : {error} , {message}");
    }

    /// <summary>
    /// 결제 실패 시 호출됨
    /// </summary>
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"구매 실패");
    }

    /// <summary>
    /// 결제 성공 시 호출됨
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        // 구매한 상품 정보 가져오기
        var product = purchaseEvent.purchasedProduct;

        Debug.Log($"구매 성공! : {product.definition.id}");

        if (product.definition.id == _gem500)
        {
            Debug.Log("보석 500개 구매 성공!");

            // 보석 500개 추가
            GemManager.AddGem(500); 

            // 구매 완료 이벤트 발생
            OnRewardedArgs args = new OnRewardedArgs()
            {
                Count = 500,
                CanBuyItem = GemManager.HasEnoughGem(ItemDropMachine.DropCost)
            };
            OnIAPCompleted?.Invoke(args);

            // 사운드 재생
            SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);
        }
        else if (product.definition.id == _gem1000)
        {
            Debug.Log("보석 1000개 구매 성공!");

            // 보석 1000개 추가
            GemManager.AddGem(1000);

            // 구매 완료 이벤트 발생
            OnRewardedArgs args = new OnRewardedArgs()
            {
                Count = 1000,
                CanBuyItem = GemManager.HasEnoughGem(ItemDropMachine.DropCost)
            };
            OnIAPCompleted?.Invoke(args);

            // 사운드 재생
            SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);

            //// 1000 보석 버튼 비활성화 (비소모성 상품이므로)
            //_gem1000Button.gameObject.SetActive(false);
        }

        // 구매 처리 완료
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// 상품 구매 요청
    /// </summary>
    public void Purchase(string productID)
    {
        _storeController.InitiatePurchase(productID);
    }

    ///// <summary>
    ///// 비소모성 아이템 구매 여부 확인
    ///// </summary>
    //private void CheckNonConsumable(string id)
    //{
    //    // id에 맞는 해당 상품 가져오기
    //    var product = _storeController.products.WithID(id);

    //    if (product != null)
    //    {
    //        // 구매했는지 여부 확인
    //        bool isBuy = product.hasReceipt;

    //        // 이미 구매한 경우 버튼 비활성화
    //        _gem1000Button.gameObject.SetActive(!isBuy);
    //    }
    //}
}
