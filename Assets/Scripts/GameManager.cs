using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // variaveis publicas para uso na interface
    public GameObject letra;                // prefab da letra no Game
    public GameObject centro;               // objeto de texto que indica o centro da tela

    // variáveis encapsuladas
    private int numTentativas;          // Armazena o total das tentativas válidas
    private int maxNumTentativas;       // Número máximo de tentativas para Forca ou Salvação
    int score = 0;

    private string palavraOculta = "";  // palavra oculta a ser descoberta

    /* private string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; */ 
    // array de palavras ocultas

    private int tamanhoPalavraOculta;   // tamanho da palavra oculta
    char[] letrasOcultas;               // letras da palavra oculta
    bool[] letrasDescobertas;           // indicador de quais letras foram descobertas

    char?[] letrasErradas; //array para guarda as letras erradas já digitada pelo usuario
    


    // Start is called before the first frame update
    void Start()
    {
        /* anteriormente montei 'centroDaTela' na hierarquia
         * e agora peço para buscar o Game Object (Find)
         * e atribuir à variavel publica 'centro'
         */
        centro = GameObject.Find("centroDaTela");

        InitGame();     // chama o método para inicializacao do game
        InitLetras();   // chama método que instancia as letras

        numTentativas = 0;
        maxNumTentativas = 10;
        UpdateNumTentativas(); // chama método para atualizar o numero de tentativas em tela
        UpdateScore(); // chama método para atualizar o numero de pontuacao em tela
    }

    // Update is called once per frame
    void Update()
    {
        CheckTeclado(); // chama funcao a ser executada em tempo de execucao
    }

    /*
     * metodo para instanciar as
     * letras do game
     */
    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;

        // A cada repeticao é criado uma o GameObject da letra e define propriedades
        for (int i = 0; i < numLetras; i++)
        {
            Vector3 novaPosicao; // cria um vetor para a posição de 3 dimensoes

            // Posiciono as letras a partir do GameObject 'centro'
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80),
                    centro.transform.position.y,
                    centro.transform.position.z
                );
            // (i-numLetras/2.0f)*80 -> ajustando pela metade do numero de letras pela posicao

            /*
             * novo object criado,
             * eh generico (l), eh do tipo GameObject
             * e instanciei (aloquei memoria): Instantiate
             * pegando o prototipo letra, na posição que quero (novaPosicao),
             * fazendo uma rotação quaternion
             */
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity); 
            // Quaternion: sempre que rodo tenho que normalizar, por isso o .identity

            l.name = "letra" + (i + 1); // nomeia na hierarquia a GameObject com letra-(ienesima+1)

            /* Define todas as letras como filhas do Canvas,
             * sendo um prefab filho dentro do Canvas na hierarquia
             */
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    /*
     * metodo para inicializacao do
     * fluxo do game
     */
    void InitGame()
    {
        /* palavraOculta = "Elefante"; */                                       // define palavra a ser descoberta (usada no lab1 - parte A)
        /* int numeroAleatorio = Random.Range(0, palavrasOcultas.Length); */    // sorteia numero dentro do numero de palavras do array
        /* palavraOculta = palavrasOcultas[numeroAleatorio]; */                 // selecionamos uma palavra sorteada

        // A palavra oculta virá da leitura aleatória de um arquivo de texto
        palavraOculta = PegaUmaPalavraDoArquivo();

        tamanhoPalavraOculta = palavraOculta.Length;                            // determina numero de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper();                                // transforma todas as letras em maiusculas

        letrasOcultas = new char[tamanhoPalavraOculta];                         // instancia array char das letras com comprimento da palavra oculta
        letrasDescobertas = new bool[tamanhoPalavraOculta];                     // instancia array booleano de marcacao com comprimento da palavra oculta
        letrasOcultas = palavraOculta.ToCharArray();                            // copia letra a letra da palavra (string) no array de letras (char[])
        letrasErradas = new char?[24];                                          // instancia array char para guarda as letras erradas digitadas pelo jogador
    }

    /*
     * metodo que valida a letra digitada e
     * verifica se a letra digitada existe no
     * array da palavra oculta e atualiza
     * a variavel de marcacao e a hierarquia
     */
    void CheckTeclado()
    {


        bool acertouLetra = false; //variavel booleana iniciada usada como indicativo se o jogador acertou ou não a letra
        if (Input.anyKeyDown)    // se qualquer tecla for pressionada
        {
            char letraTeclada = Input.inputString.ToCharArray()[0]; // instancia e inicializa com valor (letra) digitada
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada); // converte letra teclada em inteiro
            // checa consistencia da letra
            if (letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122) // 97 = a e 122 = z
            {
                /* Exibe tela de game over - perda.
                 * Quando o limite de tentativas eh
                 * excedido, carrega cena com a forca.
                 */
                if (numTentativas >= maxNumTentativas)
                {
                    SceneManager.LoadScene("lab1_forca");
                }

                // percorre palavra oculta
                for (int i = 0; i < tamanhoPalavraOculta; i++)
                {
                    // valida apenas posicoes ainda nao descobertas
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada); // transforma a letra teclada em maiuscula
                        // valida se a letra teclada é igual a letra oculta na posicao i
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            /*
                             * se forem iguais, significa que acertou a letra,
                             * então a letra é adicionada ao GameObject na  hierarquia
                             * na respectiva posição da letra,
                             * e o array de marcação é atualizado
                             */
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();

                            /* Atualiza variavel interna score 
                             * com o total da user preference 'score' 
                             * e soma a cada acerto na variavel interna de score.
                             * Em seguida, atualiza user preference de 'score',
                             * mantendo assim o score guardado 
                             * mesmo após o fim das partidas (sessões).
                             */
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore(); // chama metodo para atualziar score em tela
                            VerificaSePalavraDescoberta(); // chama método para verificar se venceu e carregar a cena
                            acertouLetra = true; //caso o jogador acerte a letra a mudo o valor da variavel para true
                        }
                    }


                }

                if (!acertouLetra)//verifico se o jogador acertou uma letra
                {
                    for (int i = 0; i <= letrasErradas.Length; i++)//pecorro meu array de letras erradas
                    {
                        if(letrasErradas[i] == letraTeclada){//verifico se o usuario já digitou esta letra antes
                            break;//paro o loop do meu for
                        }
                        if (!letrasErradas[i].HasValue)//na primeira posição disponivel do array de letras guardo a letra digitada errada
                        {
                            // cada vez que insiro uma tentativa válido, atualizo minhas tentativas
                            numTentativas++;
                            UpdateNumTentativas();
                            letrasErradas[i] = letraTeclada;
                            break;//paro o loop do meu for
                        }

                    }
                    UpdateLetrasErradas();//atualizo a exibição de letras erradas
                }
            }
        }
    }
    /* Exibir Letras Erradass
     * Pecorre o array de letras erradas para exibir ao jogador
     */
    void UpdateLetrasErradas()
    {
        string stringLetrasErradas = "";
        for (int i = 0; i <= letrasErradas.Length; i++)
        {
            if (!letrasErradas[i].HasValue)
            {
                break;
            }
            stringLetrasErradas = stringLetrasErradas + letrasErradas[i] + " ";
        }
        GameObject.Find("letrasErradas").GetComponent<Text>().text = stringLetrasErradas;
    }

    /* Total de tentativas
     * Busca na hierarquia um objeto de texto que 
     * exibirá o total de tentativas válidas e o máximo
     */
    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;
    }

    /* Total de pontos
    * Busca na hierarquia um objeto de texto que 
    * exibirá o total da pontuação
    */
    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score " + score;
    }

    /* Verifica se acertou a palavra oculta
     * E salva na player pref a ultima palavra oculta
     * E carrega cena game over de sucesso
     */
    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;

        /*
         * verifica se todas as letras da palavra foram descobertas
         */
        for(int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }

        if (condicao)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("lab1_salvo");
        }
    }

    /* Retorna palavra aleatória
     * com base na leitura do arquivos
     * de palavras
     */
    string PegaUmaPalavraDoArquivo()
    {
        // lê e carrefa o arquivo no diretório informado e tipa
        TextAsset t1 = (TextAsset)Resources.Load("palavras1", typeof(TextAsset));

        string s = t1.text;               // guarda texto
        string[] palavras = s.Split(' '); // cria array com as palavras do text separadas por espaço

        // sorteia aleatoriamente de 0 até o numero de palavras
        int palavraAleatoria = Random.Range(0, palavras.Length + 1);

        return (palavras[palavraAleatoria]);
    }
}
