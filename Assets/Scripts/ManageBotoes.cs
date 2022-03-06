using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe para carregamento de cena
/// E preferencias do player
/// </summary>
public class ManageBotoes : MonoBehaviour
{
    /* Start 
     * is called before the first frame update
     */
    void Start()
    {
        /* 
         * Sempre que o game inicializa, a vari�vel (user preference do unity) score � zerada
         * As informa��es de user preference s�o carregas em todas as cenas
         */
        PlayerPrefs.SetInt("score", 0);
    }

    /* Update
     * is called once per frame
     */
    void Update()
    {
        
    }

    /* M�todo para carregamento de cena
     * Carrega cena 1 (in�cio do jogo)
     */
    public void StartMundoGame()
    {
        /* M�todo unity para carga de cena
         * Pode ser passado no param o nome da cena ou seu index
         */
        SceneManager.LoadScene("lab1"); 
    }
    /* M�todo para linkar a cena de creditos
     * Carrega cena 4 (creditos)
     */
    public void CreditosCriadores()
    {
        /* M�todo unity para carga de cena
         * Pode ser passado no param o nome da cena ou seu index
         */
        SceneManager.LoadScene("lab1_creditos");
    }
}
