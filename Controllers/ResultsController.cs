using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/results")]
public class ResultController : ControllerBase
{
    private List<Aluno> alunos = new List<Aluno>
    {
        new Aluno { Nome = "aluno3", Notas = new List<int> { 4, 3, 3 } },
        new Aluno { Nome = "aluno7", Notas = new List<int> { 5, 6, 5 } },
        new Aluno { Nome = "aluno1", Notas = new List<int> { 3, 4, 10, 4, 10, 10, 10, 1 } },
        new Aluno { Nome = "aluno6", Notas = new List<int> { 10, 9, 10 } },
        new Aluno { Nome = "aluno5", Notas = new List<int> { 3, 4, 10 } },
        new Aluno { Nome = "aluno4", Notas = new List<int> { 3, 4, 10 } },
        new Aluno { Nome = "aluno2", Notas = new List<int> { 3, 4, 10 } }
    };

    [HttpGet]
    public IActionResult GetResults()
    {
        // Média da sala
        List<int> notasSala = alunos.SelectMany(aluno => aluno.Notas).ToList();
        double mediaSala = CalculadoraNotas.CalcularMedia(notasSala);

        // Notas médias dos alunos ordenadas pelo maior valor
        var notasMediasAlunos = alunos
            .Select(aluno => new
            {
                Nome = aluno.Nome,
                Media = CalculadoraNotas.CalcularMedia(aluno.Notas),
                MaiorNota = aluno.Notas.Max(),
                Aprovado = CalculadoraNotas.CalcularMedia(aluno.Notas) >= 7
            })
            .OrderByDescending(aluno => aluno.Media)
            .ToList();

        return Ok(new
        {
            MediaSala = Math.Round(mediaSala, 2),
            NotasMediasAlunos = notasMediasAlunos
        });
    }
}