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

        // Seleciona uma pergunta aleatória da lista
        Question question = questions[Random.Range(0, questions.Count)];
        questionText.text = question.textoPergunta;  // Define o texto da pergunta

        // Define aleatoriamente qual porta será a correta
        bool correctOnLeft = Random.Range(0, 2) == 0;

        if (correctOnLeft)
        {
            correctAnswerText.text = question.respostaCorreta;
            incorrectAnswerText.text = question.respostaIncorreta;

            correctDoor.GetComponentInChildren<TextMeshProUGUI>().text = question.respostaCorreta;
            incorrectDoor.GetComponentInChildren<TextMeshProUGUI>().text = question.respostaIncorreta;
        }
        else
        {
            correctAnswerText.text = question.respostaIncorreta;
            incorrectAnswerText.text = question.respostaCorreta;

            correctDoor.GetComponentInChildren<TextMeshProUGUI>().text = question.respostaIncorreta;
            incorrectDoor.GetComponentInChildren<TextMeshProUGUI>().text = question.respostaCorreta;
        }

        Debug.Log("Pergunta exibida: " + question.textoPergunta); // Verifica se o texto da pergunta foi definido
        Debug.Log("Resposta correta exibida: " + correctAnswerText.text); // Confirma a resposta correta

        /* if(consecutiveCorrectAnswers.count = 5)
         {
             Debug.Log("Acabou as perguntas");
         }*/
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