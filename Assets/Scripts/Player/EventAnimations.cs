using UnityEngine;

public class EventAnimations : MonoBehaviour
{

    PlayerCombat playerCombat;
    CharacterAnimator playerAnim;
    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerAnim = GetComponentInParent<CharacterAnimator>();

    }

    public void EventAnim()
    {
        playerCombat.DanoInimigoAnim();
    }
    public void DeadEventAnim()
    {
        playerAnim.AnimMorte();
    }


}
