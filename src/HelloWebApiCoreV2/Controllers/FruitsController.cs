using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HelloWebApiCoreV2.Models;
using HelloWebApiCoreV2.Service;
using Microsoft.AspNetCore.Mvc;
using HelloWebApiCoreV2.Common.ApiPack;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWebApiCoreV2.Controllers
{
    [Route("v2/[controller]")]
    public class FruitsController : Controller
    {
        IFruitService _fruitService;
        public FruitsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }
        // GET api/v2/fruit
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int skipCount = string.IsNullOrWhiteSpace(Request.Query["skipCount"]) ? 0 : Convert.ToInt32(Request.Query["skipCount"]);
                int maxResultCount = string.IsNullOrWhiteSpace(Request.Query["maxResultCount"]) ? 20 : Convert.ToInt32(Request.Query["maxResultCount"]);
                const int MAXCOUNT = 1000;
                if (maxResultCount > MAXCOUNT)
                {
                    return this.BadRequestEx(new Dictionary<string, object>
                    {
                        { "code" , 10004 },
                        { "message" , $"The number of queries must be smaller than ${MAXCOUNT}"}
                    });
                }
                string fields = Request.Query["fields"];

                string sort = Request.Query["sort"];
                sort = sort ?? "" ;
                if (sort =="")
                {
                    if (!string.IsNullOrWhiteSpace(fields))
                    {
                        sort = fields.Substring(0, fields.IndexOf(",")) + " asc";
                    }
                    else {
                        sort = "code asc";
                    }
                   
                }
                else if (sort.Substring(1, 1) == "-")
                {
                    sort = sort.Substring(1)+@" desc";
                }
                else {
                    sort = sort.Substring(0) + @" asc";
                }

              

                var fruitDtoList = await _fruitService.FruitQuery(fields,sort, skipCount, maxResultCount);
                if (fruitDtoList == null)
                    return this.NotFoundEx();
                return this.OkEx(fruitDtoList);
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        
        }

        // GET api/v2/fruit/apple
        [HttpGet("{name}", Name = "GetFruit")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var fruitDtoList = await _fruitService.FruitQuery(name);
                if (fruitDtoList == null)
                    return this.NotFoundEx();
                return this.OkEx(fruitDtoList);
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
  
        }

        // POST api/v2/fruit/list
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<FruitDto> fruitDtoList)
        {
            try
            {
                if (fruitDtoList == null)
                {
                    return this.BadRequestEx("fruitDtoList");
                }
                await _fruitService.FruitAdd(fruitDtoList);
                return this.CreatedEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }

        }
        // POST api/v2/fruit
        [HttpPost("any")]
        public async Task<IActionResult> Post([FromBody]FruitDto fruitDto)
        {
            try
            {
                if (fruitDto == null)
                {
                    return this.BadRequestEx("fruitDto");
                }
                await _fruitService.FruitAdd(new List<FruitDto>() { fruitDto });
                return this.CreatedEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }

        }

        // PUT api/v2/fruit/apple
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody]FruitDto fruitDto)
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
                    return this.NotFoundEx();
                }

                await _fruitService.FruitUpdate(fruitDto);
                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }

        // DELETE api/v2/fruit/apple
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return this.BadRequestEx("name");
                }

                await _fruitService.FruitDelete(name);

                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }
    }
}
