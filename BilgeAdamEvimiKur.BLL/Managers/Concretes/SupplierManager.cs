using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.SupplierDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class SupplierManager : BaseManager<Supplier, SupplierDTO>, ISupplierManager
    {
        readonly ISupplierRepository _sRep;
        readonly IProductManager _productManager;
        readonly IMapper _mapper;
        public SupplierManager(ISupplierRepository supRep, IMapper mapper, IProductManager productManager) : base(mapper, supRep)
        {
            _sRep = supRep;
            _mapper = mapper;
            _productManager = productManager;
        }

        public override string Destroy(SupplierDTO item)
        {
            Supplier entity = _mapper.Map<Supplier>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {
                    Supplier supplier = _sRep.FirstOrDefault(e => e.ID == entity.ID);
                    if (supplier != null)
                    {
                        _sRep.Destroy(supplier);
                        return "Destroy işlemi başarılı";
                    }
                    return "Hata :  supplier nesnesi  null geldi.  SupplierManager/Destroy/supplier";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yok etmek için önce silmeniz lazım";
        }
    }
}
