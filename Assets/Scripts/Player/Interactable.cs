
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3;
    public Transform interactionTransform;
    private bool isFocus = false;
    private bool hasInterected = false;
    private Transform player;

    //Void para ser sobrescrito em outro script
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);

    }

    public virtual void Update()
    {
        //Checa par ver se o player está perto o suficiente para interagir
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius && hasInterected)
            {
                Interact();
                hasInterected = true;
            }
        }
    }

    //funcao que ve se o player esta em foco com algum objeto de interacao
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInterected = true;
    }

    //funcao que desfoca o player do objeto
    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInterected = true;
    }

    //Cria o raio de interação na scene
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
