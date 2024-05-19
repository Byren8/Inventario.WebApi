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
    public class AsignacionActivosController : ControllerBase
    {
        private readonly ModelContext asignacion;

        public AsignacionActivosController(ModelContext asignacion)
        {
            this.asignacion = asignacion;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvAsignacionActivo>>> Listar()
        {
            return await asignacion.InvAsignacionActivos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvAsignacionActivo>> BuscarPorId(int id)
        {
            var resultado = await asignacion.InvAsignacionActivos.FindAsync(id);

            if (resultado != null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<InvAsignacionActivo>> Guardar(InvAsignacionActivo a)
        {
            try
            {
                asignacion.InvAsignacionActivos.AddAsync(a);
                await asignacion.SaveChangesAsync();
                a.AsiIdAsignacion = await asignacion.InvAsignacionActivos.MaxAsync(x => x.AsiIdAsignacion);
                return a;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvAsignacionActivo>> Actualizar(InvAsignacionActivo a)
        {
            if (a == null || a.AsiIdAsignacion == 0)
                return BadRequest("Id No Tiene Datos");
            InvAsignacionActivo cat = await asignacion.InvAsignacionActivos.FirstOrDefaultAsync(x => x.AsiIdAsignacion == a.AsiIdAsignacion);
            
            if (cat == null)
                return NotFound();

            try
            {
                cat.EmpIdEmpleado = a.EmpIdEmpleado;
                cat.AsiFechaAsignacion = a.AsiFechaAsignacion;

                asignacion.InvAsignacionActivos.Update(cat);
                await asignacion.SaveChangesAsync();
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
            InvAsignacionActivo cat = await asignacion.InvAsignacionActivos.FindAsync(id);
            if (asignacion == null)
                return NotFound();

            try
            {
                asignacion.InvAsignacionActivos.Remove(cat);
                await asignacion.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
