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
    public class ProveedorController : ControllerBase
    {
        private readonly ModelContext proveedor;

        public ProveedorController(ModelContext proveedor)
        {
            this.proveedor = proveedor;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvProveedore>>> Listar()
        {
            return await proveedor.InvProveedores.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvProveedore>> BuscarPorId(int Id)
        {
       
            var resultado = await proveedor.InvProveedores.FirstOrDefaultAsync(x => x.PrvIdProveedor == Id);

            if (resultado != null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<InvProveedore>> Guardar(InvProveedore p)
        {
            try
            {
               
                await proveedor.InvProveedores.AddAsync(p);
                await proveedor.SaveChangesAsync();
                p.PrvIdProveedor = await proveedor.InvProveedores.MaxAsync(x => x.PrvIdProveedor);
                return p;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvProveedore>> Actualizar(InvProveedore p)
        {
            if (p == null || p.PrvIdProveedor == 0)
                return BadRequest("Id No Tiene Datos");

            InvProveedore cat = await proveedor.InvProveedores.FirstOrDefaultAsync(x => x.PrvIdProveedor == p.PrvIdProveedor);
            if (cat == null)
                return NotFound();

            try
            {
                cat.UndIdUnidadDeMedida = p.UndIdUnidadDeMedida;
                cat.PrvNombreProveedor = p.PrvNombreProveedor;
                cat.PrvDireccionProveedor = p.PrvDireccionProveedor;
                cat.PrvNitProveedor = p.PrvNitProveedor;

                proveedor.InvProveedores.Update(cat);
                await proveedor.SaveChangesAsync();
                return cat;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            InvProveedore cat = await proveedor.InvProveedores.FirstOrDefaultAsync(x => x.PrvIdProveedor == Id);
            
            if (cat == null)
                return NotFound();

            try
            {
                proveedor.InvProveedores.Remove(cat);
                await proveedor.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
