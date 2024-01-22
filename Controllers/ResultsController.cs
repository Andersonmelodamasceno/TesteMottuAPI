using Microsoft.AspNetCore.Mvc;

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
    [Route("api/results")]
    public IActionResult GetResults()
    {
        // Média da sala
        List<int> notasSala = alunos.SelectMany(aluno => aluno.Notas).ToList();
        double mediaSala = CalculadoraNotas.CalcularMedia(notasSala);

        // Notas médias dos alunos ordenados pelo maior valor
        var alunosOrdenados = alunos.OrderByDescending(aluno => CalculadoraNotas.CalcularMedia(aluno.Notas));
        var notasMediasAlunos = alunosOrdenados.Select(aluno => new { Nome = aluno.Nome, Media = CalculadoraNotas.CalcularMedia(aluno.Notas) });

        // Nome do aluno com a maior nota e sua nota respectiva
        var alunoMaiorNota = alunos.OrderByDescending(aluno => aluno.Notas.Max()).First();
        var maiorNota = alunoMaiorNota.Notas.Max();

        // Aprovado/Reprovado
        bool aprovado = CalculadoraNotas.CalcularMedia(alunoMaiorNota.Notas) >= 7;

        return Ok(new
        {
            MediaSala = mediaSala,
            NotasMediasAlunos = notasMediasAlunos,
            NomeAlunoMaiorNota = alunoMaiorNota.Nome,
            MaiorNota = maiorNota,
            Aprovado = aprovado
        });
    }
}