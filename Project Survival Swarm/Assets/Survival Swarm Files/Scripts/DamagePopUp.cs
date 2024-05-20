using TMPro;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class DamagePopUp : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI popupText;
   //Damage pop up when player hits something and gives damage
   public void DamagePopUpEffect(int damagePoint, Vector3 popupPos)//TODO Poolmanager yarat
   {
      TextMeshProUGUI popupText = Instantiate(this.popupText,popupPos,quaternion.identity);//Todo yön ayarlaması yap
      popupText.transform.DOMove(popupPos+new Vector3(0,5,0),1).OnComplete(() =>
      {
         Destroy(popupText);
      });

   }
   
}
