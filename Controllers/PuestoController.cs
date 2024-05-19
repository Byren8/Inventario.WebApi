using Inventario.DataAcces_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.WebApi_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        private readonly ModelContext puesto;

        public PuestoController(ModelContext puesto)
        {
            this.puesto = puesto;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvPuesto>>> Listar()
        {
            return await puesto.InvPuestos.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvPuesto>> BuscarPorId(int Id)
        {
            var resultado = await puesto.InvPuestos.FirstOrDefaultAsync(x => x.PueIdPuesto == Id);

            if (resultado != null)
            {
                return resultado;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvPuesto>> Guardar(InvPuesto p)
        {
            try
            {
                await puesto.InvPuestos.AddAsync(p);
                await puesto.SaveChangesAsync();
                p.PueIdPuesto = await puesto.InvPuestos.MaxAsync(x => x.PueIdPuesto);
                return p;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvPuesto>> Actualizar(InvPuesto p)
        {
            if (p == null || p.PueIdPuesto == 0)
                return BadRequest("Id No Tiene Datos");
            
             InvPuesto cat = await puesto.InvPuestos.FirstOrDefaultAsync(x => x.PueIdPuesto == p.PueIdPuesto);
            if (cat == null)
                return NotFound();

            try
            {
                cat.PueDescDepto = p.PueDescDepto;
                puesto.InvPuestos.Update(cat);
                await puesto.SaveChangesAsync();
                return cat;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            InvPuesto cat = await puesto.InvPuestos.FirstOrDefaultAsync(x => x.PueIdPuesto == Id);
            if (cat == null)
                return NotFound();

            try
            {
                puesto.InvPuestos.Remove(cat);
                await puesto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
