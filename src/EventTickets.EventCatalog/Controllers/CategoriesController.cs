using EventTickets.EventCatalog.Contexts;
using EventTickets.EventCatalog.Models;
using EventTickets.EventCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var result = await _categoryRepository.GetAsync();
            return Ok(result.Select(x => new CategoryDto
            {
                CategoryId = x.CategoryId,
                Name = x.Name
            }));
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> Get(Guid categoryId)
        {
            var result = await _categoryRepository.GetByIdAsync(categoryId);
            return Ok(new CategoryDto
            {
                CategoryId = result.CategoryId,
                Name  = result.Name
            });
        }
    }
}
