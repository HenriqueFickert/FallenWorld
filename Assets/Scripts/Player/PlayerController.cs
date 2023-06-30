using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    //scripts
    private PlayerMotor motor;
    public Interactable focus;
    private PlayerStats playerStats;

    private Camera cam;
    public LayerMask movementMask;
    private Transform playerTransform;

    //cursor light
    public Transform lightG;
    public Animator lightA;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        playerStats = GetComponent<PlayerStats>();
        playerTransform = transform;
    }

    void Update()
    {

        //Se estiver clicando na UI não andar com o jogador
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Checa por um raycast se o player esta interagindo com algo ou quer andar
        if (playerStats.vidaAtual > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                    else
                    {

                            motor.MoveToPoint(hit.point);
                            RemoveFocus();
                            StartCoroutine(LightCursor(hit.point));

                    }
                }
            }
        }
    }


    //Funcao que seta um novo foco ao player
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDeFocused();
            }

            focus = newFocus;
            motor.FollowTargert(newFocus);
        }
        newFocus.OnFocused(playerTransform);
    }

    //Funcao que remove o foco do player
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocused();
        }

        focus = null;
        motor.StopFollowingTargert();
    }

    //Corrotina que faz a animacao da luz ao clicar
    IEnumerator LightCursor(Vector3 ponto)
    {
        lightG.transform.position = new Vector3(ponto.x, ponto.y + 0.1f, ponto.z);
        lightA.SetBool("Ativo", true);
        yield return new WaitForEndOfFrame();
        lightA.SetBool("Ativo", false);
        yield return 0;
    }
}
