using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Abstracts
{
    public interface IProductManager : IManager<Product, ProductDTO>
    {
        Task<string> CreateProductAsync(IFormFile formFile, ProductDTO pDTO );
        Task UpdateProductAsync(IFormFile formFile, ProductDTO pDTO);

    }
}
