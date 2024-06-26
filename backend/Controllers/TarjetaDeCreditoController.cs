using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

[Route("api/[controller]")]
[ApiController]
public class TarjetaDeCreditoController : ControllerBase
{
    private readonly ITarjetaDeCreditoService _tarjetaDeCreditoService;

    public TarjetaDeCreditoController(ITarjetaDeCreditoService tarjetaDeCreditoService)
    {
        _tarjetaDeCreditoService = tarjetaDeCreditoService;
    }

    [HttpGet]
    public IEnumerable<TarjetaDeCredito> Get()
    {
        return _tarjetaDeCreditoService.GetAll();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var tarjetaDeCredito = _tarjetaDeCreditoService.GetById(id);
        if (tarjetaDeCredito == null)
        {
            return NotFound();
        }
        return Ok(tarjetaDeCredito);
    }

    [HttpPost]
    public IActionResult Post(TarjetaDeCreditoModel tarjetaDeCreditoModel)
    {
        Console.WriteLine("NombreTitular: ");
        Console.WriteLine(tarjetaDeCreditoModel.NombreTitular);
        if (string.IsNullOrEmpty(tarjetaDeCreditoModel.NombreTitular))
        {
            return BadRequest("NombreTitular is required.");
        }

        _tarjetaDeCreditoService.Create(tarjetaDeCreditoModel);

        return CreatedAtAction(nameof(Get), new { /* id = tarjetaDeCredito.TarjetaID */ }, null);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TarjetaDeCredito tarjetaDeCredito)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != tarjetaDeCredito.TarjetaID)
        {
            return BadRequest();
        }
        _tarjetaDeCreditoService.Update(tarjetaDeCredito);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var tarjetaDeCredito = _tarjetaDeCreditoService.GetById(id);
        if (tarjetaDeCredito == null)
        {
            return NotFound();
        }
        _tarjetaDeCreditoService.Delete(id);
        return Ok(tarjetaDeCredito);
    }
}

namespace backend.Models
{
    public class TarjetaDeCreditoModel
    {
        [Required]
        [StringLength(100)]
        public required string NombreTitular { get; set; }

        [Required]
        [StringLength(16)]
        public required string NumeroTarjeta { get; set; }

        [Required]
        public decimal SaldoActual { get; set; }

        [Required]
        public decimal LimiteCredito { get; set; }
    }
}