using Microsoft.AspNetCore.Mvc;
using novaTentativa.Server;
using novaTentativa.Models;
using Newtonsoft.Json;


namespace novaTentativa.Controllers
{
    [ApiController]
    //[Route("api:[controller]")]
    [Route("api/[controller]")]
    public class CantorController : ControllerBase
    {
        private readonly Cantor_Server _cantor_Server; //injeção 

        public CantorController(Cantor_Server cantor_Server)
        {
            _cantor_Server = cantor_Server;
        }


        //##################################
        //cantor - musica
        //Atualização == 
        [HttpGet("GetMusicas")]
        public async Task<IActionResult> GetFiltroCantores(string? musica = null)
        {
            var cantores = await _cantor_Server.GetCantoresAsync();
            var filtros = string.IsNullOrEmpty(musica) 
                ?cantores :cantores.Where(m => m.Song.Contains(musica,StringComparison.OrdinalIgnoreCase));

            var resultado = filtros.Select(m => new { m.Artist, m.Song }).ToList();
            return Ok(resultado);
        }


        [HttpGet("GetTodosGeneros")]
        public async Task<IActionResult> GetGeneros()
        {
            var arti = await _cantor_Server.GetCantoresAsync();
            var gene = arti.Select(c => c.Genre?.ToLower()).Where(gene => !string.IsNullOrEmpty(gene)).Distinct().ToList();

            return Ok(gene);
        }


        //##################################
        //genero
        //Atualização ==
        [HttpGet("GetGenero")]
        public async Task<IActionResult> GetFiltroGenero(string? genero = null)
        {
            //linq == where, select 
            var cantores = await _cantor_Server.GetCantoresAsync();
            var generosFil = string.IsNullOrEmpty(genero) 
                ? cantores : cantores.Where(g => g.Genre.Contains(genero, StringComparison.OrdinalIgnoreCase));

            var resultado = generosFil.Select(g => new { Genero = g.Genre }).ToList();
            return Ok(resultado);
        }



        //##################################
        //cantor ordenado
        [HttpGet("Getartistaordenado")]
        public async Task<IActionResult> GetCantoresOrdenados()
        {
            try
            {
                var cantores = await _cantor_Server.GetCantoresAsync();
                var cantoresOrdenados = cantores.OrderBy(c => c.Artist).ToList();
                var resposta = cantoresOrdenados.Select(c => new { c.Artist }).ToList();

                return Ok(resposta);
            }
            catch (JsonException)
            { return StatusCode(500, "Erro ao  ordenar."); }
        }


        //Newtonsoft.Json == C#
        //IActionResult == resposta http



        //##################################
        //cantor
        [HttpGet("GetArtista")]
        public async Task<IActionResult> GetFiltroPorNome(string? artist = null)
        {
            try
            {
                var cantores = await _cantor_Server.GetCantoresAsync();
                var cantoresFiltro = string.IsNullOrEmpty(artist) ? cantores 
                    : cantores.Where(c => c.Artist.Contains(artist,StringComparison.OrdinalIgnoreCase));

                var resultado = cantoresFiltro.Select(c => $"{c.Artist}, Música: {c.Song}").ToList();           
                return Ok(resultado);
            }
            catch (JsonException)
            { return StatusCode(500, "Cantor não localizado."); }

        }
    }
}
