﻿using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.CardServ;
using ATM.Bank.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController: ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }



        [HttpPost("AddCard")]
       public async Task <ActionResult<ServiceResponce<string>>> AddCard(AddCardDto request)
        {
            return Ok(await _cardService.AddCard(request));
        }
        [HttpPost("AttachedExistingCardToBillNumber")]
        public async Task<ActionResult<ServiceResponce<string>>> AttachedExistingCardToBillNumber(string cardNumber, string billNumber)
        {
            return Ok(await _cardService.AttachedExistingCardToBillNumber(cardNumber, billNumber));
        }
    }
}
