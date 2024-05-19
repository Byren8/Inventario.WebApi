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
    public class EmpleadoController : ControllerBase
    {
        private readonly ModelContext empleado;

        public EmpleadoController(ModelContext empleado)
        {
            this.empleado = empleado;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvEmpleado>>> Listar()
        {
            return await empleado.InvEmpleados.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvEmpleado>> BuscarPorId(int Id)
        {
            
            var resultado = await empleado.InvEmpleados.FirstOrDefaultAsync(x => x.EmpIdEmpleado == Id);

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
        public async Task<ActionResult<InvEmpleado>> Guardar(InvEmpleado e)
        {
            try
            {
                
                await empleado.InvEmpleados.AddAsync(e);
                await empleado.SaveChangesAsync();
                e.EmpIdEmpleado = await empleado.InvEmpleados.MaxAsync(x => x.EmpIdEmpleado);
                return e;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvEmpleado>> Actualizar(InvEmpleado e)
        {
            if (e == null || e.EmpIdEmpleado == 0)
                return BadRequest("Id No Tiene Datos");

            InvEmpleado cat = await empleado.InvEmpleados.FirstOrDefaultAsync(x => x.EmpIdEmpleado == e.EmpIdEmpleado);
            if (cat == null)
                return NotFound();

            try
            {
                cat.PueIdPuesto = e.PueIdPuesto;
                cat.EmpTelefono = e.EmpTelefono;
                cat.EmpNombre = e.EmpNombre;
                cat.EmpApellido = e.EmpApellido;
                cat.EmpCodigoEmpleado = e.EmpCodigoEmpleado;

                empleado.InvEmpleados.Update(cat);
                await empleado.SaveChangesAsync();
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
            InvEmpleado cat = await empleado.InvEmpleados.FirstOrDefaultAsync(x => x.EmpIdEmpleado == Id);
            if (cat == null)
                return NotFound();

            try
            {
                empleado.InvEmpleados.Remove(cat);
                await empleado.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

