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
    public class ProductoController : ControllerBase
    {
        private readonly ModelContext producto;

        public ProductoController(ModelContext producto)
        {
            this.producto = producto;
        }

        [HttpGet]
        public async Task<List<InvProducto>> Listar()
        {
            return await producto.InvProductos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvProducto>> BuscarPorId(int id)
        {
            var resultado = await producto.InvProductos.FirstOrDefaultAsync(x => x.ProIdProducto == id);
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
        public async Task<ActionResult<InvProducto>> Guardar(InvProducto p)
        {
            try
            {
                await producto.InvProductos.AddAsync(p);
                await producto.SaveChangesAsync();
                p.ProIdProducto = await producto.InvProductos.MaxAsync(x => x.ProIdProducto);
                return p;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvProducto>> Actualizar(InvProducto p)
        {
            if (p == null || p.ProIdProducto == 0)
                return BadRequest("Id No Tiene Datos");
            InvProducto cat = await producto.InvProductos.FirstOrDefaultAsync(x => x.ProIdProducto == p.ProIdProducto);
            if (cat == null)
                return NotFound();
            try
            {
                cat.PrvIdProveedor = p.PrvIdProveedor;
                cat.ProNombreProducto = p.ProNombreProducto;
                cat.ProPrecioDeCompra = p.ProPrecioDeCompra;
                cat.ProPrecioVenta = p.ProPrecioVenta;
                cat.ProStockMinimo = p.ProStockMinimo;
                cat.ProStockMaximo = p.ProStockMaximo;
                cat.ProImagenDeProducto = p.ProImagenDeProducto;

                producto.InvProductos.Update(cat);
                await producto.SaveChangesAsync();
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
            InvProducto cat = await producto.InvProductos.FirstOrDefaultAsync(x => x.ProIdProducto == id);
            if (cat == null)
                return NotFound();
            try
            {
                producto.InvProductos.Remove(cat);
                await producto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
