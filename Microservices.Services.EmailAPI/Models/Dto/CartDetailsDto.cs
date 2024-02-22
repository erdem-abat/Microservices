﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Services.EmailAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? MyProperty { get; set; }
        public int Count { get; set; }
    }
}
