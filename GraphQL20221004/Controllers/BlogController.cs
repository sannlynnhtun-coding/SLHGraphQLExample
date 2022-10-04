using GraphQL20221004.EfDbContext;
using GraphQL20221004.Models;
using HotChocolate.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL20221004.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        //Read
        [HttpGet]
        public async Task<List<BlogModel>> GetList()
        {
            return await _db.Blogs.OrderByDescending(x => x.Blog_Id).ToListAsync();
        }

        [HttpGet("{pageNo}/{rowCount}")]
        public async Task<List<BlogModel>> GetList(int pageNo, int rowCount)
        {
            int skipRowCount = (pageNo - 1) * rowCount;
            return await _db.Blogs.OrderByDescending(x => x.Blog_Id)
                .Skip(skipRowCount)
                .Take(rowCount).ToListAsync();
        }

        //Read By Id
        [HttpGet("{id}")]
        public async Task<BlogModel> Get(int id)
        {
            return await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        }

        //Create
        [HttpPost]
        public async Task<BlogModel> AddBlog(BlogModel reqModel)
        {
            await _db.Blogs.AddAsync(reqModel);
            await _db.SaveChangesAsync();
            return reqModel;
        }

        //Update
        [HttpPut("{id}")]
        public async Task<BlogModel> UpdateBlog(int id, BlogModel reqModel)
        {
            reqModel.Blog_Id = id;
            _db.Blogs.Update(reqModel);
            await _db.SaveChangesAsync();
            return reqModel;
        }

        //Patch
        [HttpPatch("{id}")]
        public async Task<BlogModel> PatchBlog(int id, BlogModel reqModel)
        {
            reqModel.Blog_Id = id;
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (!string.IsNullOrEmpty(reqModel.Blog_Title))
                item.Blog_Author = reqModel.Blog_Title;
            if (!string.IsNullOrEmpty(reqModel.Blog_Author))
                item.Blog_Author = reqModel.Blog_Author;
            if (!string.IsNullOrEmpty(reqModel.Blog_Content))
                item.Blog_Author = reqModel.Blog_Content;
            _db.Blogs.Update(item);
            await _db.SaveChangesAsync();
            item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            return item;
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<int> DeleteBlog(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            _db.Blogs.Remove(item);
            return await _db.SaveChangesAsync();
        }
    }
}
