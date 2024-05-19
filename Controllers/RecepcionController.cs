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
    public class RecepcionController : ControllerBase
    {
        private readonly ModelContext recepcion;

        public RecepcionController(ModelContext recepcion)
        {
            this.recepcion = recepcion;
        }

        [HttpGet]
        public async Task<List<InvRecepcion>> Listar()
        {
            return await recepcion.InvRecepcions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvRecepcion>> BuscarPorId(int id)
        {
            var resultado = await recepcion.InvRecepcions.FirstOrDefaultAsync(x => x.RecIdRecepcion == id);
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
        public async Task<ActionResult<InvRecepcion>> Guardar(InvRecepcion r)
        {
            try
            {
                await recepcion.InvRecepcions.AddAsync(r);
                await recepcion.SaveChangesAsync();
                r.RecIdRecepcion = await recepcion.InvRecepcions.MaxAsync(x => x.RecIdRecepcion);
                return r;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvRecepcion>> Actualizar(InvRecepcion r)
        {
            if (r == null || r.RecIdRecepcion == 0)
                return BadRequest("Id No Tiene Datos");
            InvRecepcion rec = await recepcion.InvRecepcions.FirstOrDefaultAsync(x => x.RecIdRecepcion == r.RecIdRecepcion);
            if (rec == null)
                return NotFound();
            try
            {
                rec.PrvIdProveedor = r.PrvIdProveedor;
                rec.RecFechaRecepcion = r.RecFechaRecepcion;
                rec.RecTotalFactura = r.RecTotalFactura;

                recepcion.InvRecepcions.Update(rec);
                await recepcion.SaveChangesAsync();
                return rec;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvRecepcion rec = await recepcion.InvRecepcions.FirstOrDefaultAsync(x => x.RecIdRecepcion == id);
            if (rec == null)
                return NotFound();
            try
            {
                recepcion.InvRecepcions.Remove(rec);
                await recepcion.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
