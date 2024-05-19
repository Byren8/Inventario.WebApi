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
    public class DetalleRecepcionController : ControllerBase
    {
        private readonly ModelContext detalle;

        public DetalleRecepcionController(ModelContext detalle)
        {
            this.detalle = detalle;
        }

        [HttpGet]
        public async Task<List<InvDetalleRecepcion>> Listar()
        {
            return await detalle.InvDetalleRecepcions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvDetalleRecepcion>> BuscarPorId(int id)
        {
            var resultado = await detalle.InvDetalleRecepcions.FirstOrDefaultAsync(x => x.DetIdDetalle == id);
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
        public async Task<ActionResult<InvDetalleRecepcion>> Guardar(InvDetalleRecepcion d)
        {
            try
            {
                await detalle.InvDetalleRecepcions.AddAsync(d);
                await detalle.SaveChangesAsync();
                d.DetIdDetalle = await detalle.InvDetalleRecepcions.MaxAsync(x => x.DetIdDetalle);
                return d;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvDetalleRecepcion>> Actualizar(InvDetalleRecepcion d)
        {
            if (d == null || d.DetIdDetalle == 0)
                return BadRequest("Id No Tiene Datos");
            InvDetalleRecepcion det = await detalle.InvDetalleRecepcions.FirstOrDefaultAsync(x => x.DetIdDetalle == d.DetIdDetalle);
            if (det == null)
                return NotFound();
            try
            {
                det.RecIdRecepcion = d.RecIdRecepcion;
                det.DetCantidadRecibida = d.DetCantidadRecibida;
                det.DetPrecioUnitarioCompra = d.DetPrecioUnitarioCompra;
                det.DetSubtotal = d.DetSubtotal;

                detalle.InvDetalleRecepcions.Update(det);
                await detalle.SaveChangesAsync();
                return det;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvDetalleRecepcion det = await detalle.InvDetalleRecepcions.FirstOrDefaultAsync(x => x.DetIdDetalle == id);
            if (det == null)
                return NotFound();
            try
            {
                detalle.InvDetalleRecepcions.Remove(det);
                await detalle.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
