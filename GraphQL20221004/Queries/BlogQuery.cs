using GraphQL20221004.EfDbContext;
using GraphQL20221004.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL20221004.Queries
{
    public class BlogQuery
    {
        public async Task<List<BlogModel>> GetList()
        {
            AppDbContext _db = new AppDbContext();
            return await _db.Blogs.OrderByDescending(x => x.Blog_Id).ToListAsync();
        }

        public async Task<List<BlogModel>> GetListPage(int pageNo, int rowCount)
        {
            AppDbContext _db = new AppDbContext();
            int skipRowCount = (pageNo - 1) * rowCount;
            return await _db.Blogs.OrderByDescending(x => x.Blog_Id)
                .Skip(skipRowCount)
                .Take(rowCount).ToListAsync();
        }

        public async Task<BlogModel> Get(int id)
        {
            AppDbContext _db = new AppDbContext();
            return await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        }

        public async Task<BlogModel> AddBlog(BlogModel reqModel)
        {
            AppDbContext _db = new AppDbContext();
            await _db.Blogs.AddAsync(reqModel);
            await _db.SaveChangesAsync();
            return reqModel;
        }

        public async Task<BlogModel> UpdateBlog(int id, BlogModel reqModel)
        {
            AppDbContext _db = new AppDbContext();
            reqModel.Blog_Id = id;
            _db.Blogs.Update(reqModel);
            await _db.SaveChangesAsync();
            return reqModel;
        }

        public async Task<BlogModel> PatchBlog(int id, BlogModel reqModel)
        {
            AppDbContext _db = new AppDbContext();
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

        public async Task<int> DeleteBlog(int id)
        {
            AppDbContext _db = new AppDbContext();
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            _db.Blogs.Remove(item);
            return await _db.SaveChangesAsync();
        }
    }
}
