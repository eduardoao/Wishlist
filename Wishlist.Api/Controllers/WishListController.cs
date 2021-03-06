﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wishlist.Api.DTO;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Services;

namespace Wishlist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishListController : Controller
    {      
        private readonly IWishClientService _wishClientService;
        private readonly IMapper _mapper;


        public WishListController(IWishClientService wishClientService, IMapper mapper)
        {

            _wishClientService = wishClientService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateWishList([FromBody] WishClientDTO wishClientDTO)
        {
          
            var client = new Client(wishClientDTO.IdClient);
            var product = new Product(new Title(wishClientDTO.Title));

            var wishClient = WishClient.WishClientBuilder(client, product);
            if (wishClient.Errors.Count > 0)
                return BadRequest(wishClient.Errors.Select(c => c.ErrorMessage));

            _wishClientService.Add(wishClient);

            if (_wishClientService.GetErrors().Count > 0)
                return BadRequest(_wishClientService.GetErrors());

            return Ok();
        }

    }
}
