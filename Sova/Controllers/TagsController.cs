using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SovaDatabase;
using DataAccessLayer;
using Sova.Models;

namespace Sova.Controllers
{
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly IDataService _dataService;

        public TagsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetTags()
        {
            var data = _dataService.GetTags();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = _dataService.GetTag(id);
            if (tag == null) return NotFound();
            var model = new TagModel()
            {
                Id = tag.Id,
                keyword = tag.keyword
            };
            return Ok(model);
        }

    }
}
