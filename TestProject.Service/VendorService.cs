using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestProject.Model.Enums;
using TestProject.Model.View;
using TestProject.Repository.Interface;
using TestProject.Service.Interface;
using TestProject.Utility.Extensions;
using DomainVendor = TestProject.Model.Domain.Vendor;
using DomainBank = TestProject.Model.Domain.Bank;
using ViewVendor = TestProject.Model.View.Vendor;

namespace TestProject.Service
{
    public class VendorService : IVendorService
    {

        private readonly IVendorRepository _vendorRepository ;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public void Save(int id, ViewVendor item, int modifiedId)
        {
            if (!item.IsValid())
            {
                throw new Exception("Invalid data.");
            }

            var vendor = _vendorRepository.Get(id);

            if (vendor == null)
            {
                throw new NullReferenceException("Vendor not found.");
            }

            if (modifiedId < 1)
            {
                throw new Exception("Invalid requester.");
            }

            vendor.Code = item.Code;
            vendor.Modified = DateTime.UtcNow;
            vendor.ModifiedUserId = modifiedId;
            vendor.Name = item.Name;
            
            _vendorRepository.Save();
        }

        public object Create(ViewVendor item, int createdId)
        {
            if (!item.IsValid())
            {
                throw new Exception("Invalid data.");
            }

            var existingVendor =
                _vendorRepository.Query(v => !v.Deleted && v.Code.Equals(item.Code, StringComparison.OrdinalIgnoreCase)).Any();

            if (existingVendor)
            {
                throw new Exception("Duplicate vendor code.");
            }

            var domainVendor = new DomainVendor(item, createdId);

            if (item.Bank != null)
            {
                domainVendor.Bank = new DomainBank(item.Bank, item.Country, createdId);
            }

            _vendorRepository.Create(domainVendor);

            return domainVendor.VendorId;
        }

        public void Remove(int id, int modifiedId)
        {
            if (modifiedId < 1)
            {
                throw new Exception("Invalid requester.");
            }

            var vendor = _vendorRepository.Get(id);

            if (vendor == null)
            {
                throw new NullReferenceException("Vendor not found.");
            }

            vendor.Deleted = true;
            vendor.Modified = DateTime.UtcNow;
            vendor.ModifiedUserId = modifiedId;

            _vendorRepository.Save();
        }

        public ViewVendor Get(int id)
        {
            var domainVendor = _vendorRepository.Get(id);
            return domainVendor == null || domainVendor.Deleted
                ? null 
                : new ViewVendor(domainVendor);
        }

        public List<ViewVendor> Query(VendorQuery filter)
        {
            Expression<Func<DomainVendor, bool>> search = vendor => !vendor.Deleted;

            if (!string.IsNullOrEmpty(filter.Code))
            {
                search = search.AndAlso(vendor => vendor.Code.Equals(filter.Code));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                search = search.AndAlso(vendor => vendor.Name.Equals(filter.Name));
            }

            if (filter.Status != null)
            {
                search = search.AndAlso(vendor => vendor.Status == filter.Status);
            }

            return _vendorRepository.Query(search)
                .Select(v => new ViewVendor
                {
                    Name = v.Name,
                    Code = v.Code,
                    Status = v.Status.ToString(),
                    VendorId = v.VendorId
                })
                .ToList();
        }
    }
}
