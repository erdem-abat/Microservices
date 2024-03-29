﻿using Microservices.MessageBus;
using Microservices.Services.AuthAPI.Models.Dto;
using Microservices.Services.AuthAPI.RabbitMQSender;
using Microservices.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        //private IMessageBus _messageBus;
        private IRabbitMQAuthMessageSender _messageBus;
        private readonly IConfiguration _configuration;
        public AuthAPIController(IAuthService authService, IRabbitMQAuthMessageSender messageBus, IConfiguration configuration)
        {
            _authService = authService;
            _response = new();
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            var errorMesage = await _authService.Register(registerationRequestDto);

            if (!string.IsNullOrEmpty(errorMesage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMesage;
                return BadRequest(_response);
            }
            await AssignRole(registerationRequestDto);
            _messageBus.SendMessage(registerationRequestDto.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var login = await _authService.Login(loginRequestDto);
            if(login.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password incorrect";
                return BadRequest(_response);
            }
            _response.Result = login;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            var assignRoleStatu = await _authService.AssignRole(registerationRequestDto.Email, registerationRequestDto.Role.ToUpper());
            if (!assignRoleStatu)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
