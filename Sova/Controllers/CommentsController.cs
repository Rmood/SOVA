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

        [HttpGet(Name = nameof(GetComments))]
        public IActionResult GetComments(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetComments(resourceParameters);
            var result = Mapper.Map<IEnumerable<CommentListModel>>(data);
            var totalCount = _dataService.GetCommentsCount();
            var linkedResult = new
            {
                Result = result,
                Links = CreateLinks(resourceParameters, nameof(GetComments), totalCount)
            };
            return Ok(linkedResult);
        }

        private List<LinkModel> CreateLinks(ResourceParameters resourceParameters, string route, int count)
        {
            var links = new List<LinkModel>
            {
                new LinkModel
                {
                    Href = Url.Link(route, new {resourceParameters.PageNumber, resourceParameters.PageSize}),
                    Rel = "self",
                    Method = "GET"
                }
            };

            if (resourceParameters.PageNumber > 1)
            {
                links.Add(new LinkModel
                {
                    Href = Url.Link(route, new {PageNumber = resourceParameters.PageNumber -1, resourceParameters.PageSize}),
                    Rel = "prev_page",
                    Method = "GET"
                });
            }
            var totalPages = (int) Math.Ceiling(count / (double) resourceParameters.PageSize);
            if (resourceParameters.PageNumber < totalPages)
            {
                links.Add(new LinkModel
                {
                    Href = Url.Link(route, new {PageNumber = resourceParameters.PageNumber +1, resourceParameters.PageSize}),
                    Rel = "next_page",
                    Method = "GET"
                });
            }
            return links;
        }

        [HttpGet("{id}", Name = nameof(GetComment))]
        public IActionResult GetComment(int id)
        {
            var comment = _dataService.GetComment(id);
            if (comment == null) return NotFound();
            var model = Mapper.Map<CommentModel>(comment);
            return Ok(model);
        }


    }
}
