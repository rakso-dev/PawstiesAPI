﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PawstiesAPI.Models;
using System;
using PawstiesAPI.Helper;
using PawstiesAPI.Services;
using PawstiesAPI.Singletons;

namespace PawstiesAPI.Controllers
{
    public class MascotaController: ControllerBase
    {
        private readonly ILogger<MascotaController> _logger;
        private readonly IMascotaService _service;
        //private readonly ITallaSingleton _talla;

        public MascotaController(IMascotaService service, ILogger<MascotaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet ("pawstiesAPI/mascotas/rescatista/{rescatistaid}")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof(IEnumerable<Mascotum>))]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int rescatistaid)
        {
            _logger.LogInformation("Calling method GetAllMascotas");
            var mascota = _service.GetMascotaByRescatista(rescatistaid);
            return Ok (mascota);
        }

        [HttpGet("pawstiesAPI/mascotas/get/{distance}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Mascotum>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromBody] JSONPoint point, int distance)
        {
            _logger.LogInformation("Calling method GetAllMascotas");
            var mascotas = _service.GetAll(point, distance);
            return Ok(mascotas);
        }

        [HttpGet ("pawstiesAPI/mascotas/{petID}")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof(Mascotum))]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult GetMascota(int petID)
        {
            _logger.LogInformation($"Calling method GetMascota with petID {petID}", null);
            var mascota = _service.GetMascota(petID);
            if (mascota == null)
            {
                return BadRequest("Inexistent petID");
            }
            return Ok(mascota);
        }

        /*[HttpPut ("pawstiesAPI/mascota/{petid}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int petid, Mascotum pet)
        {
            try
            {
                Mascotum mascota = _context.Mascota.Where(e => e.Petid == petid).FirstOrDefault();
                if (pet == null)
                {
                    return BadRequest();
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during Mascota update with petid = {petid}");
                throw;
            }
        }*/
    }
}
