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
         * Sempre que o game inicializa, a variável (user preference do unity) score é zerada
         * As informações de user preference são carregas em todas as cenas
         */
        PlayerPrefs.SetInt("score", 0);
    }

    /* Update
     * is called once per frame
     */
    void Update()
    {
        
    }

    /* Método para carregamento de cena
     * Carrega cena 1 (início do jogo)
     */
    public void StartMundoGame()
    {
        /* Método unity para carga de cena
         * Pode ser passado no param o nome da cena ou seu index
         */
        SceneManager.LoadScene("lab1"); 
    }
    /* Método para linkar a cena de creditos
     * Carrega cena 4 (creditos)
     */
    public void CreditosCriadores()
    {
        /* Método unity para carga de cena
         * Pode ser passado no param o nome da cena ou seu index
         */
        SceneManager.LoadScene("lab1_creditos");
    }
}
