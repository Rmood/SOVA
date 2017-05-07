using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SovaDatabase;
using DataAccessLayer;
using Sova.Models;
using AutoMapper;

namespace Sova.Controllers
{
    [Route("api/comments")]
    public class CommentsController : Controller
    {
        private readonly IDataService _dataService;

        public CommentsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            var data = _dataService.GetComments();
            var result = Mapper.Map<IEnumerable<CommentListModel>>(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = _dataService.GetComment(id);
            if (comment == null) return NotFound();
            var model = Mapper.Map<CommentModel>(comment);
            return Ok(model);
        }

    }
}
