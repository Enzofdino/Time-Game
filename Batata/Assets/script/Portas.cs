using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DoorInteraction : MonoBehaviour
{
    public Collider2D doorCollider;
    private Collider2D playerCollider;
    private bool isPlayerNearby = false;
    private int questionIndex = 0;
    private int consecutiveCorrectAnswers = 0;
    private const int maxConsecutiveCorrect = 5;

    public Sprite defaultSprite;
    public Sprite highlightedSprite;
    private SpriteRenderer spriteRenderer;

    // Componentes de texto para exibir pergunta e respostas
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI correctAnswerText;
    public TextMeshProUGUI incorrectAnswerText;

    // Portas para respostas corretas e incorretas
    public GameObject correctDoor;
    public GameObject incorrectDoor;

    // Lista pública de perguntas para serem editadas no Inspector
    public List<Question> questions = new List<Question>();

    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;

        // Chama AskQuestion() para exibir a primeira pergunta e respostas assim que o jogo começa
        AskQuestion();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby)
        {
            AskQuestion();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            isPlayerNearby = true;
            Debug.Log("Jogador pode interagir com a porta.");
            spriteRenderer.sprite = highlightedSprite;

            // Verifica se o jogador entrou na porta correta
            if (other.gameObject == correctDoor)
            {
                Debug.Log("O jogador acertou a questão!");
            }
            else if (other.gameObject == incorrectDoor)
            {
                Debug.Log("O jogador errou a questão!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            isPlayerNearby = false;
            Debug.Log("Jogador saiu da área da porta.");
            spriteRenderer.sprite = defaultSprite;
        }
    }

    void AskQuestion()
    {
        if (questions.Count == 0)
        {
            Debug.LogWarning("Nenhuma pergunta encontrada.");
            return;
        }

        if (questionText == null || correctAnswerText == null || incorrectAnswerText == null)
        {
            Debug.LogError("Os componentes de texto não estão atribuídos no Inspector!");
            return;
        }

        if (correctDoor == null || incorrectDoor == null)
        {
            Debug.LogError("Os objetos das portas não estão atribuídos no Inspector!");
            return;
        }

        // Seleciona uma pergunta aleatória da lista
        Question question = questions[Random.Range(0, questions.Count)];
        questionText.text = question.textoPergunta;

        // Define aleatoriamente qual porta será a correta
        bool correctOnLeft = Random.Range(0, 2) == 0;

        TextMeshProUGUI correctDoorText = correctDoor.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI incorrectDoorText = incorrectDoor.GetComponentInChildren<TextMeshProUGUI>();

        if (correctDoorText == null)
            Debug.LogError("TextMeshProUGUI não encontrado dentro de correctDoor!", correctDoor);

        if (incorrectDoorText == null)
            Debug.LogError("TextMeshProUGUI não encontrado dentro de incorrectDoor!", incorrectDoor);

        if (correctDoorText == null || incorrectDoorText == null)
        {
            Debug.LogError("As portas não possuem um componente TextMeshProUGUI dentro!");
            return;
        }
        
        if (correctOnLeft)
        {
            correctAnswerText.text = question.respostaCorreta;
            incorrectAnswerText.text = question.respostaIncorreta;

            correctDoorText.text = question.respostaCorreta;
            incorrectDoorText.text = question.respostaIncorreta;
        }
        else
        {
            correctAnswerText.text = question.respostaIncorreta;
            incorrectAnswerText.text = question.respostaCorreta;

            correctDoorText.text = question.respostaIncorreta;
            incorrectDoorText.text = question.respostaCorreta;
        }

        Debug.Log("Pergunta exibida: " + question.textoPergunta);
    }

    bool GetAnswerFromPlayer()
    {
        return Random.Range(0, 2) == 0;
    }
}
 

// Classe para armazenar perguntas e respostas
[System.Serializable]
public class Question
{
    public string textoPergunta;
    public string respostaCorreta;
    public string respostaIncorreta;

    public Question(string textoPergunta, string respostaCorreta, string respostaIncorreta)
    {
        this.textoPergunta = textoPergunta;
        this.respostaCorreta = respostaCorreta;
        this.respostaIncorreta = respostaIncorreta;
    }
}