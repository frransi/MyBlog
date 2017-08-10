using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using blog.ViewModels;

namespace blog.Controllers
{
    [Authorize]
    public class BlogPostController : Controller
    {
        private readonly IBlogContext _blogContext;

        public BlogPostController(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            var post = _blogContext.Posts.Find(id);
            _blogContext.Posts.Remove(post);
            _blogContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult NewPost()
        {
            return View("NewPost");
        }

        [HttpPost]
        public IActionResult NewPost(SavePostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _blogContext.Users.FirstOrDefault(uss => uss.Name == User.Identity.Name);
                if (user != null)
                {
                    var post = new Post() { Title = viewModel.Title, Text = viewModel.Content, UserId = user.UserId };
                    _blogContext.Posts.Add(post);
                    _blogContext.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Fuck!");
                    return View(viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _blogContext.Posts.Find(id);
            var viewModel = new EditPostViewModel(){Content = post.Text, Title = post.Title, Id = post.Id};

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var post = _blogContext.Posts.Find(viewModel.Id);
                post.Title = viewModel.Title;
                post.Text = viewModel.Content;
                _blogContext.Posts.Update(post);
                _blogContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
