using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.CategoryDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class CategoryManager : BaseManager<Category, CategoryDTO>, ICategoryManager
    {
        readonly ICategoryRepository _catRep;
        readonly IProductManager _productManager;
        readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository catRep, IMapper mapper, IProductManager productManager) : base(mapper, catRep)
        {
            _catRep = catRep;
            _productManager = productManager;
            _mapper = mapper;
        }

        public override string Destroy(CategoryDTO item)
        {
            Category entity = _mapper.Map<Category>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {
                    Category category = _catRep.FirstOrDefault(e => e.ID == item.ID);
                    if (category != null)
                    {
                        _catRep.Destroy(category);
                        return "Destroy işlemi başarılı";
                    }
                    return "Hata :  category nesnesi  null geldi.  CategoryManager/Destroy/category";

                } catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yok etmek için önce silmeniz lazım";
        }
    }
}
