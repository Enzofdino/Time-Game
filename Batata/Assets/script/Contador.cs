using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Contador : MonoBehaviour
{
    int tick;
    public int hora;
    int minuto;
    int segundo;
    int dia;
    int tDia;
    int mes;
    int ano;
    [SerializeField]
    TextMeshProUGUI tempo;

    public void Start()
    {
        segundo = 0;
        minuto = 59;
        hora = 22;
        tick = 0;
        dia = 31  ;
        mes = 12;
        ano = 0;
    }

    public void Update()
    {
        if (tick < 1)
        {
            tick = tick + 1;
        }
        else if (tick == 1)
        {
            segundo = segundo + 1;
            tick = 0;
        }

        if (segundo == 60)
        {
            minuto = minuto + 1;
            segundo = 0;
        }

        if (minuto == 60)
        {
            hora = hora + 1;
            minuto = 0;
        }

        if (hora == 24)
        {
            dia = dia + 1;
            tDia = tDia + 1;
            hora = 0;
        }

        int diasNoMes = GetDiasNoMes(mes, ano);

        if (dia > diasNoMes)
        {
            dia = 1;
            mes = mes + 1;

            if (mes > 12)
            {
                mes = 1;
                ano = ano + 1;
            }
        }
        if (mes == 13)
        {
            mes = 1;
            ano = ano + 1;
        }

        string formattedHora = hora.ToString("D2");
        string formattedMinuto = minuto.ToString("D2");
        string formattedSegundo = segundo.ToString("D2");
        string formattedDia = dia.ToString("D2");
        string formattedMes = mes.ToString("D2");
        string formattedAno = ano.ToString("D2");

        tempo.text = $"A tempo é {formattedAno}:{formattedMes}:{formattedDia}:{formattedHora}:{formattedMinuto}:{formattedSegundo}";
    }

    int GetDiasNoMes(int mes, int ano)
    {
        switch (mes)
        {
            case 1: // Janeiro
            case 3: // Março
            case 5: // Maio
            case 7: // Julho
            case 8: // Agosto
            case 10: // Outubro
            case 12: // Dezembro
                return 31;
            case 4: // Abril
            case 6: // Junho
            case 9: // Setembro
            case 11: // Novembro
                return 30;
            case 2: // Fevereiro
                // Verifica se o ano é bissexto
                if (IsLeapYear(ano))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            default:
                throw new System.ArgumentOutOfRangeException("Mês inválido");
        }
    }

    bool IsLeapYear(int ano)
    {
        // Ano é bissexto se for divisível por 4 e (não divisível por 100 ou divisível por 400)
        return (ano % 4 == 0) && (ano % 100 != 0 || ano % 400 == 0);
    }
}