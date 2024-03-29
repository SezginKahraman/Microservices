﻿using MediatR;
using Microservices.Services.Order.Application.Dtos;
using Microservices.Shared.Core_3_1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        //public int PaymentNo{ get; set; } // will add to match with the payment

        public List<OrderItemDto> OrderItems{ get; set; }

        public AddressDto Address { get; set; }
    }
}
