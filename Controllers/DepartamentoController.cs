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
    public class DepartamentoController : ControllerBase
    {
        private readonly ModelContext departamento;

        public DepartamentoController(ModelContext departamento)
        {
            this.departamento = departamento;
        }

        [HttpGet]
        public async Task<List<InvDepartamento>> Listar()
        {
            return await departamento.InvDepartamentos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvDepartamento>> BuscarPorId(int id)
        {
            var resultado = await departamento.InvDepartamentos.FirstOrDefaultAsync(x => x.DepIdDepartamento == id);
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
        public async Task<ActionResult<InvDepartamento>> Guardar(InvDepartamento d)
        {
            try
            {
                await departamento.InvDepartamentos.AddAsync(d);
                await departamento.SaveChangesAsync();
                d.DepIdDepartamento = await departamento.InvDepartamentos.MaxAsync(x => x.DepIdDepartamento);
                return d;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvDepartamento>> Actualizar(InvDepartamento d)
        {
            if (d == null || d.DepIdDepartamento == 0)
                return BadRequest("Id No Tiene Datos");
            InvDepartamento dep = await departamento.InvDepartamentos.FirstOrDefaultAsync(x => x.DepIdDepartamento == d.DepIdDepartamento);
            if (dep == null)
                return NotFound();
            try
            {
                dep.MunIdMunicipio = d.MunIdMunicipio;
                dep.DepNombre = d.DepNombre;
                dep.DepCodigoPostal = d.DepCodigoPostal;

                departamento.InvDepartamentos.Update(dep);
                await departamento.SaveChangesAsync();
                return dep;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            InvDepartamento dep = await departamento.InvDepartamentos.FirstOrDefaultAsync(x => x.DepIdDepartamento == id);
            if (dep == null)
                return NotFound();
            try
            {
                departamento.InvDepartamentos.Remove(dep);
                await departamento.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
