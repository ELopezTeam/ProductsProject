﻿using System;
namespace ProductApi.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId)
            : base($"Product with ID {productId} not found.")
        {
        }
    }
}
