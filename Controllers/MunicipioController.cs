using Inventario.DataAcces_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.WebApi_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        public readonly ModelContext municipio;

        public MunicipioController(ModelContext municipio)
        {
            this.municipio = municipio;
        }

        [HttpGet]
        public async Task<List<InvMunicipio>> Listar()
        {
            return await municipio.InvMunicipios.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvMunicipio>> BuscarPorId(int Id)
        {
            var resultado = await municipio.InvMunicipios.FirstOrDefaultAsync(x => x.MunIdMunicipio == Id);
            if(resultado != null)
            {
                return resultado;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvMunicipio>> Guardar(InvMunicipio m)
        {
            try
            {
                await municipio.InvMunicipios.AddAsync(m);
                await municipio.SaveChangesAsync();
                m.MunIdMunicipio = await municipio.InvMunicipios.MaxAsync(x => x.MunIdMunicipio );
                return m;

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvMunicipio>> Actualizar(InvMunicipio m)
        {
            if (m == null || m.MunIdMunicipio == 0)
                return BadRequest("Id No Tiene Datos");
            InvMunicipio cat = await municipio.InvMunicipios.FirstOrDefaultAsync(x => x.MunIdMunicipio == m.MunIdMunicipio);
            if (cat == null)
                return NotFound();
            try
            {
                cat.MunDescMunicipio = m.MunDescMunicipio;
                municipio.InvMunicipios.Update(cat);
                await municipio.SaveChangesAsync();
                return cat;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            InvMunicipio cat = await municipio.InvMunicipios.FirstOrDefaultAsync(x => x.MunIdMunicipio == Id);
            if (cat == null)
                return NotFound();
            try
            {
                municipio.InvMunicipios.Remove(cat);
                await municipio.SaveChangesAsync();
                return true;

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
