using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class BrandController : ApiController
    {
        private readonly BrandRepository brandRepository;

        public BrandController()
        {
            brandRepository = new BrandRepository();
        }               
        
        [Route("api/Brand/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var brands = brandRepository.GetAll();            
            return Ok(brands);
        }        

        [Route("api/Brand/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var brands = brandRepository.GetAllOrderBy(propertyName);
            return Ok(brands);
        }

        [Route("api/Brand/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var brand = brandRepository.GetById(id);
            return Ok(brand);
        }

        [Route("api/Brand/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var brands = brandRepository.GetAllByProperty(propertyName, propertyValue);
            return Ok(brands);
        }

        [Route("api/Brand/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var brands = brandRepository.GetAllByPropertyILike(propertyName, propertyValue);
            return Ok(brands);
        }

        [Route("api/Brand/Create"), HttpPost]
        public IHttpActionResult Create([FromBody]Brand brand)
        {            
            brandRepository.Add(brand);           
            brandRepository.SaveChanges();           
            return Ok(brand);
        }
                
        [Route("api/Brand/Update"), HttpPut]
        public IHttpActionResult Update(int id, [FromBody]Brand brand)
        {
            if (id != brand.Id)
                return BadRequest();
            
            brandRepository.Update(brand);
            brandRepository.SaveChanges();
            return Ok(brand);
        }
                
        [Route("api/Brand/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var brand = brandRepository.GetById(id);

            if (brand == null)
                return NotFound();

            brandRepository.Remove(brand);
            brandRepository.SaveChanges();
            return Ok(brand);
        }       
    }
}