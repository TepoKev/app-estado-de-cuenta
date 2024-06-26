using Microsoft.AspNetCore.Mvc;
using backend.services.interfaces;
[Route("api/[controller]")]
[ApiController]
public class CompraController : ControllerBase
{
    private readonly ICompraService _compraService;

    public CompraController(ICompraService compraService)
    {
        _compraService = compraService;
    }

    [HttpGet]
    public IEnumerable<Compra> Get()
    {
        return _compraService.GetAll();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var compra = _compraService.GetById(id);
        if (compra == null)
        {
            return NotFound();
        }
        return Ok(compra);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Compra compra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _compraService.Create(compra);
        return CreatedAtAction(nameof(Get), new { id = compra.CompraID }, compra);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Compra compra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != compra.CompraID)
        {
            return BadRequest();
        }
        _compraService.Update(compra);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var compra = _compraService.GetById(id);
        if (compra == null)
        {
            return NotFound();
        }
        _compraService.Delete(id);
        return Ok(compra);
    }
}
