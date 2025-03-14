using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDetailDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Interfaces;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OrderDetailManager : BaseManager<OrderDetail, OrderDetailDTO>, IOrderDetailManager
{
    readonly IOrderDetailRepository _oDR;
    public OrderDetailManager(IOrderDetailRepository odRep, IMapper mapper) : base(mapper, odRep)
    {
        _oDR = odRep;
    }

    public override string Destroy(OrderDetailDTO item)
    {
        OrderDetail entity= _mapper.Map<OrderDetail>(item);
        if (entity.Status == DataStatus.Deleted)
        {
            try
            {
                OrderDetail orderDetail = _oDR.FirstOrDefault(e => e.OrderID == item.OrderID && e.ProductID == item.ProductID);
                if (orderDetail != null)
                {
                    _oDR.Destroy(orderDetail);
                    return "Destroy işlemi başarılı";
                }
             
                return "Hata :  orderDetail nesnesi  null geldi.  OrderDetailManager/Destroy/orderDetail";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        return "Veriyi yok etmek için önce silmeniz lazım";
    }


    public override void Delete(OrderDetailDTO item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item), "Öğe boş olamaz.");
        OrderDetail entity = _mapper.Map<OrderDetail>(item);
        entity.Status = DataStatus.Deleted;
        entity.DeletedDate = DateTime.Now;
        OrderDetail originalEntity = _iRep.Find(entity.OrderID, entity.ProductID);
        _iRep.Entry(originalEntity, entity);
    }

    public override async Task UpdateAsync(OrderDetailDTO item)
    {
        OrderDetail entity = _mapper.Map<OrderDetail>(item);
        entity.ModifiedDate = DateTime.Now;
        entity.Status = DataStatus.Updated;
        OrderDetail originalEntity = await _iRep.FindAsync(entity.OrderID, entity.ProductID);
        _iRep.Entry(originalEntity, entity);
    }

}
