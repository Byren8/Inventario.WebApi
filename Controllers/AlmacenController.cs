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
    public class AlmacenController : ControllerBase
    {
        private readonly ModelContext almacen;

        public AlmacenController(ModelContext almacen)
        {
            this.almacen = almacen;
        }

        [HttpGet]
        public async Task<List<InvAlmacen>> Listar()
        {
            return await almacen.InvAlmacens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvAlmacen>> BuscarPorId(int id)
        {
            var resultado = await almacen.InvAlmacens.FirstOrDefaultAsync(x => x.AlmIdAlmacen == id);
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
        public async Task<ActionResult<InvAlmacen>> Guardar(InvAlmacen a)
        {
            try
            {
                await almacen.InvAlmacens.AddAsync(a);
                await almacen.SaveChangesAsync();
                a.AlmIdAlmacen = await almacen.InvAlmacens.MaxAsync(x => x.AlmIdAlmacen);
                return a;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvAlmacen>> Actualizar(InvAlmacen a)
        {
            if (a == null || a.AlmIdAlmacen == 0)
                return BadRequest("Id No Tiene Datos");
            InvAlmacen alm = await almacen.InvAlmacens.FirstOrDefaultAsync(x => x.AlmIdAlmacen == a.AlmIdAlmacen);
            if (alm == null)
                return NotFound();
            try
            {
                alm.AlmNombreAlmacen = a.AlmNombreAlmacen;
                alm.AlmDireccion = a.AlmDireccion;

                almacen.InvAlmacens.Update(alm);
                await almacen.SaveChangesAsync();
                return alm;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvAlmacen alm = await almacen.InvAlmacens.FirstOrDefaultAsync(x => x.AlmIdAlmacen == id);
            if (alm == null)
                return NotFound();
            try
            {
                almacen.InvAlmacens.Remove(alm);
                await almacen.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
