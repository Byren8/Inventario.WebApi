using Inventario.DataAcces_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.WebApi_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoSalidaController : ControllerBase
    {
        private readonly ModelContext documentosalida;

        public TipoDocumentoSalidaController(ModelContext documentosalida)
        {
            documentosalida = documentosalida;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvTipoDocumentoSalida>>> Listar()
        {
            return await documentosalida.InvTipoDocumentoSalida.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvTipoDocumentoSalida>> BuscarPorId(int id)
        {
            var tipoDocumento = await documentosalida.InvTipoDocumentoSalida.FindAsync(id);

            if (tipoDocumento == null)
            {
                return NotFound();
            }

            return tipoDocumento;
        }

        [HttpPost]
        public async Task<ActionResult<InvTipoDocumentoSalida>> Guardar(InvTipoDocumentoSalida tipoDocumento)
        {
            try
            {
                documentosalida.InvTipoDocumentoSalida.Add(tipoDocumento);
                await documentosalida.SaveChangesAsync();
                tipoDocumento.TipIdDocumento = await documentosalida.InvTipoDocumentoSalida.MaxAsync(x => x.TipIdDocumento);
                return tipoDocumento;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvTipoDocumentoSalida>> Actualizar(InvTipoDocumentoSalida d)
        {
            if (d == null || d.TipIdDocumento == 0)
                return BadRequest("Id No Tiene Datos");

            InvTipoDocumentoSalida cat = await documentosalida.InvTipoDocumentoSalida.FirstOrDefaultAsync(x => x.TipIdDocumento == d.TipIdDocumento);
            if (cat == null)
                return NotFound();

            try
            {
                cat.TipDescripcionDocumento = d.TipDescripcionDocumento;
                documentosalida.InvTipoDocumentoSalida.Update(cat);
                await documentosalida.SaveChangesAsync();
                return cat;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
             InvTipoDocumentoSalida cat = await documentosalida.InvTipoDocumentoSalida.FirstOrDefaultAsync(x => x.TipIdDocumento == id);
            if (cat == null)
                return NotFound();

            try
            {
                documentosalida.InvTipoDocumentoSalida.Remove(cat);
                await documentosalida.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

