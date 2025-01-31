﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainTdo;

namespace SEIIApp.Server.Controllers {

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase {
       
        private LoginService LoginService { get; set; }
        private IMapper Mapper { get; set; }

        public LoginController(LoginService LoginService, IMapper mapper) {
            this.LoginService = LoginService;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Return the quiz with the given id.
        /// </summary>
        /// <param userName="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Shared.DomainTdo.LoginDto> GetRole([FromRoute] string username) {
            string user = username.Split("$")[0];
            string password = username.Split("$")[1];
            var auth = LoginService.GetAuth(user, password);

            if (auth == null) return StatusCode(StatusCodes.Status404NotFound);

            var successLogin = Mapper.Map<LoginDto>(auth);
            return Ok(successLogin);
        }


        /// <summary>
        /// Adds or updates a course definition.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LoginDto> AddAuth([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {

                var mappedModel = Mapper.Map<Authentifizierung>(model);

                if (model.Id == 0)
                { //add
                    mappedModel = LoginService.AddAuth(mappedModel);
                }

                model = Mapper.Map<LoginDto>(mappedModel);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }


    }
}
