using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HelloWebApiCoreV2.Models;
using HelloWebApiCoreV2.Service;
using Microsoft.AspNetCore.Mvc;
using HelloWebApiCoreV2.Common.ApiPack;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWebApiCoreV2.Controllers
{
    [Route("api/v2/[controller]")]
    public class FruitController : Controller
    {
        IFruitService _fruitService;
        public FruitController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }
        // GET api/v2/fruit
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruitDtoList = await _fruitService.FruitQuery();
            if (fruitDtoList == null)
                return this.NoContentEx();
            return this.OkEx(fruitDtoList);
        }

        // GET api/v2/fruit/apple
        [HttpGet("{name}", Name = "GetFruit")]
        public async Task<IActionResult> Get(string name)
        {
            var fruitDtoList = await _fruitService.FruitQuery(name);
            if (fruitDtoList == null)
                return this.NoContentEx();
            return this.OkEx(fruitDtoList);
        }

        // POST api/v2/fruit/list
        [HttpPost("{list}")]
        public IActionResult Post([FromBody]List<FruitDto> fruitDtoList)
        {
            try
            {
                if (fruitDtoList == null)
                {
                    return this.BadRequestEx("fruitDtoList");
                }
                _fruitService.FruitAdd(fruitDtoList);
                return this.CreatedEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }

        }
        // POST api/v2/fruit
        [HttpPost]
        public IActionResult Post([FromBody]FruitDto fruitDto)
        {
            try
            {
                if (fruitDto == null)
                {
                    return this.BadRequestEx("fruitDto");
                }
                _fruitService.FruitAdd(new List<FruitDto>() { fruitDto });
                return this.CreatedEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }

        }

        // PUT api/v2/fruit/apple
        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody]FruitDto fruitDto)
        {
            try
            {
                if (fruitDto == null)
                {
                    return this.BadRequestEx("fruitDto");
                }

                var fruit = _fruitService.FruitQuery(name);
                if (fruit == null)
                {
                    return this.NoContentEx();
                }

                _fruitService.FruitUpdate(fruitDto);
                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }

        // DELETE api/v2/fruit/apple
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return this.BadRequestEx("name");
                }

                _fruitService.FruitDelete(name);

                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }
    }
}
