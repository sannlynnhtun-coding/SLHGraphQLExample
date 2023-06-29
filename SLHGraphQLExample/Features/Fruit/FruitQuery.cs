using GraphQL20221004.EfDbContext;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL20221004.Features.Fruit
{
    public class FruitQuery
    {
        public async Task<List<FruitModel>> Fruits([Service] AppDbContext db,
            int pageNo = 1, int pageSize = 4)
        {
            return await db.Fruits
                    .OrderBy(x=> x.Fruit_Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<FruitModel> Fruit([Service] AppDbContext db, int id)
        {
            return await db.Fruits.FirstOrDefaultAsync(x => x.Fruit_Id == id);
        }

        public async Task<FruitModel> AddFruit([Service] AppDbContext db, FruitModel reqModel)
        {
            await db.Fruits.AddAsync(reqModel);
            await db.SaveChangesAsync();
            return reqModel;
        }

        public async Task<FruitModel> UpdateFruit([Service] AppDbContext db, int id, 
            FruitModel reqModel)
        {
            var item = await db.Fruits.FirstOrDefaultAsync(x=> x.Fruit_Id == id);
            FruitModel model = new FruitModel
            {
                Fruit_Id = reqModel.Fruit_Id,
                Fruit_Name = reqModel.Fruit_Name,
            };

            db.Entry(item).State = EntityState.Modified;
            db.Fruits.Update(item);
            await db.SaveChangesAsync();
            return model;
        }

        public async Task<FruitModel> PatchFruit([Service] AppDbContext db, int id, 
            FruitModel reqModel)
        {
            var item = await db.Fruits.FirstOrDefaultAsync(x => x.Fruit_Id == id);
            if (!string.IsNullOrEmpty(reqModel.Fruit_Id.ToString()))
                item.Fruit_Id = reqModel.Fruit_Id;
            if(!string.IsNullOrEmpty(reqModel.Fruit_Name))
                item.Fruit_Name= reqModel.Fruit_Name;
            db.Entry(item).State= EntityState.Modified;
            db.Fruits.Update(item);
            await db.SaveChangesAsync();
            return await db.Fruits.FirstOrDefaultAsync(x => x.Fruit_Id == id);
        }

        public async Task<int> DeleteFruit([Service] AppDbContext db, int id)
        {
            var item = await db.Fruits.FirstOrDefaultAsync(x => x.Fruit_Id == id);
            db.Entry(item).State = EntityState.Deleted;
            db.Fruits.Remove(item);
            int result = await db.SaveChangesAsync();
            return result;
        }
    }
}
