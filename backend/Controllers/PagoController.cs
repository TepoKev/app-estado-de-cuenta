using System.Collections.Generic;
using backend.services.interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService)
    {
        _pagoService = pagoService;
    }

    [HttpGet]
    public IEnumerable<Pago> Get()
    {
        return _pagoService.GetAll();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var pago = _pagoService.GetById(id);
        if (pago == null)
        {
            return NotFound();
        }
        return Ok(pago);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pago pago)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _pagoService.Create(pago);
        return CreatedAtAction(nameof(Get), new { id = pago.PagoID }, pago);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Pago pago)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != pago.PagoID)
        {
            return BadRequest();
        }
        _pagoService.Update(pago);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pago = _pagoService.GetById(id);
        if (pago == null)
        {
            return NotFound();
        }
        _pagoService.Delete(id);
        return Ok(pago);
    }
}
