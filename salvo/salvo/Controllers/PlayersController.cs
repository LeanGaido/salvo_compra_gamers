using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salvo.Models;
using salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace salvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerRepository _repository;

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerDTO playerDTO)
        {
            bool CamposInvalidos = false;
            string MensajeError = "";

            #region Validaciones
            if (string.IsNullOrEmpty(playerDTO.Name))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Name cannot be Empty" : MensajeError + " - The Name cannot be Empty";
            }
            if (!isValidName(playerDTO.Name))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Name is not valid" : MensajeError + " - The Name is not valid";
            }
            if (string.IsNullOrEmpty(playerDTO.Email))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Email cannot be Empty" : MensajeError + " - The Email cannot be Empty";
            }
            if (!isValidEmail(playerDTO.Email))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Email is not valid" : MensajeError + " - The Email is not valid";
            }
            if (string.IsNullOrEmpty(playerDTO.Password))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Password cannot be Empty" : MensajeError + " - The Password cannot be Empty";
            }
            if (!IsValidPassword(playerDTO.Password))
            {
                CamposInvalidos = true;
                MensajeError = (string.IsNullOrEmpty(MensajeError)) ? "The Password is not valid" : MensajeError + " - The Password is not valid";
            }
            #endregion

            if (!CamposInvalidos)
            {
                Player newPlayer = _repository.FindByEmail(playerDTO.Email);
                if (newPlayer == null)
                {
                    newPlayer = new Player
                    {
                        Name = playerDTO.Name,
                        Email = playerDTO.Email,
                        Password = playerDTO.Password
                    };

                    _repository.Create(newPlayer);
                    _repository.SaveChanges();

                    return StatusCode(201, "Creado");
                }
                else
                {
                    return StatusCode(403, "Email en uso");
                }
            }
            else
            {
                return StatusCode(403, MensajeError);
            }
        }

        public bool isValidName(string Name)
        {
            var hasBetween3And30Chars = new Regex(@".{3,30}");

            return hasBetween3And30Chars.IsMatch(Name);
        }
        public bool isValidEmail(string Email)
        {
            var hasBetween10And30Chars = new Regex(@".{10,30}");

            bool hasValidFormat = true;

            try
            {
                MailAddress address = new MailAddress(Email);
                hasValidFormat = (address.Address == Email);
                // or
                // isValid = string.IsNullOrEmpty(address.DisplayName);
            }
            catch (FormatException)
            {
                hasValidFormat = false;
            }

            return hasValidFormat && hasBetween10And30Chars.IsMatch(Email);
        }
        public bool IsValidPassword(string Password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSpecialChar = new Regex("[^a-zA-Z0-9]");

            return hasNumber.IsMatch(Password) && hasUpperChar.IsMatch(Password) && hasMinimum8Chars.IsMatch(Password) && hasSpecialChar.IsMatch(Password);
        }
    }
}
