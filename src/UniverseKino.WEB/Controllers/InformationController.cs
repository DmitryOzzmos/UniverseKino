﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniverseKino.Services.Dto;
using UniverseKino.Services.Services;
using UniverseKino.Services.Interfaces;
using UniverseKino.WEB.Models;
using UniverseKino.Services;

namespace UniverseKino.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private IInfoMoviesService _moviesServ;
        private IMapper _mapper;
        private readonly ISessionsInfoService _sessionsInfoService;

        public InfoController(IInfoMoviesService moviesServ, ISessionsInfoService sessionsInfoService, IMapper mapper)
        {
            _moviesServ = moviesServ;
            _sessionsInfoService = sessionsInfoService;
            _mapper = mapper;
        }


        //public InformationController(ISessionsInfoService sessionsInfoService, IMapper mapper)
        //{
        //    _sessionsInfoService = sessionsInfoService;
        //    _mapper = mapper;
        //}

        [HttpGet("schedule")]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessionDTO = _sessionsInfoService.GetAllSessions();

            //var sessionModel = _mapper.Map<List<SeatModel>>(sessionDTO);

            return await Task.Run(() => Ok(sessionDTO));
        }

        [HttpGet("sessions/{id}")]
        public async Task<IActionResult> GetSession([FromQuery] int id)
        {
            return await Task.Run(() => Ok());
        }

        [HttpGet]
        [Route("movies")]
        public IActionResult GetAllMovies()
        {
            var movies = _moviesServ.GetAllMovies();

            return Ok(_mapper.Map<List<MovieDTO>>(movies));
        }

        [HttpGet("movies/{id}")]
        public async Task<IActionResult> GetMovie([FromQuery] int id)
        {
            var movie = await _moviesServ.GetMovieByID(id);

            return Ok(_mapper.Map<MovieDTO>(movie));
        }

        [HttpGet("movies/{id}/sessions")]
        public async Task<IActionResult> GetSessionsMovie([FromQuery] int id)
        {
            var sessions = await _moviesServ.GetMoviesSessions(id);

            return Ok(_mapper.Map<List<SessionDTO>>(sessions));
        }
    }
}
