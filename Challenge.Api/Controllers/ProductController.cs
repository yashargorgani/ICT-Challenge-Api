using Challenge.Api.Context;
using Challenge.Api.Models;
using Challenge.Api.Services;
using Challenge.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Challenge.Api.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private ChallengeContext db;
        public ProductController()
        {
            db = new ChallengeContext();
        }

        //[HttpGet]
        // // GET: api/Product
        // public IQueryable<Product> GetProducts()
        // {
        //     return db.Products;
        // }

        [HttpGet]
        public async Task<IHttpActionResult>
            GetProducts(string q = "", int? CategoryId = null, int? CompanyId = null, int? BrandId = null)
        {
            var sp = new SearchParameters
            {
                q = q,
                CategoryId = CategoryId,
                ComapnyId = CompanyId,
                BrandId = BrandId
            };

            var entities = await LoadData(sp);

            return Ok(entities);
        }

        private async Task<IEnumerable<ProductViewModel>> LoadData(SearchParameters sp)
        {
            Expression<Func<Product, bool>> filter = PredicateBuilder.True<Product>();

            if (sp.CategoryId.HasValue)
                filter = filter.And(x => x.CategoryId == sp.CategoryId.Value);

            if (sp.ComapnyId.HasValue)
                filter = filter.And(x => x.CompanyId == sp.ComapnyId);

            if (sp.BrandId.HasValue)
                filter = filter.And(x => x.BrandId == sp.BrandId.Value);

            if (!string.IsNullOrEmpty(sp.q))
            {
                filter = filter.And(x => x.Company.Title.Contains(sp.q) ||
                                         x.Category.Title.Contains(sp.q) ||
                                         x.Brand.Title.Contains(sp.q));
            }

            var entities = await db.Products.Where(filter).Select(x => new ProductViewModel
            {
                Id = x.Id,

                CategoryId = x.CategoryId,
                CategoryTitle = x.Category.Title,

                CompanyId = x.CompanyId,
                CompanyTitle = x.Company.Title,

                BrandId = x.BrandId,
                BrandTitle = x.Brand.Title,

                Title = x.Title,
                CreateOn = x.CreateOn
            }).ToListAsync();

            return entities;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCompanies()
        {
            var entities = await db.Companies.Select(x => new CompanyViewModel
            {
                Id = x.Id,
                Title = x.Title,
                UniqueName = x.UniqueName,
                Address = x.Address,
                CreateOn = x.CreateOn
            }).ToListAsync();

            return Ok(entities);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCategories()
        {
            var entities = await db.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CreateOn = x.CreateOn
            }).ToListAsync();

            return Ok(entities);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBrands()
        {
            var entities = await db.Brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CreateDate = x.CreateDate
            }).ToListAsync();

            return Ok(entities);
        }
        // GET: api/Product/5
        [ResponseType(typeof(ProductViewModel))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var entity = await db.Products.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductViewModel
            {
                Id = entity.Id,

                CategoryId = entity.CategoryId,
                CategoryTitle = entity.Category.Title,

                CompanyId = entity.CompanyId,
                CompanyTitle = entity.Company.Title,

                BrandId = entity.BrandId,
                BrandTitle = entity.Brand.Title,

                Title = entity.Title,
                CreateOn = entity.CreateOn
            };

            return Ok(model);
        }

        public class SearchParameters
        {
            public int? CategoryId { get; set; }
            public int? ComapnyId { get; set; }
            public int? BrandId { get; set; }
            public string q { get; set; }
        }
    }
}