using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    private PopUpName popup_name;
    public PopUpName GetPopUpName()
    {
        return popup_name;
    }
    public void SetPopUpName(PopUpName _name)
    {
        popup_name = _name;
    }

    public virtual PopUpName getPopUpName()
    {
        return popup_name;
    }

    public virtual void Show()
    {
        if (this.gameObject != null && this.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void Hide()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void DestroyPopup()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        else return;
    }


}

public enum PopUpName
{
    Win = 0,
    Lose,
    Start,
    Sound,
    Congratulation
}