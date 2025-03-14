using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.COMMON.Tools.Services;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class ProductManager : BaseManager<Product, ProductDTO>, IProductManager
    {
        readonly IProductRepository _prdRep;
        readonly IMapper _mapper;

        public ProductManager(IProductRepository prdRep, IMapper mapper) : base(mapper, prdRep)
        {
            _prdRep = prdRep;
            _mapper = mapper;
        }

        public override string Destroy(ProductDTO item)
        {

            Product entity = _mapper.Map<Product>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {

                   Product product = _prdRep.FirstOrDefault(e => e.ID == item.ID);
                    if (product != null)
                    {
                        DeleteImage(product);
                        _prdRep.Destroy(product);
                        return "Destroy işlemi başarılı";
                    }
                    return "Hata :  product nesnesi  null geldi.  ProductManager/Destroy/product";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yok etmek için önce silmeniz lazım";
        }

        void DeleteImage(Product product)
        {
            string fullPath = $"{Directory.GetCurrentDirectory()}/wwwroot{product.ImagePath}";
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

                // Yeni migration işleminde hata vermemesi için!
                fullPath = $"{Directory.GetCurrentDirectory()}/wwwroot/images/imagePathsForDataSeed.txt";
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            }

        }

        public async Task<string> CreateProductAsync(IFormFile formFile, ProductDTO pDTO)
        {
            string path = await ImageService.Upload(formFile);
            if (path != null)
            {
                pDTO.ImagePath = path;
                return await AddAsync(pDTO);
            }
            else return "Resim yükleme sırasında bir hata oluştu";
        }

        public async Task UpdateProductAsync(IFormFile formFile, ProductDTO pDTO)
        {
            string path = await ImageService.Upload(formFile);
            if (path != null)
            {
                pDTO.ImagePath = path;
            }
            await UpdateAsync(pDTO);
        }
    }
}
