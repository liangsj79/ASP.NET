﻿using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCastDetails(int id)
        {
            var cast = await _castService.GetCastDetailsById(id);
            return Ok(cast);
        }
    }
}
