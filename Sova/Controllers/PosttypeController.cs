using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Sova.Models;
using AutoMapper;
using DomainModel;


namespace Sova.Controllers
{
    [Route("api/posttype")]
    public class PosttypeController : Controller
    {
        private readonly IDataService _dataService;

        public PosttypeController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetPosttypes()
        {
            var data = _dataService.GetPosttypes();
            var result = Mapper.Map <IEnumerable<PosttypeModel>>(data);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPosttype")]
        public IActionResult GetPosttype(int id)
        {
            var posttype = _dataService.GetPosttype(id);
            if (posttype == null) return NotFound();
            var model = Mapper.Map<PosttypeModel>(posttype);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreatePosttype([FromBody]PosttypeCreateModel model)
        {
            if (model == null) return BadRequest();
            var posttype = Mapper.Map<Posttype>(model);
            _dataService.CreatePosttype(posttype);
            return CreatedAtRoute(null, null, Mapper.Map<PosttypeModel>(posttype));
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePosttype(int id, [FromBody] PosttypeCreateModel model)
        {
            if (model == null) return BadRequest();
            var posttype = _dataService.GetPosttype(id);
            if (posttype == null) return NotFound();
            Mapper.Map(model, posttype);
            _dataService.UpdatePosttype(posttype);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePosttype(int id)
        {
            var posttype = _dataService.GetPosttype(id);
            if (posttype == null) return NotFound();
            _dataService.DeletePosttype(posttype);
            return NoContent();
        }


    }
}
