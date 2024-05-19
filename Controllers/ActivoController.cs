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
    public class ActivoController : ControllerBase
    {
        private readonly ModelContext activo;

        public ActivoController(ModelContext activo)
        {
            this.activo = activo;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvActivo>>> Listar()
        {
            return await activo.InvActivos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvActivo>> BuscarPorId(int id)
        {
            var resultado = await activo.InvActivos.FindAsync(id);

            if (resultado != null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<InvActivo>> Guardar(InvActivo ac)
        {
            try
            {
                activo.InvActivos.AddAsync(ac);
                await activo.SaveChangesAsync();
                ac.ActIdActivos = await activo.InvActivos.MaxAsync(x => x.ActIdActivos);
                return ac;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvActivo>> Actualizar(InvActivo ac)
        {
            if (ac == null || ac.ActIdActivos == 0)
                return BadRequest("Id No Tiene Datos");
       
            InvActivo cat = await activo.InvActivos.FirstOrDefaultAsync(x => x.ActIdActivos == ac.ActIdActivos);
            if (cat == null)
                return NotFound();

            try
            {
                cat.AsiIdAsignacion = ac.AsiIdAsignacion;
                cat.ActNombreActivo = ac.ActNombreActivo;
                cat.ActDescActivo = ac.ActDescActivo;
                cat.ActCantidadActivo = ac.ActCantidadActivo;
                cat.ActFechaAdquisicion = ac.ActFechaAdquisicion;
                cat.ActCostoActivo = ac.ActCostoActivo;

                activo.InvActivos.Update(cat);
                await activo.SaveChangesAsync();
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
            InvActivo cat = await activo.InvActivos.FindAsync(id);
            if (activo == null)
                return NotFound();

            try
            {
                activo.InvActivos.Remove(cat);
                await activo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
