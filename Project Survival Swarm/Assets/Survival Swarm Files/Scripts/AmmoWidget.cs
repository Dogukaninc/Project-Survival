using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
   public TMPro.TMP_Text ammoText;

    public void Refresh(int ammoCount)
    {
        ammoText.text = ammoCount.ToString();
    }
}
