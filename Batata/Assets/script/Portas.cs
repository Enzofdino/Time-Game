using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DoorInteraction : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;

    private Collider2D playerCollider;
    private bool isPlayerNearby = false;

    public GameObject doorToOpen;

    private int consecutiveCorrectAnswers = 0;
    private const int maxConsecutiveCorrect = 5;

    public List<Question> questions = new List<Question>();
    private Question currentQuestion;
    private bool optionAIsCorrect;

    private int selectedOption = 0; // 0 = A, 1 = B

    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        questionPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E) && !questionPanel.activeSelf)
            {
                ShowQuestion();
            }

            if (questionPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    selectedOption = 1 - selectedOption; // Alterna entre 0 e 1
                    UpdateOptionHighlight();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    EvaluateAnswer();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            questionPanel.SetActive(false);
        }
    }

    void ShowQuestion()
    {
        questionPanel.SetActive(true);

        currentQuestion = questions[Random.Range(0, questions.Count)];
        questionText.text = currentQuestion.textoPergunta;

        bool correctOnA = Random.Range(0, 2) == 0;
        optionAIsCorrect = correctOnA;

        if (correctOnA)
        {
            optionAText.text = currentQuestion.respostaCorreta;
            optionBText.text = currentQuestion.respostaIncorreta;
        }
        else
        {
            optionAText.text = currentQuestion.respostaIncorreta;
            optionBText.text = currentQuestion.respostaCorreta;
        }

        selectedOption = 0;
        UpdateOptionHighlight();
    }

    void UpdateOptionHighlight()
    {
        // Destaca a opção selecionada
        optionAText.color = selectedOption == 0 ? Color.yellow : Color.white;
        optionBText.color = selectedOption == 1 ? Color.yellow : Color.white;
    }

    void EvaluateAnswer()
    {
        bool playerChoseCorrect = (selectedOption == 0 && optionAIsCorrect) || (selectedOption == 1 && !optionAIsCorrect);

        if (playerChoseCorrect)
        {
            consecutiveCorrectAnswers++;
            Debug.Log($"Acertou! ({consecutiveCorrectAnswers}/{maxConsecutiveCorrect})");

            if (consecutiveCorrectAnswers >= maxConsecutiveCorrect)
            {
                AbrirPorta();
                return;
            }
        }
        else
        {
            Debug.Log("Errou! Reiniciando progresso.");
            consecutiveCorrectAnswers = 0;
        }

        ShowQuestion(); // Próxima pergunta
    }

    void AbrirPorta()
    {
        questionPanel.SetActive(false);
        doorToOpen.SetActive(false);
        Debug.Log("Porta liberada!");
        this.enabled = false;
    }
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

}

