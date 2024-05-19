using Inventario.DataAcces_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebApi_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrasladoAlmacenController : ControllerBase
    {
        private readonly ModelContext traslado;

        public TrasladoAlmacenController(ModelContext traslado)
        {
            this.traslado = traslado;
        }

        [HttpGet]
        public async Task<List<InvTrasladoAlmacen>> Listar()
        {
            return await traslado.InvTrasladoAlmacens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvTrasladoAlmacen>> BuscarPorId(int id)
        {
            var resultado = await traslado.InvTrasladoAlmacens.FirstOrDefaultAsync(x => x.TraIdTraslado == id);
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
        public async Task<ActionResult<InvTrasladoAlmacen>> Guardar(InvTrasladoAlmacen t)
        {
            try
            {
                await traslado.InvTrasladoAlmacens.AddAsync(t);
                await traslado.SaveChangesAsync();
                t.TraIdTraslado = await traslado.InvTrasladoAlmacens.MaxAsync(x => x.TraIdTraslado);
                return t;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvTrasladoAlmacen>> Actualizar(InvTrasladoAlmacen t)
        {
            if (t == null || t.TraIdTraslado == 0)
                return BadRequest("Id No Tiene Datos");
            InvTrasladoAlmacen tra = await traslado.InvTrasladoAlmacens.FirstOrDefaultAsync(x => x.TraIdTraslado == t.TraIdTraslado);
            if (tra == null)
                return NotFound();
            try
            {
                tra.DepIdDepartamento = t.DepIdDepartamento;
                tra.AlmIdAlmacen = t.AlmIdAlmacen;
                tra.TraNombreTraslado = t.TraNombreTraslado;
                tra.TraFechaTraslado = t.TraFechaTraslado;

                traslado.InvTrasladoAlmacens.Update(tra);
                await traslado.SaveChangesAsync();
                return tra;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvTrasladoAlmacen tra = await traslado.InvTrasladoAlmacens.FirstOrDefaultAsync(x => x.TraIdTraslado == id);
            if (tra == null)
                return NotFound();
            try
            {
                traslado.InvTrasladoAlmacens.Remove(tra);
                await traslado.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
