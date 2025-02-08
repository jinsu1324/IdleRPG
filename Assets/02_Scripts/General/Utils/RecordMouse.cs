using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordMouse : MonoBehaviour
{
    public RectTransform cursorImage;

    private void Awake()
    {
        cursorImage.gameObject.SetActive(false);
        //Cursor.visible = false;
    }

#if UNITY_EDITOR // Unity Editor에서만 실행
    void Start()
    {
        cursorImage.gameObject.SetActive(true);
        //Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition;
    }
#endif
}
