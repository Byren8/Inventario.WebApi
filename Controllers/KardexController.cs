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
    public class InventarioKardexController : ControllerBase
    {
        private readonly ModelContext inventarioKardex;

        public InventarioKardexController(ModelContext inventarioKardex)
        {
            this.inventarioKardex = inventarioKardex;
        }

        [HttpGet]
        public async Task<List<InvInventarioKardex>> Listar()
        {
            return await inventarioKardex.InvInventarioKardices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvInventarioKardex>> BuscarPorId(int id)
        {
            var resultado = await inventarioKardex.InvInventarioKardices.FirstOrDefaultAsync(x => x.IneIdInventario == id);
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
        public async Task<ActionResult<InvInventarioKardex>> Guardar(InvInventarioKardex i)
        {
            try
            {
                await inventarioKardex.InvInventarioKardices.AddAsync(i);
                await inventarioKardex.SaveChangesAsync();
                i.IneIdInventario = await inventarioKardex.InvInventarioKardices.MaxAsync(x => x.IneIdInventario);
                return i;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvInventarioKardex>> Actualizar(InvInventarioKardex i)
        {
            if (i == null || i.IneIdInventario == 0)
                return BadRequest("Id No Tiene Datos");
            InvInventarioKardex inv = await inventarioKardex.InvInventarioKardices.FirstOrDefaultAsync(x => x.IneIdInventario == i.IneIdInventario);
            if (inv == null)
                return NotFound();
            try
            {
                inv.InvDetalleRecepcion = i.InvDetalleRecepcion;
                inv.ProIdProducto = i.ProIdProducto;
                inv.AlmIdAlmacen = i.AlmIdAlmacen;
                inv.InvTipoDocumentoSalida = i.InvTipoDocumentoSalida;
                inv.PrvIdProveedor = i.PrvIdProveedor;
                inv.IneCantidadStock = i.IneCantidadStock;
                inv.IneFechaIngreso = i.IneFechaIngreso;
                inv.IneFechaSalida = i.IneFechaSalida;

                inventarioKardex.InvInventarioKardices.Update(inv);
                await inventarioKardex.SaveChangesAsync();
                return inv;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvInventarioKardex inv = await inventarioKardex.InvInventarioKardices.FirstOrDefaultAsync(x => x.IneIdInventario == id);
            if (inv == null)
                return NotFound();
            try
            {
                inventarioKardex.InvInventarioKardices.Remove(inv);
                await inventarioKardex.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
