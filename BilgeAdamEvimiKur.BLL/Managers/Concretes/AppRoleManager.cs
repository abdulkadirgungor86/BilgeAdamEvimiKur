using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppRoleDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class AppRoleManager : BaseManager<AppRole, AppRoleDTO>, IAppRoleManager
    {
        readonly IMapper _mapper;
        readonly IAppRoleRepository _aRRep;
        public AppRoleManager(IMapper mapper, IAppRoleRepository aRRep) : base(mapper, aRRep)
        {
            _mapper = mapper;
            _aRRep = aRRep;
        }
        public override string Destroy(AppRoleDTO item)
        {
            AppRole entity = _mapper.Map<AppRole>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {
                    AppRole existingItem = _aRRep.FirstOrDefault(e => e.Id == entity.Id);
                    if (existingItem != null) _aRRep.Destroy(existingItem);
                    else throw new ArgumentException("Hata : existingItem nesnesi değeri null. Veritabanında bulunamadı. AppRoleManager/Destroy/existingItem");
                    return "Destroy işlemi başarılı";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yoketmek için önce silmeniz lazım";
        }

        public override void Delete(AppRoleDTO item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Öğe boş olamaz.");
            AppRole entity = _mapper.Map<AppRole>(item);
            entity.Status = DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            AppRole originalEntity = _aRRep.Find(entity.Id);
            _aRRep.Entry(originalEntity, entity);
        }



        public override async Task UpdateAsync(AppRoleDTO item)
        {
            AppRole entity = _mapper.Map<AppRole>(item);
            entity.ModifiedDate = DateTime.Now;
            entity.Status = DataStatus.Updated;
            entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            AppRole originalEntity = await _aRRep.FindAsync(entity.Id);
            _aRRep.Entry(originalEntity, entity);
        }

        public override async Task<string> AddAsync(AppRoleDTO item)
        {
            try
            {
                AppRole entity = _mapper.Map<AppRole>(item);
                entity.ConcurrencyStamp = Guid.NewGuid().ToString();
                await _iRep.AddAsync(entity);
                return "Başarıyla eklendi";
            }
            catch (Exception ex)
            {
                return $"Ekleme yaparken hata oluştu : {ex.Message}";
            }

        }
    }
}
