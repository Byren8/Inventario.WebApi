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
    public class UnidadDeMedidaController : ControllerBase
    {
        private readonly ModelContext unidad;

        public UnidadDeMedidaController(ModelContext unidad)
        {
            this.unidad = unidad;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvUnidadDeMedidum>>> Listar()
        {
            return await unidad.InvUnidadDeMedidas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvUnidadDeMedidum>> BuscarPorId(int id)
        {
            var resultado = await unidad.InvUnidadDeMedidas.FindAsync(id);

            if (unidad != null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<InvUnidadDeMedidum>> Guardar(InvUnidadDeMedidum u)
        {
            try
            {
                unidad.InvUnidadDeMedidas.AddAsync(u);
                await unidad.SaveChangesAsync();
                u.UndIdUnidadDeMedida = await unidad.InvUnidadDeMedidas.MaxAsync(x => x.UndIdUnidadDeMedida);
                return u;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvUnidadDeMedidum>> Actualizar(InvUnidadDeMedidum u)
        {
            if (u == null || u.UndIdUnidadDeMedida == 0)
                return BadRequest("Id No Tiene Datos");

            InvUnidadDeMedidum cat = await unidad.InvUnidadDeMedidas.FirstOrDefaultAsync(x => x.UndIdUnidadDeMedida == u.UndIdUnidadDeMedida);
            if (cat == null)
                return NotFound();

            try
            {
                cat.CatIdCategoria = u.CatIdCategoria;
                cat.UndNombre = u.UndNombre;
                cat.UndAbreviatura = u.UndAbreviatura;

                unidad.InvUnidadDeMedidas.Update(cat);
                await unidad.SaveChangesAsync();
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
            InvUnidadDeMedidum cat = await unidad.InvUnidadDeMedidas.FindAsync(id);
            if (unidad == null)
                return NotFound();

            try
            {
                unidad.InvUnidadDeMedidas.Remove(cat);
                await unidad.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
