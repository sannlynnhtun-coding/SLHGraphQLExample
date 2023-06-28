using GraphQL20221004.EfDbContext;
using GraphQL20221004.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;

namespace GraphQL20221004.Queries
{
    public class BlogQuery
    {
        public async Task<List<BlogModel>> Blogs([Service] AppDbContext db, int pageNo, int pageSize)
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            return await db.Blogs.OrderByDescending(x => x.Blog_Id)
                .Skip(skipRowCount)
                .Take(pageSize).ToListAsync();
        }

        public async Task<BlogModel> Blog([Service] AppDbContext db, int id)
        {
            return await db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        }

        public async Task<BlogModel> AddBlog([Service] AppDbContext db, BlogModel reqModel)
        {
            await db.Blogs.AddAsync(reqModel);
            await db.SaveChangesAsync();
            return reqModel;
        }

        public async Task<BlogModel> UpdateBlog([Service] AppDbContext db, int id, BlogModel reqModel)
        {
            reqModel.Blog_Id = id;
            db.Blogs.Update(reqModel);
            await db.SaveChangesAsync();
            return reqModel;
        }

        public async Task<BlogModel> PatchBlog([Service] AppDbContext db, int id, BlogModel reqModel)
        {
            reqModel.Blog_Id = id;
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (!string.IsNullOrEmpty(reqModel.Blog_Title))
                item.Blog_Author = reqModel.Blog_Title;
            if (!string.IsNullOrEmpty(reqModel.Blog_Author))
                item.Blog_Author = reqModel.Blog_Author;
            if (!string.IsNullOrEmpty(reqModel.Blog_Content))
                item.Blog_Author = reqModel.Blog_Content;
            db.Blogs.Update(item);
            await db.SaveChangesAsync();
            item = await db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            return item;
        }

        public async Task<int> DeleteBlog([Service] AppDbContext db, int id)
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            db.Blogs.Remove(item);
            return await db.SaveChangesAsync();
        }
    }
}
