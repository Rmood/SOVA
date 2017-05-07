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
            return Ok(value: CreateLinkedResult(data));
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

        private object CreateLinkedResult(PagedList<Comment> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<CommentListModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<Comment> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetComments), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.hasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetComments), new
                {
                    PageNumber = data.CurrentPage -1, data.PageSize
                }, "prev_page", "GET"));
            }

            if (data.hasNext)
            {
                links.Add(CreateLinkModel(nameof(GetComments), new
                {
                    PageNumber = data.CurrentPage +1, data.PageSize}, "next_page", "GET"));
            }
            return links;
        }

        private T CreateLinks<T>(Comment comment) where T : LinkedResourceModel
        {
            var routeValues = new {comment.Id};
            var model = Mapper.Map<T>(comment);
            model.Url = CreateUrl(nameof(GetComment), routeValues);

            return model;
        }

        private LinkModel CreateLinkModel(string routeString, object routeValues, string rel, string method)
        {
            return new LinkModel
            {
                Href = CreateUrl(routeString, routeValues),
                Rel = rel,
                Method = method
            };
        }

        private string CreateUrl(string routeString, object routeValues)
        {
            return _urlHelper.Link(routeString, routeValues);
        }

    }
}
