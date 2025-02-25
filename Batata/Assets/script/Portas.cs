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

    public List<Question> primitiveQuestions = new List<Question>();
    public List<Question> medievalQuestions = new List<Question>();
    public List<Question> contemporaryQuestions = new List<Question>();
    public List<Question> modernQuestions = new List<Question>();

    private List<Question> currentQuestions = new List<Question>();
    private HashSet<int> usedQuestions = new HashSet<int>();
    private int currentEra = 0;

    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;

        UpdateQuestions(0);
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
        if (currentQuestions.Count == 0)
        {
            Debug.LogWarning("Nenhuma pergunta disponível para esta era.");
            return;
        }

        // Se todas as perguntas já foram usadas, reinicia a sequência e embaralha
        if (questionIndex >= currentQuestions.Count)
        {
            questionIndex = 0;
            ShuffleQuestions();
        }

        Question question = currentQuestions[questionIndex];

        questionText.text = question.textoPergunta;
        bool correctOnLeft = Random.Range(0, 2) == 0;

        if (correctOnLeft)
        {
            correctAnswerText.text = question.respostaCorreta;
            incorrectAnswerText.text = question.respostaIncorreta;
        }
        else
        {
            correctAnswerText.text = question.respostaIncorreta;
            incorrectAnswerText.text = question.respostaCorreta;
        }

        correctDoor.GetComponentInChildren<TextMeshProUGUI>().text = correctAnswerText.text;
        incorrectDoor.GetComponentInChildren<TextMeshProUGUI>().text = incorrectAnswerText.text;
    }


    public void UpdateQuestions(int newEra)
    {
        currentEra = newEra;
        usedQuestions.Clear();
        questionIndex = 0; // Reinicia o índice ao mudar de era

        switch (newEra)
        {
            case 0:
                currentQuestions = new List<Question>(primitiveQuestions);
                break;
            case 1:
                currentQuestions = new List<Question>(medievalQuestions);
                break;
            case 2:
                currentQuestions = new List<Question>(contemporaryQuestions);
                break;
            case 3:
                currentQuestions = new List<Question>(modernQuestions);
                break;
        }

        // Embaralha as perguntas para evitar padrões
        ShuffleQuestions();

        AskQuestion();
    }
    private void ShuffleQuestions()
    {
        for (int i = currentQuestions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Question temp = currentQuestions[i];
            currentQuestions[i] = currentQuestions[j];
            currentQuestions[j] = temp;
        }
    }
    public void ProcessCorrectAnswer()
    {
        if (questionIndex < currentQuestions.Count)
        {
            currentQuestions.RemoveAt(questionIndex); // Remove a pergunta correta
        }

        // Se todas as perguntas foram respondidas corretamente, reinicia a lista
        if (currentQuestions.Count == 0)
        {
            UpdateQuestions(currentEra);
        }
        else
        {
            AskQuestion();
        }
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
