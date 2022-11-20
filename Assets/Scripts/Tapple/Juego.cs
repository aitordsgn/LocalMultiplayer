using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Linq;

public class Juego : MonoBehaviour
{
    #region Variables
    //UI
    [SerializeField] private string[] temasprivate; //El array con los temas
    [SerializeField] private TextMeshProUGUI TemaTMPro,Tema1,Tema2, TurnoIzda,TurnoDcha;
    [SerializeField] private int randomnum1,randomnum2;
    [SerializeField] private GameObject BotonPrevPausa;

    //Temas
    [SerializeField] private LectorDeTemas lectorDeTemas;
    [SerializeField] private List<string> temasusados = new List<string>();


    //Mover Animator
    [SerializeField] private Animator ControladorUI;
    [SerializeField] private List<Button> Letras;
    [SerializeField] private bool TodasLetras;



    [SerializeField] private MenuTapple menuTapple;



    //State machine
    public enum BattleState {START,J1,J2,WON,LOST,PAUSE}
    public BattleState state, prevstate;



    //Event System para controlar el menu
    [SerializeField] private GameObject SeleccionTemas, Tapple;

    [SerializeField] private GameObject Primeraletra;


    //Tiempo
    [SerializeField] private TextMeshProUGUI TextoTiempo,TextoTiempo2;
    [SerializeField] float Tiempo=10;

    //Frame Siguiendo Seleccionado
    [SerializeField] private GameObject Frame,Selection;


    //Jugadores
    [SerializeField] private GameObject J1, J2;


    [SerializeField] private PlayerConfiguration[] Players;

    #endregion
    private void Awake()
    {
        lectorDeTemas = GetComponent<LectorDeTemas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        temasprivate = lectorDeTemas.temas;
        state = BattleState.START;
        DobleTema();
        Players = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        Destroy(GameObject.Find("PlayerConfigurationManager"));
        FindObjectOfType<AudioManager>().PlayMusic("Tema");

    }

    // Update is called once per frame
    void Update()
    {
        

        if (state == BattleState.J1 || state == BattleState.J2)
        {
           if(Tiempo>0)
            {
                Tiempo -= Time.deltaTime;
                TextoTiempo.text = Tiempo.ToString("0");
                TextoTiempo2.text = Tiempo.ToString("0");

            }
            else
            {
                Tiempo = 10;
                StartCoroutine("GameFinished");
            }
            Selection = EventSystem.current.currentSelectedGameObject;

            if(Selection == null)
            {
                Frame.SetActive(false);
            }
            else
            {
                Frame.transform.position = Selection.transform.position;
            }

        }
        
        if(Letras.All (p => p.interactable == false))
        {
            Frame.SetActive(false);
            StartCoroutine("GameFinished");
        }
        
        if(state == BattleState.J1)
        {
            TurnoIzda.text = "Jugador 1";
            TurnoDcha.text = "Jugador 1";
        }
        else if(state == BattleState.J2)
        {
            TurnoIzda.text = "Jugador 2";
            TurnoDcha.text = "Jugador 2";
        }
    
    
    }

    void DobleTema()
    {
        randomnum1 = UnityEngine.Random.Range(1,temasprivate.Length);
        while (temasusados.Contains(temasprivate[randomnum1]))
        {
            randomnum1 = UnityEngine.Random.Range(1, temasprivate.Length);
        }
        Tema1.text = temasprivate[randomnum1];

        randomnum2 = UnityEngine.Random.Range(1, temasprivate.Length);
        while (temasusados.Contains(temasprivate[randomnum2]) && randomnum2==randomnum1)
        {
            randomnum2 = UnityEngine.Random.Range(1, temasprivate.Length);
        }
        Tema2.text = temasprivate[randomnum2];

    }
    void CambiarTema(string tema)
    {
        //Cambiamos el tema visible en pantalla
        TemaTMPro.text = tema;
        temasusados.Add(tema);
        if(temasusados.Count == temasprivate.Length)
        {
            temasusados.RemoveRange(0,temasusados.Count);
        }
    }

    public void BotonDcha()
    {
        temasusados.Add(Tema1.text);
        TemaTMPro.text = Tema1.text;
        ControladorUI.SetTrigger("Seleccion");
        if(Random.Range(0,1)>0.5)
        {
            state = BattleState.J1;
        }
        else
        {
            state = BattleState.J2;
        }
        //Limpiar el objeto seleccionado
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(Primeraletra);
        SelectRandom();
        SeleccionTemas.SetActive(false);
        SonidoBoton();
    }

    public void BotonIzda()
    {
        temasusados.Add(Tema2.text);
        TemaTMPro.text = Tema2.text;
        ControladorUI.SetTrigger("Seleccion");
        if (Random.Range(0, 1) > 0.5)
        {
            state = BattleState.J1;
        }
        else
        {
            state = BattleState.J2;
        }
        //Limpiar el objeto seleccionado
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(Primeraletra);
        SeleccionTemas.SetActive(false);
        SonidoBoton();
    }
    #region Controles Gamepad
    public void OnGamePadA(InputAction.CallbackContext context)
    {
        
    }

    public void OnGamePadB(InputAction.CallbackContext context)
    {
        
    }
    public void OnGamePadX(InputAction.CallbackContext context)
    {
        Debug.Log(InputSystem.devices);
        Debug.Log("X");
    }
    public void OnGamePadY(InputAction.CallbackContext context)
    {
        
    }
    public void OnGamePadRT(InputAction.CallbackContext context)
    {
        if(state == BattleState.START)
        {
            BotonIzda();
        }
    }

    public void OnGamePadLT(InputAction.CallbackContext context)
    {
        if (state == BattleState.START)
        {
            BotonIzda();
        }
    }
    public void OnGamePadMove(InputAction.CallbackContext context)
    {
        
    }
    public void OnPausa(InputAction.CallbackContext context)
    {
        if (state != BattleState.PAUSE)
        {
            BotonPrevPausa = EventSystem.current.currentSelectedGameObject;

            prevstate = state;
            state = BattleState.PAUSE;
        }
        else
        {
            state = prevstate;

        }
        menuTapple.PauseUnpause(Selection);
    }

    #endregion
    public void ReiniciarTiempo()
    {
        Tiempo = 10;
        if (state == BattleState.J1)
        {
            Frame.GetComponent<Image>().color = Players[0].PlayerColor;
            state = BattleState.J2;
        }
        else
        {
            Frame.GetComponent<Image>().color = Players[1].PlayerColor;
            state = BattleState.J1;

        }
    }
    public void SelectRandom()
    {
      if(Random.Range(0,1) < 0.5f)
        {
            state = BattleState.J1;
            Frame.GetComponent<Image>().color = Players[1].PlayerColor;
        }
        else
        {
            state = BattleState.J2;
            Frame.GetComponent<Image>().color = Players[0].PlayerColor;

        }
    }
    IEnumerator GameFinished()
    {
        state = BattleState.LOST;
        yield return new WaitForSeconds(5f);
        FindObjectOfType<LevelLoader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);

    }


    public void SonidoBoton()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
    
}
