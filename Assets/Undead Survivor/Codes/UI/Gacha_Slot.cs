using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Gacha_Slot : MonoBehaviour
{
    
    public Image Item;
    public TMP_Text Upgrade_item;
    public Image Frame;
    public GameObject effect;
    public Sprite SS_Frame;
    public Sprite S_Frame;
    public Sprite A_Frame;
    public Sprite B_Frame;
    public Sprite C_Frame;
    public Sprite D_Frame;
    public Image fire_effect;
    private void OnEnable()
    {
        Item.gameObject.SetActive(false);
        effect.SetActive(true);
        Upgrade_item.gameObject.SetActive(false);
        
        
    }
    private void OnDisable()
    {
        fire_effect.gameObject.SetActive(false);
    }
    public void image_change(EquipmentData item)
    {
        Item.gameObject.SetActive(true);
        Frame_change(item);
        Item.sprite = item.texture;
    }
    public void image_change_Upgrade(EquipmentData item)
    {
        Item.gameObject.SetActive(true);
        Frame_change(item);
        Item.sprite = item.texture;
       
        Upgrade_item.gameObject.SetActive(true);
    }
    public void Frame_change(EquipmentData item)
    {
        var color = Item.color;
        color.a = 0f;
        if (item.grade == ItemGrade.SS)
        { 
            Frame.sprite = SS_Frame;
            Item.color = color;
            //Upgrade_item.color = color;
            Frame.color = color;
            fire_effect.color = Color.yellow;
        }
        else if (item.grade == ItemGrade.S)
        {
            Frame.sprite = S_Frame;
            Item.color = color;
            //Upgrade_item.color = color;
            Frame.color = color;
            fire_effect.color = Color.magenta;
        }
        else if (item.grade == ItemGrade.A)
        {
            Frame.sprite = A_Frame;
            Item.color = color;
            //Upgrade_item.color = color;
            Frame.color = color;
            Color aa= ColorUtility.TryParseHtmlString("#00A1FF", out Color colors) ? colors : Color.white; ;
            fire_effect.color = aa;
            
        }
        else if (item.grade == ItemGrade.B)
        {
            effect.SetActive(false);
            fire_effect.gameObject.SetActive(false);
            Frame.sprite = B_Frame;

        }
        else if (item.grade == ItemGrade.C)
        {
            effect.SetActive(false);
            fire_effect.gameObject.SetActive(false);
            Frame.sprite = C_Frame;
        }
        else
        {
            effect.SetActive(false);
            fire_effect.gameObject.SetActive(false);
            Frame.sprite = D_Frame;
            
        }
    }

    public void FadeInImage(GameObject targetObject, float fadeInDuration, bool includeChildren)
    {
        // 상위 오브젝트의 Image 컴포넌트를 가져옵니다.
        Image image = targetObject.GetComponent<Image>();

        
            // 지정된 이미지의 투명도를 서서히 변경합니다.
            Color initialColor = image.color;
            initialColor.a = 0;
            image.color = initialColor;
            image.DOFade(1, fadeInDuration).SetEase(Ease.InOutSine);
        

        if (includeChildren)
        {
            // 하위 오브젝트의 Image 컴포넌트를 가져옵니다.
            Image[] childImages = targetObject.GetComponentsInChildren<Image>(true);

            // 각 하위 이미지의 투명도를 서서히 변경합니다.
            foreach (Image childImage in childImages)
            {
                if (childImage != image) // 이미 처리한 상위 이미지는 건너뜁니다.
                {
                    initialColor = childImage.color;
                    initialColor.a = 0;
                    childImage.color = initialColor;
                    childImage.DOFade(1, fadeInDuration).SetEase(Ease.InOutSine);
                }
            }
        }
    }
}
