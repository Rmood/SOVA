using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SovaDatabase;
using DataAccessLayer;
using Sova.Models;
using AutoMapper;
using DomainModel;

namespace Sova.Controllers
{
    [Route("api/comments")]
    public class CommentsController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public CommentsController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetComments))]
        public IActionResult GetComments(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetComments(resourceParameters);
            // var result = Mapper.Map<IEnumerable<CommentListModel>>(data);
            
            var linkedResult = new
            {
                //Result = result,
                //Result = data.Select(CreateLinks),
                //Values = data.Select(x => CreateLinks<CommentListModel>(x, Url)),
                Values = data.Select(CreateLinks<CommentListModel>),
                Links = CreateLinks(data, nameof(GetComments))
            };
            return Ok(linkedResult);
        }

        [HttpGet("{id}", Name = nameof(GetComment))]
        public IActionResult GetComment(int id)
        {
            var comment = _dataService.GetComment(id);
            if (comment == null) return NotFound();
            //var model = Mapper.Map<CommentModel>(comment);
            //return Ok(model);
            return Ok(CreateLinks<CommentModel>(comment));
        }

        //////////////////////////////////////////////
        /// 
        /// Helper Methods
        /// 
        //////////////////////////////////////////////

        private List<LinkModel> CreateLinks(PagedList<Comment> data, string route)
        {
            var links = new List<LinkModel>
            {
                new LinkModel
                {
                    Href = _urlHelper.Link(route, new {data.CurrentPage, data.PageSize}),
                    Rel = "self",
                    Method = "GET"
                }
            };

            if (data.hasPrev)
            {
                links.Add(new LinkModel
                {
                    Href = _urlHelper.Link(route, new {PageNumber = data.CurrentPage - 1, data.PageSize}),
                    Rel = "prev_page",
                    Method = "GET"
                });
            }

            if (data.hasNext)
            {
                links.Add(new LinkModel
                {
                    Href = _urlHelper.Link(route, new {pageNumber = data.CurrentPage + 1, data.PageSize}),
                    Rel = "next_page",
                    Method = "GET"
                });
            }

            return links;
        }

        private T CreateLinks<T>(Comment comment) where T : LinkedResourceModel
        {
            var model = Mapper.Map<T>(comment);
            model.Url = _urlHelper.Link(nameof(GetComment), new {comment.Id});

            return model;
        }

    }
}
