using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace TargetSistemas.Controllers;

/// <summary>
/// Controller que contém a resolução dos exercícios do processo seletivo
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProcessoSeletivoController : ControllerBase
{
    /// <summary>
    /// Exercício 1 - Calcula a soma dos números de 1 até 13
    /// </summary>
    /// <returns>Retorna a soma final do processamento</returns>
    /// <response code="200">Retorna o valor da soma calculada</response>
    [HttpGet("1")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult Exercicio1()
    {
        int INDICE = 13, SOMA = 0, K = 0;
        
        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }
        
        return Ok(new { soma = SOMA });
    }

    /// <summary>
    /// Exercício 2 - Verifica se um número pertence à sequência de Fibonacci
    /// </summary>
    /// <param name="numero">Número a ser verificado</param>
    /// <returns>Mensagem indicando se o número pertence ou não à sequência</returns>
    /// <response code="200">Retorna o resultado da verificação</response>
    /// <response code="400">Quando o número informado é negativo</response>
    [HttpGet("2/{numero}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Exercicio2(int numero)
    {
        if (numero < 0) return BadRequest("O número deve ser positivo");

        List<int> fibonacci = new() { 0, 1 };
        
        while (fibonacci[^1] < numero)
        {
            fibonacci.Add(fibonacci[^1] + fibonacci[^2]);
        }

        bool pertence = fibonacci.Contains(numero);
        
        return Ok(new { 
            pertenceSequencia = pertence,
            mensagem = pertence ? 
                $"O número {numero} pertence à sequência de Fibonacci!" : 
                $"O número {numero} NÃO pertence à sequência de Fibonacci!"
        });
    }

    /// <summary>
    /// Exercício 3 - Calcula estatísticas de faturamento diário usando Json ou XML
    /// </summary>
    /// <param name="faturamento">Objeto contendo os dados de faturamento diário</param>
    /// <returns>Estatísticas calculadas do faturamento</returns>
    /// <response code="200">Retorna as estatísticas calculadas</response>
    /// <response code="400">Quando a lista de faturamento está vazia ou nula</response>
    [HttpPost("3")]
    [Consumes("application/json", "application/xml")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Exercicio3([FromBody] FaturamentoMensal faturamento)
    {
        if (faturamento?.Rows == null || !faturamento.Rows.Any()) 
            return BadRequest("Lista de faturamento vazia");

        var diasComFaturamento = faturamento.Rows.Where(f => f.Valor > 0).ToList();
        var mediaMensal = diasComFaturamento.Average(f => f.Valor);
        var diasAcimaMedia = diasComFaturamento.Count(f => f.Valor > mediaMensal);

        return Ok(new
        {
            menorValor = diasComFaturamento.Min(f => f.Valor),
            maiorValor = diasComFaturamento.Max(f => f.Valor),
            diasAcimaMediaMensal = diasAcimaMedia
        });
    }

    /// <summary>
    /// Exercício 4 - Calcula o percentual de representação do faturamento por estado
    /// </summary>
    /// <returns>Dicionário com os percentuais de cada estado</returns>
    /// <response code="200">Retorna os percentuais calculados</response>
    [HttpGet("4")]
    [ProducesResponseType(typeof(Dictionary<string, decimal>), StatusCodes.Status200OK)]
    public IActionResult Exercicio4()
    {
        Dictionary<string, decimal> faturamentoPorEstado = new()
        {
            { "SP", 67836.43m },
            { "RJ", 36678.66m },
            { "MG", 29229.88m },
            { "ES", 27165.48m },
            { "Outros", 19849.53m }
        };

        decimal total = faturamentoPorEstado.Values.Sum();
        var percentuais = faturamentoPorEstado.ToDictionary(
            x => x.Key,
            x => Math.Round((x.Value / total) * 100, 2)
        );

        return Ok(percentuais);
    }

    /// <summary>
    /// Exercício 5 - Inverte os caracteres de uma string
    /// </summary>
    /// <param name="texto">Texto a ser invertido</param>
    /// <returns>Texto original e sua versão invertida</returns>
    /// <response code="200">Retorna o texto original e invertido</response>
    /// <response code="400">Quando o texto está vazio ou nulo</response>
    [HttpGet("5/{texto}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Exercicio5(string texto)
    {
        if (string.IsNullOrEmpty(texto)) return BadRequest("Texto não pode ser vazio");

        char[] caracteres = texto.ToCharArray();
        for (int i = 0; i < caracteres.Length / 2; i++)
        {
            char temp = caracteres[i];
            caracteres[i] = caracteres[caracteres.Length - 1 - i];
            caracteres[caracteres.Length - 1 - i] = temp;
        }

        return Ok(new { 
            textoOriginal = texto,
            textoInvertido = new string(caracteres)
        });
    }
}

/// <summary>
/// Classe que representa o faturamento mensal com uma lista de faturamentos diários
/// </summary>
[XmlRoot("root")]
public class FaturamentoMensal
{
    /// <summary>
    /// Lista de registros de faturamento diário
    /// </summary>
    [XmlElement("row")]
    public List<FaturamentoDiario> Rows { get; set; } = new();
}

/// <summary>
/// Classe que representa um registro de faturamento diário
/// </summary>
public class FaturamentoDiario
{
    /// <summary>
    /// Dia do mês
    /// </summary>
    [XmlElement("dia")]
    public int Dia { get; set; }

    /// <summary>
    /// Valor do faturamento no dia
    /// </summary>
    [XmlElement("valor")]
    public decimal Valor { get; set; }
}
