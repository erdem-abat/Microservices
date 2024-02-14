using AutoMapper;
using Microservices.Services.ProductAPI.Data;
using Microservices.Services.ProductAPI.Models;
using Microservices.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProductAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _context.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product value = _context.Products.First(x => x.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(value);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto GetByCode(string name)
        {
            try
            {
                Product value = _context.Products.First(x => x.Name.ToLower() == name.ToLower());
                _response.Result = _mapper.Map<ProductDto>(value);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Create([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _context.Products.Add(obj);
                _context.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Update([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _context.Products.Update(obj);
                _context.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _context.Products.First(x=>x.ProductId == id);
                _context.Products.Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
