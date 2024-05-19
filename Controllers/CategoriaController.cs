using Inventario.DataAcces_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.WebApi_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        public readonly ModelContext categoria;

        public CategoriaController(ModelContext categoria)
        {
            this.categoria = categoria;
        }

        [HttpGet]
        public async Task<List<InvCategoria>> Listar()
        {
            return await categoria.InvCategorias.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvCategoria>> BuscarPorId(int Id)
        {
            var resultado = await categoria.InvCategorias.FirstOrDefaultAsync(x => x.CatIdCategoria == Id);
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
        public async Task<ActionResult<InvCategoria>> Guardar(InvCategoria c)
        {
            try
            {
                await categoria.InvCategorias.AddAsync(c);
                await categoria.SaveChangesAsync();
                c.CatIdCategoria = await categoria.InvCategorias.MaxAsync(x => x.CatIdCategoria);
                return c;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<InvCategoria>> Actualizar(InvCategoria c)
        {
            if (c == null || c.CatIdCategoria == 0)
                return BadRequest("Id No Tiene Datos");
            InvCategoria cat = await categoria.InvCategorias.FirstOrDefaultAsync(x => x.CatIdCategoria == c.CatIdCategoria);
            if (cat == null)
                return NotFound();
            try
            {
                cat.CatNombreCategoria = c.CatNombreCategoria;
                cat.CatDescCategoria = c.CatDescCategoria;
                categoria.InvCategorias.Update(cat);
                await categoria.SaveChangesAsync();
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
            InvCategoria cat = await categoria.InvCategorias.FirstOrDefaultAsync(x => x.CatIdCategoria == Id);
            if (cat == null)
                return NotFound();
            try
            {
                categoria.InvCategorias.Remove(cat);
                await categoria.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
