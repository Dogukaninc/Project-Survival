using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamagePopUp : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI popupText;
    //[SerializeField] private GameObject _popupText;
    [SerializeField] private TextMeshPro __popupText;
    //Damage pop up when player hits something and gives damage

    //TODO Poolmanager yarat
    //TODO bu metod bir managerdan cagirilarak tek bir yerden yonetilecek boylece hasar verdiğimiz her halta bunu atmak zorunda kalmayacağız
    //Textin rotasyonu ve scali randomize edilecek
    //Pop up sonlara doğru fade olacak
    //Spawn olan textler her zaman bize bakacak
    public void DamagePopUpEffect(int damagePoint, Vector3 popupPos)
    {
        TextMeshPro popupText = Instantiate(this.__popupText, popupPos, Quaternion.identity);
        popupText.text = damagePoint.ToString();
        popupText.color = Color.white;

        Quaternion billboardDir = Quaternion.LookRotation(Camera.main.transform.forward);
        popupText.transform.rotation = billboardDir;
        popupText.transform.localScale = new Vector3(UnityEngine.Random.Range(1, 2), 1, UnityEngine.Random.Range(1, 2));

        //popupText.material.DOFade(0,2f); //TODO material aynı olduğu için tüm ui ları etkiliyor galiba
        popupText.transform.DOPunchRotation(popupText.transform.position, 0.5f, 10, 1);
        popupText.transform.DOPunchScale(Vector3.one, 0.5f, 1, 1f);

        popupText.transform.DOMove(popupPos + new Vector3(0, 2, 0), 2f).OnComplete(() => { Destroy(popupText); });
    }
}